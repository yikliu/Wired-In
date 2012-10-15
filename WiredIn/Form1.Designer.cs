using System.Windows.Forms;
using Wired_In;
using Gma.UserActivityMonitor;
using Wired_In.View;
namespace Wired_In
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
            this.win_watcher_timer = new System.Windows.Forms.Timer(this.components);
            this.mainFormContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sizesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalEventProvider1 = new Gma.UserActivityMonitor.GlobalEventProvider();
           
            this.pnlControls = new Wired_In.TransparentPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.mainFormContextMenu.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // win_watcher_timer
            // 
            this.win_watcher_timer.Interval = 1000;
            this.win_watcher_timer.Tick += new System.EventHandler(this.WindowsWatcherTimerTick);
            // 
            // mainFormContextMenu
            // 
            this.mainFormContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizesToolStripMenuItem});
            this.mainFormContextMenu.Name = "contextStrip";
            this.mainFormContextMenu.Size = new System.Drawing.Size(112, 28);
            // 
            // sizesToolStripMenuItem
            // 
            this.sizesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.fullScreenToolStripMenuItem});
            this.sizesToolStripMenuItem.Name = "sizesToolStripMenuItem";
            this.sizesToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.sizesToolStripMenuItem.Text = "Sizes";
            // 
            // smallToolStripMenuItem
            // 
            this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            this.smallToolStripMenuItem.Size = new System.Drawing.Size(147, 24);
            this.smallToolStripMenuItem.Text = "Small";
            this.smallToolStripMenuItem.Click += new System.EventHandler(this.smallToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(147, 24);
            this.mediumToolStripMenuItem.Text = "Medium";
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(147, 24);
            this.fullScreenToolStripMenuItem.Text = "FullScreen";
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
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
            this.myView.MouseHover += new System.EventHandler(myView_MouseHover);
            this.myView.MouseLeave += new System.EventHandler(myView_MouseLeave);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnExit);
            this.pnlControls.Controls.Add(this.btnStart);
            this.pnlControls.Location = new System.Drawing.Point(203, 255);
            this.pnlControls.Margin = new System.Windows.Forms.Padding(2);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(416, 62);
            this.pnlControls.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.CausesValidation = false;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(215, 12);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(116, 37);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.CausesValidation = false;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Location = new System.Drawing.Point(76, 12);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(125, 37);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlControls);
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
            

            this.mainFormContextMenu.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer win_watcher_timer;

        //private System.Windows.Forms.PictureBox picBox;
        private Button btnExit;
        private Button btnStart;
        private TransparentPanel pnlControls;
        private GlobalEventProvider globalEventProvider1;
        private ContextMenuStrip mainFormContextMenu;
        private ToolStripMenuItem sizesToolStripMenuItem;
        private ToolStripMenuItem smallToolStripMenuItem;
        private ToolStripMenuItem mediumToolStripMenuItem;
        private ToolStripMenuItem fullScreenToolStripMenuItem;
        private AbstractView myView;
       
        
    }
}

