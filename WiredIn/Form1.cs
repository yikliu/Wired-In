using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using ManagedWinapi.Windows;
using WiredIn.Analyzer;
using WiredIn.UserActivity;
using WiredIn.Windows;
using WiredIn;

namespace WiredIn
{
    public partial class MainForm : Form
    {
        private ObservableCollection<Activity> activityQueue;
        
        private WindowInfo currentWindowInfo;        
        
        private Worker worker;
        
        private Logger logger;

        public Point lastClick;
        
        public bool drag = false;       

        public bool isTimerStarted = false;                

        private FormState formState = new FormState();

        public MainForm()
        {           
            switch (Constants.Config.VIS_IMAGE)
            {
                case Constants.imagery.flower:
                    this.myView = new WiredIn.View.ImageView();
                    break;
                case Constants.imagery.progressbar:
                    this.myView = new WiredIn.View.ProgressBarView();
                    break;
            }

            InitializeComponent();
            activityQueue = new ObservableCollection<Activity>();
            currentWindowInfo = new WindowInfo();
            winWatchTimer.Enabled = false;
            worker = new Worker(activityQueue,myView);
            activityQueue.CollectionChanged += worker.OnActiveQueueChange;
            logger = new Logger(activityQueue);
        }

        private void showOnMonitor(int monitor)
        {
            Screen[] sc;
            sc = Screen.AllScreens;           

            Screen s = monitor >= sc.Length ? sc[0] : sc[monitor];
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.Left = s.Bounds.Left;
            this.Top = s.Bounds.Top;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Left, Top);
            this.Size = s.Bounds.Size;      
            this.WindowState = FormWindowState.Maximized;
            this.myView.setSize(this.Size);
            //ChangeSize(this.Size);
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            showOnMonitor(1);              
        }

        /// <summary>
        ///   Put an user activity in the end of the activity queue
        /// </summary>
        private void enqueueActivity(Activity a)
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

        private void WindowsWatcherTimerTick(object sender, EventArgs e)
        {
            SystemWindow window = SystemWindow.ForegroundWindow;
            if (!currentWindowInfo.belongToSameProcess(window))
            {
                enqueueActivity(new WindowChangeActivity(window.Process.ProcessName, window.Title, DateTime.Now));
                currentWindowInfo.update(window);
            }
            if (!currentWindowInfo.WinTitle.Equals(window.Title))
            {
                currentWindowInfo.update(window);
            }           
            this.logger.DequeueActivity();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem start = (ToolStripMenuItem)this.menu.Items[0];
            if(start.Text.Equals("Start"))            
            {
                start.Text = "Pause";
                isTimerStarted = true;
                winWatchTimer.Start();
                worker.StartJudge();
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
            Form setting = new SettingForm();

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
            enqueueActivity(new KeyPress(e.KeyCode, DateTime.Now));
        }

        private void globalEventProvider_MouseUp(object sender, MouseEventArgs e)
        {
            // System.Console.WriteLine("Mouse Clicked: " + e.Location.ToString());
            enqueueActivity(new MouseClick(DateTime.Now));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            winWatchTimer.Stop();
            worker.StopJudge();
        }
        /*
        private void ChangeSize(Size newSize)
        {
            myView.Width = newSize.Width / 2;
            myView.Height = newSize.Height / 2;
            this.Size = myView.Size;

            
            pnlControls.Height = (int)Math.Round(myView.Width * 0.1);
            pnlControls.Width = (int)Math.Round(myView.Width * 0.8);

            btnStart.Width = (int)Math.Round(pnlControls.Width * 0.25);
            btnStart.Height = (int)Math.Round(pnlControls.Height * 0.7);
           

            btnExit.Width = (int)Math.Round(pnlControls.Width * 0.25);
            btnExit.Height = (int)Math.Round(pnlControls.Height * 0.7);            

            btnSetting.Width = (int)Math.Round(pnlControls.Width * 0.25);
            btnSetting.Height = (int)Math.Round(pnlControls.Height * 0.7);


            int a = (pnlControls.Width - 3 * btnStart.Width) / 4;
            btnStart.Left = a;
            btnExit.Left = btnStart.Width + 2 * a;
            btnSetting.Left = 2*btnStart.Width + 3 * a;

            Point ptControlPanel = new Point();
            ptControlPanel.X = this.Size.Width / 2 - pnlControls.Width / 2;
            ptControlPanel.Y = this.Size.Height / 2;
            pnlControls.Location = ptControlPanel;
            
        }
         */
        /*
        public void updateScore(double score)
        {
            myView.updateView(score);    
        }*/

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

        private void myView_Load(object sender, EventArgs e)
        {            
            myView.updateView(false);   
        }
    }
}
