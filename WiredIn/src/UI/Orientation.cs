// ***********************************************************************
// Assembly         : WiredIn
// Author           : yikliu
// Created          : 09-04-2013
//
// Last Modified By : yikliu
// Last Modified On : 09-05-2013
// ***********************************************************************
// <copyright file="Orientation.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WiredIn.Constants;
using WiredIn.UI.RadioButtonTile;
using ManagedWinapi.Windows;
using MetroFramework.Controls;

namespace WiredIn.UI
{
    /// <summary>
    /// Class Orientation
    /// </summary>
    public partial class Orientation : MetroForm
    {
        private List<string> visNames;
        private SingletonConstant constant = SingletonConstant.GetSingletonConstant();
        private ChosenVisualization visualizerCenter = new ChosenVisualization();

        private SystemWindow currentWindow;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation" /> class.
        /// </summary>
        public Orientation()
        {
            InitializeComponent();   
            tabControl.SelectedTab = tabpageTask;
            lblDate.Text = "Today is " + DateTime.Now.Date.ToString("d") + ",";

            RefreshVisualizationTiles();
        }

        private void RefreshVisualizationTiles()
        {
            String visualizationFolder = Path.Combine(constant.WiredInFolder, "visualizations");
            bool folder_exists = Directory.Exists(visualizationFolder);
            if (!folder_exists)
            {
                Directory.CreateDirectory(visualizationFolder);
            }
            else
            {
                visNames = new List<string>();
                foreach (string s in Directory.GetDirectories(visualizationFolder))
                {
                    visNames.Add(s.Remove(0, visualizationFolder.Length + 1));
                }
            }

            visNames.Add("progressbar");
            tableLayoutPanel.ColumnCount = visNames.Count;
            visualizerCenter.UnregisterAll();
            tableLayoutPanel.Controls.Clear();
            foreach (string name in visNames)
            {
                MyTile tile = new MyTile();
                tile.SetVisualizationCenter(visualizerCenter);
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

        /// <summary>
        /// Handles the CheckedChanged event of the metroRadioButton4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void metroRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (metroRadioButton4.Checked)
            {
                tbxOther.Visible = true;
            }
            else
            {
                tbxOther.Visible = false;
            }
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

        private void htmlLabel1_Click(object sender, EventArgs e)
        {
            Form import = new VisualizationOrganizer();
            if (import.ShowDialog() == DialogResult.OK)
            {
                RefreshVisualizationTiles();
            }
        }

        private void crosshair_CrosshairDragged(object sender, EventArgs e)
        {
            updateWindowTitleLabel();
        }

        private void updateWindowTitleLabel()
        {
            currentWindow = SystemWindow.FromPointEx(MousePosition.X, MousePosition.Y, false, false);
            label9.Text = currentWindow.Title.Substring(0, 20) + "...";
        }

        private void metroButtonAdd_Click(object sender, EventArgs e)
        {
            if (null != currentWindow && currentWindow.Title.Length > 0)
            {
                this.lbxWhiteList.Items.Add(currentWindow.Title);
            }            
        }

        private void metroButtonDelete_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selected = this.lbxWhiteList.SelectedIndices;
            foreach (int i in selected)
            {
                this.lbxWhiteList.Items.RemoveAt(i);
            }
        }

        private void crosshair_CrosshairDragging(object sender, EventArgs e)
        {
            updateWindowTitleLabel();
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name.Equals("tabWorkSphere"))
            {
                MetroPanel box = MakeTagBox();
                keywordsTable.Controls.Add(box, 0, 0);                
            }
        }

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
                    
                    if (row == 1 && col == 4)
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

        private void NullifyTextBox(MetroTextBox box)
        {
            box.Enabled = false;
            box.BackColor = System.Drawing.Color.AliceBlue;
            box.UseCustomBackColor = true;
            MetroButton del = (MetroButton)box.Parent.Controls[3];
            del.Visible = true;
            del.UseCustomBackColor = true;
            del.Enabled = true;
            del.BackColor = System.Drawing.Color.AliceBlue; ;
        }

        private MetroPanel MakeTagBox()
        {
            MetroPanel panel = new MetroPanel();
            panel.AutoSize = true;
            panel.Dock = DockStyle.Fill;

            MetroTextBox box = new MetroTextBox();

            //box.Anchor = AnchorStyles.Right;
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
                
        private void TagDelete_Click(object o, EventArgs e)
        {
            MetroButton del = (MetroButton)o;
            int row = keywordsTable.GetPositionFromControl(del.Parent).Row;
            int col = keywordsTable.GetPositionFromControl(del.Parent).Column;
            
            keywordsTable.Controls.Remove(del.Parent);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabpageFinish;
        }

        private void button4_Click(object sender, EventArgs e)
        {           
            //setup the visualization
            constant.ActiveVisualizationName = this.visualizerCenter.GetSelectedKey();
            switch (constant.ActiveVisualizationName)
            {
                case "rose":
                case "moon":
                    constant.ActiveView = Constants.Visualization.ManyStepImages;
                    break;
                case "progressbar":
                    constant.ActiveView = Constants.Visualization.Progressbar;
                    break;
                default:
                    constant.ActiveView = Constants.Visualization.Custom;
                    break;
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
