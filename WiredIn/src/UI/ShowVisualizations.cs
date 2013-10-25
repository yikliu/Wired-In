/**
 * WiredIn - Visual Reminder of Suspended Tasks
 *
 * The MIT License (MIT)
 * Copyright (c) 2012 Yikun Liu, https://github.com/yikliu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
#region Header

// ***********************************************************************
// Assembly         : WiredIn
// Author           : Yikun Liu
// Created          : 06-28-2013
//
// Last Modified By : Yikun Liu
// Last Modified On : 09-27-2013
// ***********************************************************************
// <summary>
// The main form.
//</summary>
// ***********************************************************************

#endregion Header

/// <summary>
/// The WiredIn namespace.
/// </summary>
namespace WiredIn
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    using WiredIn.Analyzer;
    using WiredIn.DataStructure;
    using WiredIn.Globals;
    using WiredIn.UserActivity;
    using WiredIn.Visualization.Builder;
    using WiredIn.Visualization.View;
    using WiredIn.Visualization.Visualizer;

    /// <summary>
    /// Main Form
    /// </summary>
    public partial class ShowVisualizations : Form
    {
        #region Fields

        //whether dragging is allowed
        /// <summary>
        /// The drag
        /// </summary>
        public bool drag = false;

        //Allowing dragging a windows
        /// <summary>
        /// The last click
        /// </summary>
        public Point lastClick;

        /// <summary>
        /// The builder
        /// </summary>
        private AbstractBuilder builder;

        /// <summary>
        /// The config
        /// </summary>
        private ConfigVariables config = ConfigVariables.GetConfigVariables();

        /// <summary>
        /// The orientation success
        /// </summary>
        private bool orientationSuccess = false;
        /// <summary>
        /// The visualizer
        /// </summary>
        private AbstractVisualizer visualizer;

        //An instance of background worker
        /// <summary>
        /// The worker
        /// </summary>
        private Worker worker;
        /// <summary>
        /// The worksphere
        /// </summary>
        private Worksphere worksphere = Worksphere.GetWorkSphere();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowVisualizations" /> class.
        /// </summary>
        public ShowVisualizations()
        {
            InitializeComponent();
            this.Hide();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Create the appropriate visualizer builder 
        /// </summary>
        public void CreateVisualizer()
        {
            switch (worksphere.ActiveView)
            {
                case Globals.Visualizer.Rose:
                    builder = new RoseVisualizerBuilder();
                    break;
                case Globals.Visualizer.Moon:
                    builder = new MoonVisualizerBuilder();
                    break;
                case Globals.Visualizer.Progressbar:
                    builder = new ProgressbarVisualizerBuilder();
                    break;
                case Globals.Visualizer.Empty:
                    builder = new EmptyVisualizerBuilder();
                    break;
                case Globals.Visualizer.Custom:
                    builder = new CustomVisualizerBuilder();
                    break;
            }

            builder.Build();
            visualizer = builder.GetVisualizer();

            this.Controls.Remove(this.myView);
            this.myView = visualizer.GetView();
            this.SuspendLayout();
            this.myView.ForeColor = System.Drawing.Color.Transparent;
            this.myView.Location = new System.Drawing.Point(0, 0);
            this.myView.Name = "myView";
            this.myView.Dock = DockStyle.Fill;
            this.myView.ContextMenuStrip = menu;

            this.Controls.Add(this.myView);
            SwitchViewSize();
            this.ResumeLayout();
        }

        /// <summary>
        /// Switches the size of the view.
        /// </summary>
        public void SwitchViewSize()
        {
            if (config.WindowSize == Globals.ApplicationSize.Small)
            {
                ShowOnRightBottom();
            }
            else
            {
                ShowOnFullScreenMonitor(1);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem start = (ToolStripMenuItem)this.menu.Items[0];
            if (!worker.IsRunning()) // not running
            {
                start.Text = "Stop";

                worker.Start();
                this.menu.Items[1].Enabled = false;
                worker.EnqueueActivity(new StartUp(DateTime.Now, myView.GetScore()));
            }
            else //is running
            {
                start.Text = "Start";
                this.menu.Items[1].Enabled = true;
                worker.Stop();
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_exit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_exit_Click(object sender, EventArgs e)
        {
            worker.TearDown();
            Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the smallToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOnRightBottom();
        }

        /// <summary>
        /// Handles the Click event of the fullScreenToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config.WindowSize = Globals.ApplicationSize.Full;
            SwitchViewSize();
        }

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myView != null && worker != null)
            {
                worker.EnqueueActivity(new ShutDown(DateTime.Now, myView.GetScore()));
                worker.Stop();
                worker.TearDown();
            }
        }

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            WiredIn.UI.Orientation orientation = new WiredIn.UI.Orientation();
            orientationSuccess = (orientation.ShowDialog() == DialogResult.OK);

            if (orientationSuccess)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                CreateVisualizer();
                worker = new Worker(visualizer);
                worker.SetUp();
                this.Show();

            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            lastClick = e.Location;
        }

        /// <summary>
        /// Handles the MouseMove event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag && e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        /// <summary>
        /// Shows the on full screen monitor.
        /// </summary>
        /// <param name="monitor">The monitor.</param>
        private void ShowOnFullScreenMonitor(int monitor)
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
        private void ShowOnRightBottom()
        {
            Screen s = Screen.PrimaryScreen;
            int het = s.WorkingArea.Height / config.ShrinkFactor;
            int wth = s.WorkingArea.Width / config.ShrinkFactor;

            this.Size = new Size(wth, het);
            this.Left = s.WorkingArea.Right - wth;
            this.Top = s.WorkingArea.Bottom - het;
            this.StartPosition = FormStartPosition.Manual;

            this.TopMost = true;

            this.Location = new Point(this.Left, this.Top);
            this.WindowState = FormWindowState.Normal;
            this.myView.SetSize(this.Size);
        }

        #endregion Methods

       
    }
}