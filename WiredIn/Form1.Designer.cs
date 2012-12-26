using System.Windows.Forms;
using WiredIn;
using Gma.UserActivityMonitor;
using WiredIn.View;
namespace WiredIn
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private bool draggable = true;
        private string exclude_list = "";

        public string ExcludeList
        {
            set
            {
                this.exclude_list = value;
            }
            get
            {
                return this.exclude_list.Trim();
            }
        }

        public bool Draggable
        {
            set
            {
                this.draggable = value;
            }
            get
            {
                return this.draggable;
            }
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Overriden Functions

        protected override void OnControlAdded(ControlEventArgs e)
        {
            //
            //Add Mouse Event Handlers for each control added into the form,
            //if Draggable property of the form is set to true and the control
            //name is not in the ExcludeList.Exclude list is the comma separated
            //list of the Controls for which you do not require the mouse handler 
            //to be added. For Example a button.  
            //
            if (this.Draggable && (this.ExcludeList.IndexOf(e.Control.Name) == -1))
            {
                e.Control.MouseDown += new MouseEventHandler(MainForm_MouseDown);
                e.Control.MouseUp += new MouseEventHandler(MainForm_MouseUp);
                e.Control.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            }
            base.OnControlAdded(e);
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.winWatchTimer = new System.Windows.Forms.Timer(this.components);
          
            this.globalEventProvider1 = new Gma.UserActivityMonitor.GlobalEventProvider();          

            this.menu = new ContextMenuStrip();         
          
            this.SuspendLayout();
            // 
            // win_watcher_timer
            // 
            this.winWatchTimer.Interval = 1000;
            this.winWatchTimer.Tick += new System.EventHandler(this.WindowsWatcherTimerTick);
            
            // 
            // globalEventProvider1
            // 
            this.globalEventProvider1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider_MouseUp);
            this.globalEventProvider1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.globalEventProvider_KeyUp);
            // 
            // myView
            // 
            this.myView.ForeColor = System.Drawing.Color.Transparent;
            this.myView.Location = new System.Drawing.Point(0, 0);
            this.myView.Name = "myView";
            this.myView.Size = new System.Drawing.Size(800, 600);
            this.myView.TabIndex = 7;
            this.myView.Load += new System.EventHandler(this.myView_Load);
            //this.myView.MouseHover += new System.EventHandler(myView_MouseHover);
            //this.myView.MouseLeave += new System.EventHandler(myView_MouseLeave);
            this.myView.Dock = DockStyle.Fill;

            ToolStripMenuItem mnuStart = new ToolStripMenuItem("Start");
            ToolStripMenuItem mnuExit = new ToolStripMenuItem("Exit");
            ToolStripMenuItem mnuSetting = new ToolStripMenuItem("Settings");
            //Assign event handlers
            mnuStart.Click += new System.EventHandler(this.btnStart_Click);
            mnuExit.Click += new System.EventHandler(this.btn_exit_Click);
            mnuSetting.Click += new System.EventHandler(this.btnSetting_Click);
            //Add to main context menu
            this.menu.Items.AddRange(new ToolStripItem[] { mnuStart, mnuExit, mnuSetting });
            
            myView.ContextMenuStrip = menu;
            
            

            

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(800, 600);
           
            this.Controls.Add(this.myView);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainFrame";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
           
          
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer winWatchTimer;

        //private System.Windows.Forms.PictureBox picBox;
      
       
        private ContextMenuStrip menu;
        private GlobalEventProvider globalEventProvider1;        
        private AbstractView myView;
       
        
    }
}

