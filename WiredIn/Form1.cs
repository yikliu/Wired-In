using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using ManagedWinapi.Windows;
using Wired_In.Analyzer;
using Wired_In.UserActivity;
using Wired_In.Windows;
using Wired_In;

namespace Wired_In
{
    public partial class MainForm : Form
    {
        private ObservableCollection<Activity> activityQueue;
        
        private WindowInfo currentWindowInfo;        
        
        private Judge judge;
        
        private Logger logger;

        public Point lastClick;
        
        public bool drag = false;       

        public bool isTimerStarted = false;                

        private FormState formState = new FormState();

        public MainForm()
        {
            this.myView = new Wired_In.View.ImageView();
            //this.myView = new Wired_In.View.DigitalClockView();

            InitializeComponent();
            activityQueue = new ObservableCollection<Activity>();
            currentWindowInfo = new WindowInfo();
            win_watcher_timer.Enabled = false;
            judge = new Judge(activityQueue,myView);
            activityQueue.CollectionChanged += judge.OnActiveQueueChange;
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
            //this.myView.Size = this.Size;
            this.WindowState = FormWindowState.Normal;
            //this.WindowState = FormWindowState.Maximized;            
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            showOnMonitor(1);  
            //updateScore(0.01);                      
            this.TopMost = false;
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
                enqueueActivity(new ProcChanged(window.Process.ProcessName, DateTime.Now));
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
            if (btnStart.Text.Equals("START"))
            {
                btnStart.Text = "PAUSE";
                isTimerStarted = true;
                win_watcher_timer.Start();
                judge.StartJudge();
            }
            else
            {
                btnStart.Text = "START";
                isTimerStarted = false;
                win_watcher_timer.Stop();
                judge.PauseJudge();
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
            win_watcher_timer.Stop();
            judge.StopJudge();
        }

        private void ChangeSize(Size newSize)
        {
            myView.Width = newSize.Width / 2;
            myView.Height = newSize.Height / 2;
            this.Size = myView.Size;

            pnlControls.Height = (int)Math.Round(myView.Width * 0.1);
            pnlControls.Width = (int)Math.Round(myView.Width * 0.8);
            btnStart.Width = (int)Math.Round(pnlControls.Width * 0.25);
            btnStart.Height = (int)Math.Round(pnlControls.Height * 0.7);
            btnStart.Left = (int)Math.Round(pnlControls.Width * 0.1);
            btnExit.Width = (int)Math.Round(pnlControls.Width * 0.25);
            btnExit.Height = (int)Math.Round(pnlControls.Height * 0.7);

            int a = pnlControls.Width / 2 - btnStart.Width - btnStart.Left;
            btnExit.Left = btnStart.Width + 2 * a;
            Point ptControlPanel = new Point();
            ptControlPanel.X = myView.Width / 2 - pnlControls.Width / 2;
            ptControlPanel.Y = myView.Height - pnlControls.Height - 10;
            pnlControls.Location = ptControlPanel;
        }
         
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


        private void myView_MouseHover(object sender, EventArgs e)
        {
            pnlControls.Visible = true;
        }

        /// <summary>
        ///   mouse leave the picture box
        /// </summary>
        private void myView_MouseLeave(object sender, EventArgs e)
        {
            if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                //the mouse is inside the control bounds
                return;
            }
            else
            {  // the mouse is outside the control bounds
                pnlControls.Visible = false;
            }

        }

       

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm settings = new SettingForm();
            settings.Show();
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formState.Maximize(this);
        }

        private void myView_Load(object sender, EventArgs e)
        {            
            myView.updateView(false);   
        }

      

        

                
        
    }
}
