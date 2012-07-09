using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wired_In.UserActivity;
using ManagedWinapi.Windows;
using Wired_In.Windows;
using Wired_In.Analyzer;

using ManagedWinapi.Hooks;
using WiredIn;

namespace Wired_In
{
    public partial class MainForm : Form
    {
        private ObservableCollection<Activity> activity_queue;
        
        private WindowInfo current_window_info;
        private Judge theJudge;
        private Logger theLogger;

        public Point lastClick;
        public bool drag = false;

        public int pic_id;

        public bool IsTimerStarted = false;
        
        public int number_of_pics;

        private Size currentSize;

        private FormState formState = new FormState();

        public MainForm()
        {
            InitializeComponent();
            activity_queue = new ObservableCollection<Activity>();
            current_window_info = new WindowInfo();
            win_watcher_timer.Enabled = false;
            theJudge = new Judge(activity_queue);
            activity_queue.CollectionChanged += theJudge.OnActiveQueueChange;
            theLogger = new Logger(activity_queue);
        }

        private void setFormLocation()
        {
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            //default location is right bottom
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            countNumberOfFiles();
            showImage(0.01);
            setFormLocation(); 
            
            this.TopMost = true;
        }

        /// <summary>
        ///   Put an user activity in the end of the activity queue
        /// </summary>
        private void enqueueActivity(Activity a)
        {
            if (!IsTimerStarted)
                return;
            try
            {
                lock (this)
                {
                    activity_queue.Add(a);
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
            if (!current_window_info.belongToSameProcess(window))
            {
                enqueueActivity(new ProcChanged(window.Process.ProcessName, DateTime.Now));
                current_window_info.update(window);
            }
            if (!current_window_info.WinTitle.Equals(window.Title))
            {
                current_window_info.update(window);
            }
            this.updateScore(theJudge.getCurrentScore());
            this.theLogger.DequeueActivity();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text.Equals("START"))
            {
                btnStart.Text = "PAUSE";
                IsTimerStarted = true;
                win_watcher_timer.Start();
                theJudge.StartJudge();
            }
            else
            {
                btnStart.Text = "START";
                IsTimerStarted = false;
                win_watcher_timer.Stop();
                theJudge.PauseJudge();
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
            theJudge.StopJudge();
        }

        private void ChangeSize(Size newSize)
        {
            picBox.Width = newSize.Width / 2;
            picBox.Height = newSize.Height / 2;
            this.Size = picBox.Size;

            pnlControls.Height = (int)Math.Round(picBox.Width * 0.1);
            pnlControls.Width = (int)Math.Round(picBox.Width * 0.8);
            btnStart.Width = (int)Math.Round(pnlControls.Width * 0.25);
            btnStart.Height = (int)Math.Round(pnlControls.Height * 0.7);
            btnStart.Left = (int)Math.Round(pnlControls.Width * 0.1);
            btnExit.Width = (int)Math.Round(pnlControls.Width * 0.25);
            btnExit.Height = (int)Math.Round(pnlControls.Height * 0.7);

            int a = pnlControls.Width / 2 - btnStart.Width - btnStart.Left;
            btnExit.Left = btnStart.Width + 2 * a;
            Point ptControlPanel = new Point();
            ptControlPanel.X = picBox.Width / 2 - pnlControls.Width / 2;
            ptControlPanel.Y = picBox.Height - pnlControls.Height - 10;
            pnlControls.Location = ptControlPanel;
        }
              

        private void showImage(double score)
        {
            if (picBox.Image != null)
            {
                Image oldImage = picBox.Image;
                picBox.Image = null;
                oldImage.Dispose();
            }

            pic_id = number_of_pics + 1 - (int)Math.Ceiling(score * number_of_pics);

            string path = Application.StartupPath+"//pics//" + pic_id + ".jpg";
            Image img = Image.FromFile(path);
            picBox.Image = img;
             
            if (currentSize.Height != img.Size.Height || currentSize.Width != img.Size.Width)
            {
                ChangeSize(img.Size);
            }
             
            pnlControls.InvalidateEx();
        }

        public void updateScore(double score)
        {
            showImage(score);
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
            

        private void countNumberOfFiles()
        {
            String path = Application.StartupPath + "//pics//";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            number_of_pics = dir.GetFiles().Length;
        }

        
        private void picBox_MouseHover(object sender, EventArgs e)
        {
            pnlControls.Visible = true;
        }

        /// <summary>
        ///   mouse leave the picture box
        /// </summary>
        private void picBox_MouseLeave(object sender, EventArgs e)
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

        /// <summary>
        ///   picture box painting 
        /// </summary>
        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            double score;
            if (!IsTimerStarted)
                score = 0.001;
            else
                score = Math.Round(theJudge.getCurrentScore(), 2);

            String str_score = "Score:" + score + " Pic:" + pic_id + ".jpg" +
                " Proc:" + current_window_info.ProcName +
                "\nWindow:" + current_window_info.WinTitle;
            using (Font myFont = new Font("Arial", 11))
            {
                e.Graphics.DrawString(str_score, myFont, Brushes.White, new Point(2, 2));
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



       
        
    }
}
