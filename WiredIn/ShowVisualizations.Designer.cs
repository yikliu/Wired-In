using System.Windows.Forms;
using WiredIn;
using Gma.UserActivityMonitor;
using WiredIn.View;
namespace WiredIn
{
    partial class ShowVisualizations
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
            this.globalEventProvider = new Gma.UserActivityMonitor.GlobalEventProvider();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.importImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // winWatchTimer
            // 
            this.winWatchTimer.Interval = 1000;
            this.winWatchTimer.Tick += new System.EventHandler(this.WindowsWatcherTimerTick);
           // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStart,
            this.mnuExit,
            this.mnuSetting,
            this.importImagesToolStripMenuItem});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(174, 114);
            // 
            // mnuStart
            // 
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.Size = new System.Drawing.Size(173, 22);
            this.mnuStart.Text = "Start";
            this.mnuStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(173, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // mnuSetting
            // 
            this.mnuSetting.Name = "mnuSetting";
            this.mnuSetting.Size = new System.Drawing.Size(173, 22);
            this.mnuSetting.Text = "Settings";
            this.mnuSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // importImagesToolStripMenuItem
            // 
            this.importImagesToolStripMenuItem.Name = "importImagesToolStripMenuItem";
            this.importImagesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.importImagesToolStripMenuItem.Text = "Import Images";
            this.importImagesToolStripMenuItem.Click += new System.EventHandler(this.importImagesToolStripMenuItem_Click);
            // 
            // ShowVisualizations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowVisualizations";
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
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer winWatchTimer;

        private ContextMenuStrip menu;
        private GlobalEventProvider globalEventProvider;        
        private AbstractView myView;
        private ToolStripMenuItem mnuStart;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuSetting;
        private ToolStripMenuItem importImagesToolStripMenuItem;       
    }
}

