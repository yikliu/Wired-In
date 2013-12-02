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
/// <summary>
/// The UI namespace.
/// </summary>
namespace WiredIn.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using ManagedWinapi.Windows;

    using MetroFramework.Controls;
    using MetroFramework.Forms;

    using Newtonsoft.Json;

    using WiredIn.DataStructure;
    using WiredIn.Globals;
    using WiredIn.Log;
    using WiredIn.UI.RadioButtonTile;

    /// <summary>
    /// Class Orientation
    /// </summary>
    public partial class Orientation : MetroForm
    {
        #region Fields

        /// <summary>
        /// All windows
        /// </summary>
        private List<WindowInfo> allWindows;
        /// <summary>
        /// The bg worker
        /// </summary>
        private BackgroundWorker bgWorker;
        /// <summary>
        /// The configuration
        /// </summary>
        private ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The j log
        /// </summary>
        private JSONLogger jLog;
        /// <summary>
        /// The run ins
        /// </summary>
        private RunInstance runIns;
        /// <summary>
        /// The vis names
        /// </summary>
        private List<string> visNames;
        /// <summary>
        /// The visualizer center
        /// </summary>
        private RadioButtonTileSubject visualizerCenter = new RadioButtonTileSubject();
        /// <summary>
        /// The white window infos
        /// </summary>
        private List<WindowInfo> whiteWindowInfos;
        /// <summary>
        /// The worksphere
        /// </summary>
        private Worksphere worksphere = Worksphere.GetWorkSphere();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation" /> class.
        /// </summary>
        public Orientation()
        {
            InitializeComponent();
            allWindows = new List<WindowInfo>();
            whiteWindowInfos = new List<WindowInfo>();

            lbxAllWindows.DataSource = allWindows;
            lbxWhiteWinList.DataSource = whiteWindowInfos;

            runIns = new RunInstance();
            jLog = new JSONLogger();

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            tabControl.SelectedTab = tabpageTask;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the Click event of the btnGoToContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void btnGoToContext_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageEnv;
        }

        /// <summary>
        /// Handles the DoWork event of the bgWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            EnumAllWindows();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((CurrencyManager)lbxAllWindows.BindingContext[allWindows]).Refresh();
        }

        /// <summary>
        /// Handles the Click event of the btnAddWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddWindow_Click(object sender, EventArgs e)
        {
            IEnumerable<WindowInfo> list = this.lbxAllWindows.SelectedItems.Cast<WindowInfo>();
            whiteWindowInfos.AddRange(list);
            ((CurrencyManager)lbxAllWindows.BindingContext[whiteWindowInfos]).Refresh();
        }

        /// <summary>
        /// Handles the Click event of the btnDelWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelWindow_Click(object sender, EventArgs e)
        {
            WindowInfo wi = (WindowInfo) this.lbxWhiteWinList.SelectedItem;
            whiteWindowInfos.Remove(wi);
            ((CurrencyManager)lbxWhiteWinList.BindingContext[whiteWindowInfos]).Refresh();
        }

        /// <summary>
        /// BTNs the finalize orientation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFinalizeOrientation(object sender, EventArgs e)
        {
            //setup the visualization
               worksphere.CustomVisualizerName = this.visualizerCenter.GetSelectedKey();
               switch (worksphere.CustomVisualizerName)
            {
                case "rose":
                    worksphere.ActiveView = Visualizer.Rose;
                    break;
                case "moon":
                    worksphere.ActiveView = Visualizer.Moon;
                    break;
                case "progressbar":
                    worksphere.ActiveView = Visualizer.Progressbar;
                    break;
                case "mirror":
                    worksphere.ActiveView = Visualizer.Mirror;
                    break;
                case "clock":
                       worksphere.ActiveView = Visualizer.Clock;
                       break;
                default:
                    worksphere.ActiveView = Visualizer.Custom;
                    break;
            }

            //setup worksphere
            worksphere.ClearAll(); //clear first

            //store keywords
            foreach (Control c in keywordsTable.Controls)
            {
                string word = ((MetroTextBox)c.Controls[2]).Text;
                worksphere.AddKeyword(word.ToLowerInvariant());
            }

            //store titles;
            foreach(WindowInfo w in whiteWindowInfos)
            {
                worksphere.AppendWhiteWindowInfo(w);
            }

            runIns.PrimaryTaskTitles = worksphere.AcceptableWindowTitles;
            runIns.PrimaryTaskKeywords = worksphere.AcceptableKeywords.ToList();
            runIns.PrimaryTaskProcesses = worksphere.AcceptableProcNames.ToList();

            runIns.TaskDescription = tbxTaskDesc.Text;
            runIns.ExpectedDifficulty = tbDifficulty.Value;
            runIns.ExpectedUrgency = tbUrgency.Value;
            runIns.ExpectedFamilarity = tbFamiliarity.Value;
            runIns.NumOfToDoItems = (int) numOtherItems.Value;
            runIns.ExpectedTimeOnHour = (int)numExpectTimeOnTask.Value;
            if (tbxOther.Text.Length > 0)
                runIns.Location = tbxOther.Text;

            runIns.NoisyLevel = tbNoisy.Value;
            runIns.EstimatedBusiness = tbBusiness.Value;
            runIns.EstimatedEnergy = tbEnergy.Value;
            runIns.EstimatedStresslevel = tbStress.Value;
            runIns.EstimatedMotivation = tbMotivation.Value;

            runIns.ChosenVisualization = worksphere.ActiveView.ToString();

            LogRunIns(runIns);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnGoFinish control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnGoFinish_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageFinish;
        }

        /// <summary>
        /// Handles the Click event of the btnGoToVisualzation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnGoToVisualzation_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageVis;
        }

        /// <summary>
        /// Handles the Click event of the btnGoToWorkSphere control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnGoToWorkSphere_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabWorkSphere;
        }

        /// <summary>
        /// Handles the Click event of the btnOpenWindowUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOpenWindowUpdate_Click(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageVis;
        }

        /// <summary>
        /// Enums all windows.
        /// </summary>
        private void EnumAllWindows()
        {
            Predicate<SystemWindow> filterVisibleWnd = new Predicate<SystemWindow>(this.VisbleWindows);
            SystemWindow[] filtered =  SystemWindow.FilterToplevelWindows(filterVisibleWnd);
            allWindows.Clear();
            foreach (SystemWindow sw in filtered)
            {
                allWindows.Add(new WindowInfo(sw));
            }
        }

        /// <summary>
        /// Handles the Click event of the htmlLabel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void htmlLabel1_Click(object sender, EventArgs e)
        {
            Form import = new VisualizationOrganizer();
            if (import.ShowDialog() == DialogResult.OK)
            {
                ShowVisualizationTiles();
            }
        }

        /// <summary>
        /// Logs the run ins.
        /// </summary>
        /// <param name="ins">The ins.</param>
        private void LogRunIns(RunInstance ins)
        {
            string dir = Path.Combine(config.WiredInFolder, "log");
            dir = Path.Combine(dir, RunIDKeeper.GetIDKeeper().GetRunID());
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string content = JsonConvert.SerializeObject(ins);
            string fileName = Path.Combine(dir, "run.json");
            jLog.LogThisToHere(content, fileName);
        }

        /// <summary>
        /// Makes the tag box.
        /// </summary>
        /// <returns>MetroPanel.</returns>
        private MetroPanel MakeTagBox()
        {
            MetroPanel panel = new MetroPanel();
            panel.AutoSize = true;
            panel.Dock = DockStyle.Fill;

            MetroTextBox box = new MetroTextBox();
            box.Dock = DockStyle.Fill;
            box.TextAlign = HorizontalAlignment.Center;
            box.Location = new System.Drawing.Point(5, 5);
            box.PromptText = "+";
            box.KeyDown += new KeyEventHandler(textBox_KeyDown);

            panel.Controls.Add(box);

            MetroButton deleteBtn = new MetroButton();
            deleteBtn.Text = "x";
            deleteBtn.Anchor = AnchorStyles.Right;
            deleteBtn.AutoSize = true;
            deleteBtn.Width = 20;
            deleteBtn.Height = 30;
            deleteBtn.Visible = false;
            deleteBtn.Enabled = false;
            deleteBtn.Dock = DockStyle.Right;
            deleteBtn.Click += new System.EventHandler(this.TagDelete_Click);
            panel.Controls.Add(deleteBtn);
            return panel;
        }

        /// <summary>
        /// Nullifies the text box.
        /// </summary>
        /// <param name="box">The box.</param>
        private void NullifyTextBox(MetroTextBox box)
        {
            box.Enabled = false;
            box.BackColor = System.Drawing.Color.AliceBlue;
            box.UseCustomBackColor = true;
            MetroButton del = (MetroButton)box.Parent.Controls[3];
            del.Visible = true;
            del.UseCustomBackColor = true;
            del.Enabled = true;
            del.BackColor = System.Drawing.Color.AliceBlue;
        }

        /// <summary>
        /// Handles the Load event of the Orientation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Orientation_Load(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rbLocation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rbLocation_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                switch (rb.Name)
                {
                    case "rbHome" :
                        runIns.Location = "Home";
                        break;
                    case "rbPublic":
                        runIns.Location = "Pulic ";
                        break;
                    case "rbOpenOffice":
                        runIns.Location = "Open Office";
                        break;
                    case "rbSingleRoom":
                        runIns.Location = "Single Office Room";
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rbOffice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rbOffice_CheckedChanged(object sender, EventArgs e)
        {
            pnlOfficeDetails.Visible = rbOffice.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rbOther control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            tbxOther.Visible = rbOther.Checked;
        }

        /// <summary>
        /// Shows the visualization tiles.
        /// </summary>
        private void ShowVisualizationTiles()
        {
            String visualizationFolder = Path.Combine(config.WiredInFolder, "visualizations");
            bool folder_exists = Directory.Exists(visualizationFolder);
            if (!folder_exists)
            {
                Directory.CreateDirectory(visualizationFolder);
            }

            visNames = new List<string>();
            foreach (string s in Directory.GetDirectories(visualizationFolder))
            {
                visNames.Add(s.Remove(0, visualizationFolder.Length + 1));
            }

            visNames.AddRange(config.vendorVisualizerNames);
            
            visualizerCenter.UnregisterAll();
            tableLayoutPanel1.Controls.Clear();
            foreach (string name in visNames)
            {
                MyTile tile = new MyTile();
                tile.SetTileSubject(visualizerCenter);
                tile.Name = name;
                tile.Register();
                tile.Text = name;
                tile.TileImage = global::WiredIn.Properties.Resources.selection;
                tile.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(tile);
            }

            foreach (Control c in tableLayoutPanel1.Controls)
            {
                MyTile tile = (MyTile)c;
                tile.Width = 130;
                tile.Height = 130;
            }
        }

        /// <summary>
        /// Handles the Selected event of the tabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TabControlEventArgs"/> instance containing the event data.</param>
        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if(e.TabPage.Name.Equals("tabpageTask"))
            {
                lblDate.Text = "Today is " + DateTime.Now.Date.ToString("d") + ",";
            }
            if (e.TabPage.Name.Equals("tabWorkSphere"))
            {
                if (0 == keywordsTable.Controls.Count)
                {
                    MetroPanel box = MakeTagBox();
                    keywordsTable.Controls.Add(box);
                }
            }
            if (e.TabPage.Name.Equals("tabpageVis"))
            {
                ShowVisualizationTiles();
            }
        }

        /// <summary>
        /// Handles the Click event of the TagDelete control.
        /// </summary>
        /// <param name="o">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TagDelete_Click(object o, EventArgs e)
        {
            MetroButton del = (MetroButton) o;
            keywordsTable.Controls.Remove(del.Parent); //just remove it, the controls behind this one will tuck along
        }

        /// <summary>
        /// Handles the KeyDown event of the textBox control.
        /// </summary>
        /// <param name="o">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void textBox_KeyDown(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MetroTextBox caller = (MetroTextBox) o;
                if (caller.Text.Length > 0)
                {
                    NullifyTextBox(caller);

                    int row = keywordsTable.GetPositionFromControl(caller.Parent).Row;
                    int col = keywordsTable.GetPositionFromControl(caller.Parent).Column;

                    if (row == 1 && col == 4) //end
                    {
                        e.Handled = true; //avoid the ding sound
                        e.SuppressKeyPress = true; // avoid the ding sound
                        return;
                    }

                    MetroPanel newPanel = MakeTagBox();
                    keywordsTable.Controls.Add(newPanel);
                    keywordsTable.RowStyles.Clear();
                    keywordsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                    foreach (RowStyle style in keywordsTable.RowStyles)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = 35;
                    }
                }
                e.Handled = true; //avoid the ding sound
                e.SuppressKeyPress = true; // avoid the ding sound
            }
        }

        /// <summary>
        /// Visbles the windows.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool VisbleWindows(SystemWindow w)
        {
            if (string.Equals("Wired In", w.Title))
            {
                return false;
            }
            if((w.ExtendedStyle & WindowExStyleFlags.TOOLWINDOW) != 0)
            {
                return false;
            }
            if (w.Visible && !string.IsNullOrEmpty(w.Title))
            {
                return true;
            }
            return false;
        }

        #endregion Methods
    }
}