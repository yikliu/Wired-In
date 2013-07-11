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
    public partial class ShowVisualizations : Form
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

        private bool guideSuccess = false;

        private string selected_vis = "rose";

        public ShowVisualizations()
        {           
            InitializeComponent();
            this.Hide();
            Guide theGuideForm = new Guide();
            if (theGuideForm.ShowDialog() == DialogResult.OK)
            {
                guideSuccess = true;
                this.selected_vis = theGuideForm.GetSelectedVis();
                switch (this.selected_vis.ToLower())
                {
                    case "rose":
                    case "moon":
                        Constants.Config.VIS_IMAGE = Constants.Visualization.ManyStepImages;
                        break;
                    case "progressbar":
                        Constants.Config.VIS_IMAGE = Constants.Visualization.Progressbar;
                        break;
                    default:
                        Constants.Config.VIS_IMAGE = Constants.Visualization.Custom;
                        break;
                }
            }
            else
            {
                guideSuccess = false;
            }            
         }

        public void AttachListeners()
        {
            this.globalEventProvider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider_MouseUp);
            this.globalEventProvider.KeyUp += new System.Windows.Forms.KeyEventHandler(this.globalEventProvider_KeyUp);           
        }

        public void DetachListeners()
        {
            this.globalEventProvider.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.globalEventProvider_MouseUp);
            this.globalEventProvider.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.globalEventProvider_KeyUp);
        }

        public void TurnOnVisualization()
        {            
            CreateView();
            AttachListeners();          
            activityQueue = new ObservableCollection<Activity>();
            currentWindowInfo = new WindowInfo();
            winWatchTimer.Enabled = false;
            worker = new Worker(activityQueue, myView.GetTransitCommand());
            activityQueue.CollectionChanged += worker.OnActiveQueueChange;
            this.Show();
        }

        public void AttachView()
        {
            worker.TransitCommand = myView.GetTransitCommand();
        }
        
        /// <summary>
        /// Init the view based on config setting
        /// </summary>
        public void CreateView()
        {
            switch (Constants.Config.VIS_IMAGE)
            {
                case Constants.Visualization.ManyStepImages:
                    if (this.myView is WiredIn.View.ManyStepsView)
                    {
                        return;
                    }
                    this.Controls.Remove(this.myView);
                    this.myView = new WiredIn.View.ManyStepsView(this.selected_vis);
                    break;
                case Constants.Visualization.Progressbar:
                    if (this.myView is WiredIn.View.ProgressBarView)
                        return;
                    this.Controls.Remove(this.myView);
                    this.myView = new WiredIn.View.ProgressBarView();
                    break;
                case Constants.Visualization.Empty:
                    if (this.myView is WiredIn.View.EmptyView)
                        return;
                    this.Controls.Remove(this.myView);
                    this.myView = new WiredIn.View.EmptyView();
                    break;
                case Constants.Visualization.Custom:
                    if (this.myView is WiredIn.View.CustomView)
                        return;
                    this.Controls.Remove(this.myView);
                    this.myView = new WiredIn.View.CustomView(this.selected_vis);
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
            this.myView.SetSize(this.Size);             
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
            this.myView.SetSize(this.Size);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (guideSuccess)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                TurnOnVisualization();
                SwitchAppSize();
                currentWindowInfo.update(SystemWindow.ForegroundWindow); //init Current WindowInfo
            }
            else
            {
                this.Close();
            }
            
        }

        public void SwitchAppSize()
        {
            if (Constants.Config.APP_SIZE == Constants.AppSize.Small)
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
                EnqueueActivity(new WindowChangeActivity(window.Process.ProcessName, window.Title, DateTime.Now, myView.GetScore()));
                currentWindowInfo.update(window);
            }
            worker.DriveView();
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
                EnqueueActivity(new StartUp(DateTime.Now, myView.GetScore()));
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

            if (setting.ShowDialog() == DialogResult.OK)
            {
                // Do something here to handle data from dialog box.
            }
        }

        private void globalEventProvider_KeyUp(object sender, KeyEventArgs e)
        {           
            EnqueueActivity(new KeyPress(e.KeyCode, DateTime.Now, myView.GetScore()));
        }

        private void globalEventProvider_MouseUp(object sender, MouseEventArgs e)
        {            
            EnqueueActivity(new MouseClick(DateTime.Now,myView.GetScore()));
        }

        /// <summary>
        /// Clean up before exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myView != null && winWatchTimer != null && worker != null)
            {
                EnqueueActivity(new ShutDown(DateTime.Now, myView.GetScore()));
                winWatchTimer.Stop();
                worker.StopWorker();
            }            
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
            myView.UpdateView(false);   
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Constants.Config.APP_SIZE = Constants.AppSize.Small;
            SwitchAppSize();
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Constants.Config.APP_SIZE = Constants.AppSize.Full;
            SwitchAppSize();
        }
    }
}
