namespace WiredIn.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.ComponentModel;

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

        private ConfigVariables config = ConfigVariables.GetConfigVariables();
        private Worksphere worksphere = Worksphere.GetWorkSphere();
        private RunInstance runIns;
        private JSONLogger jLog;
        private List<string> visNames;

        private RadioButtonTileSubject visualizerCenter = new RadioButtonTileSubject();

        private List<WindowInfo> allWindows;
        private List<WindowInfo> whiteWindowInfos;

        private BackgroundWorker bgWorker;

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

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((CurrencyManager)lbxAllWindows.BindingContext[allWindows]).Refresh();
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            EnumAllWindows();
        }

        #endregion Constructors

        #region Methods

        public void btnGoToContext_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageEnv;
        }

        private void btnAddWindow_Click(object sender, EventArgs e)
        {
            IEnumerable<WindowInfo> list = this.lbxAllWindows.SelectedItems.Cast<WindowInfo>();
            whiteWindowInfos.AddRange(list);
            ((CurrencyManager)lbxAllWindows.BindingContext[whiteWindowInfos]).Refresh();
        }

        private void btnDelWindow_Click(object sender, EventArgs e)
        {
            WindowInfo wi = (WindowInfo)this.lbxWhiteWinList.SelectedItem;
            whiteWindowInfos.Remove(wi);
            ((CurrencyManager)lbxWhiteWinList.BindingContext[whiteWindowInfos]).Refresh();
        }

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
            foreach (WindowInfo w in whiteWindowInfos)
            {
                worksphere.AppendWhiteWindowInfo(w);
            }

            runIns.TaskDescription = tbxTaskDesc.Text;
            runIns.ExpectedDifficulty = tbDifficulty.Value;
            runIns.ExpectedFamilarity = tbFamiliarity.Value;
            runIns.NumOfToDoItems = (int)numOtherItems.Value;
            runIns.ExpectedTimeOnHour = (int)numExpectTimeOnTask.Value;
            if (tbxOther.Text.Length > 0)
                runIns.Location = tbxOther.Text;

            runIns.NoisyLevel = tbNoisy.Value;
            runIns.EstimatedBusiness = tbBusiness.Value;
            runIns.EstimatedEnergy = tbEnergy.Value;
            runIns.EstimatedStresslevel = tbStress.Value;

            LogRunIns(runIns);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LogRunIns(RunInstance ins)
        {
            string dir = Path.Combine(config.WiredInFolder, "log", RunIDKeeper.GetIDKeeper().GetRunID());
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string content = JsonConvert.SerializeObject(ins);
            string fileName = Path.Combine(dir, "run.json");
            jLog.LogThisToHere(content, fileName);
        }

        private void btnGoFinish_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageFinish;
        }

        private void btnGoToVisualization_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageVis;
        }

        private void btnGoToWorkSphere_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabWorkSphere;
        }

        private void htmlLabel1_Click(object sender, EventArgs e)
        {
            Form import = new VisualizationOrganizer();
            if (import.ShowDialog() == DialogResult.OK)
            {
                ShowVisualizationTiles();
            }
        }

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

        private void rbOffice_CheckedChanged(object sender, EventArgs e)
        {
            //pnlOfficeDetails.Visible = rbOffice.Checked;
            if (rbOffice.Checked)
            {
                //expand row
                tableLayoutPanel4.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel4.RowStyles[1].Height = tableLayoutPanel4.RowStyles[0].Height;
            }
            else
            {
                //collapse row
                tableLayoutPanel4.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanel4.RowStyles[1].Height = 0;
                rbSingleRoom.Checked = false;
                rbOpenOffice.Checked = false;
            }
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            tbxOther.Visible = rbOther.Checked;
        }

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

            visNames.Add("progressbar");
            tableLayoutPanel.ColumnCount = visNames.Count;
            visualizerCenter.UnregisterAll();
            tableLayoutPanel.Controls.Clear();
            foreach (string name in visNames)
            {
                MyTile tile = new MyTile();
                tile.SetTileSubject(visualizerCenter);
                tile.Name = name;
                tile.Register();
                tile.Text = name;
                tile.TileImage = global::WiredIn.Properties.Resources.selection;
                tile.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tableLayoutPanel.Controls.Add(tile);
            }

            foreach (Control c in tableLayoutPanel.Controls)
            {
                MyTile tile = (MyTile)c;
                tile.Width = 130;
                tile.Height = 130;
            }

            tableLayoutPanel.Width = 130 * tableLayoutPanel.ColumnCount;
            tableLayoutPanel.Left = (this.Width - tableLayoutPanel.Width) / 2;
            TableLayoutColumnStyleCollection styles = tableLayoutPanel.ColumnStyles;
            foreach (ColumnStyle style in styles)
            {
                style.SizeType = SizeType.AutoSize;
            }
        }

        private bool VisbleWindows(SystemWindow w)
        {
            if (string.Equals("Wired In", w.Title))
            {
                return false;
            }
            if ((w.ExtendedStyle & WindowExStyleFlags.TOOLWINDOW) != 0)
            {
                return false;
            }
            if (w.Visible && !string.IsNullOrEmpty(w.Title))
            {
                return true;
            }
            return false;
        }

        private void EnumAllWindows()
        {
            Predicate<SystemWindow> filterVisibleWnd = new Predicate<SystemWindow>(this.VisbleWindows);
            SystemWindow[] filtered = SystemWindow.FilterToplevelWindows(filterVisibleWnd);
            allWindows.Clear();
            foreach (SystemWindow sw in filtered)
            {
                allWindows.Add(new WindowInfo(sw));
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name.Equals("tabpageTask"))
            {
                lblDate.Text = "Today is " + DateTime.Now.Date.ToString("d");
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

        private void TagDelete_Click(object o, EventArgs e)
        {
            MetroButton del = (MetroButton)o;
            keywordsTable.Controls.Remove(del.Parent); //just remove it, the controls behind this one will tuck along
        }

        private void textBox_KeyDown(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MetroTextBox caller = (MetroTextBox)o;
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

        #endregion Methods

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageVis;
        }

        private void Orientation_Load(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        private void btnOpenWindowUpdate_Click(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        private void rbLocation_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                switch (rb.Name)
                {
                    case "rbHome":
                        runIns.Location = "Home";
                        break;
                    case "rbPublic":
                        runIns.Location = "Public";
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

    }
}