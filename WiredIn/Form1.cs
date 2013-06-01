using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using ManagedWinapi.Windows;
using WiredIn.Analyzer;
using WiredIn.UserActivity;
using WiredIn.Windows;

namespace WiredIn
{
    /// <summary>
    /// Main Form
    /// </summary>
    public partial class MainForm : Form
    {
        //The queue storing all keyboard/mouse activity
        private ObservableCollection<Activity> activityQueue;
        
        //A data structure for storing windows information (See MangedWinapi)
        private WindowInfo currentWindowInfo;        
        
        //An instance of background worker
        private Worker worker;

        //Allowing dragging a windows
        public Point lastClick;
        
        //whether dragging is allowed
        public bool drag = false;       

        //whether the timer has started
        public bool isTimerStarted = false;                

        public MainForm()
        {           
            InitializeComponent();
            
            createView();
            
            activityQueue = new ObservableCollection<Activity>();
            
            currentWindowInfo = new WindowInfo();
            
            winWatchTimer.Enabled = false;
            
            worker = new Worker(activityQueue,myView);
            
            activityQueue.CollectionChanged += worker.OnActiveQueueChange;
         }

        public void AttachView()
        {
            worker.setView(myView);
        }
        
        /// <summary>
        /// Init the view based on config setting
        /// </summary>
        public void createView()
        {
            switch (Constants.Config.VIS_IMAGE)
            {
                case Constants.imagery.flower:
                    if (this.myView is WiredIn.View.ImageView)
                    {
                        return;
                    }
                    this.Controls.Remove(this.myView);
                    this.myView = new WiredIn.View.ImageView();
                    break;
                case Constants.imagery.progressbar:
                    if (this.myView is WiredIn.View.ProgressBarView)
                        return;
                    this.Controls.Remove(this.myView);
                    this.myView = new WiredIn.View.ProgressBarView();
                    break;
                case Constants.imagery.empty:
                    if (this.myView is WiredIn.View.EmptyView)
                        return;
                    this.Controls.Remove(this.myView);
                    this.myView = new WiredIn.View.EmptyView();
                    break;
            }
            
            this.SuspendLayout();
            this.myView.ForeColor = System.Drawing.Color.Transparent;
            this.myView.Location = new System.Drawing.Point(0, 0);
            this.myView.Name = "myView";
            this.myView.TabIndex = 7;
            this.myView.Load += new System.EventHandler(this.myView_Load);
            this.myView.Dock = DockStyle.Fill;
            this.myView.ContextMenuStrip = menu;
           
            this.Controls.Add(this.myView);
            SwitchAppSize();
            this.ResumeLayout();
        }

        /// <summary>
        /// Show on first extended monitor
        /// </summary>
        /// <param name="monitor"></param>
        private void ShowOnMonitor(int monitor)
        {
            Screen[] sc;
            sc = Screen.AllScreens;        
            Screen s = monitor >= sc.Length ? sc[0] : sc[monitor];            
            this.Left = s.Bounds.Left;
            this.Top = s.Bounds.Top;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Left, this.Top);
            this.Size = s.Bounds.Size;
            this.WindowState = FormWindowState.Maximized;
            this.myView.setSize(this.Size);             
        }
        
        /// <summary>
        /// Show on right-bottom of the primary screen
        /// </summary>
        private void showOnRightBottom()
        {
            Screen s = Screen.PrimaryScreen;
            int het = s.WorkingArea.Height / Constants.Config.SHRINK_FACTOR;
            int wth = s.WorkingArea.Width / Constants.Config.SHRINK_FACTOR;

            this.Size = new Size(wth, het);            
            this.Left = s.WorkingArea.Right - wth;
            this.Top = s.WorkingArea.Bottom - het;          
            this.StartPosition = FormStartPosition.Manual;

            this.TopMost = true;
            
            this.Location = new Point(this.Left, this.Top);
            this.WindowState = FormWindowState.Normal;
            this.myView.setSize(this.Size);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            SwitchAppSize();
            currentWindowInfo.update(SystemWindow.ForegroundWindow); //init Current WindowInfo
        }

        public void SwitchAppSize()
        {
            if (Constants.Config.APP_SIZE == Constants.app_size.small)
            {
                showOnRightBottom();                
            }
            else
            {
                ShowOnMonitor(1);
            }
        }

        /// <summary>
        ///   Put an user activity in the end of the activity queue
        /// </summary>
        private void EnqueueActivity(Activity a)
        {
            if (!isTimerStarted)
                return;
            try
            {
                lock (this)
                {
                    activityQueue.Add(a);
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Check the topmost window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowsWatcherTimerTick(object sender, EventArgs e)
        {
            SystemWindow window = SystemWindow.ForegroundWindow;            
            if (!currentWindowInfo.WinTitle.Equals(window.Title))
            {
                EnqueueActivity(new WindowChangeActivity(window.Process.ProcessName, window.Title, DateTime.Now, myView.getScore()));
                currentWindowInfo.update(window);
            }           
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem start = (ToolStripMenuItem)this.menu.Items[0];
            if(start.Text.Equals("Start"))            
            {
                start.Text = "Pause";
                isTimerStarted = true;
                winWatchTimer.Start();
                worker.StartWoker();
                EnqueueActivity(new StartUp(DateTime.Now, myView.getScore()));
            }
            else
            {
                start.Text = "Start";
                isTimerStarted = false;
                winWatchTimer.Stop();
                worker.PauseJudge();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            // Create and display an instance of the dialog box
            Form setting = new SettingForm(this);

            // Show the dialog and determine the state of the 
            // DialogResult property for the form.
            if (setting.ShowDialog() == DialogResult.OK)
            {
                // Do something here to handle data from dialog box.
            }
        }

        private void globalEventProvider_KeyUp(object sender, KeyEventArgs e)
        {
            //System.Console.WriteLine("Key: " + e.KeyCode);
            EnqueueActivity(new KeyPress(e.KeyCode, DateTime.Now, myView.getScore()));
        }

        private void globalEventProvider_MouseUp(object sender, MouseEventArgs e)
        {
            // System.Console.WriteLine("Mouse Clicked: " + e.Location.ToString());
            EnqueueActivity(new MouseClick(DateTime.Now,myView.getScore()));
        }

        /// <summary>
        /// Clean up before exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EnqueueActivity(new ShutDown(DateTime.Now, myView.getScore()));
            winWatchTimer.Stop();
            worker.StopWorker();
        }
       
        private void btn_exit_Click(object sender, EventArgs e)
        {            
            Application.Exit();
        }
        
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag && e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            lastClick = e.Location;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        /// <summary>
        /// Refresh the view when loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myView_Load(object sender, EventArgs e)
        {            
            myView.updateView(false);   
        }

        public void setTransitSpeed()
        {
            this.worker.setInterval();
        }
    }
}
