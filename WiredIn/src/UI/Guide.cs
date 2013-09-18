using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using ManagedWinapi.Windows;
using WiredIn.Constants;

namespace WiredIn.UI
{
    public partial class Guide : Form
    {
        private string customViewFolder;
        
        private List<string> customVisNames;

        private string selected_vis;

        private SystemWindow currentWindow;

        private int indexOfSelectedRadioButton;

        private SingletonConstant _constant = SingletonConstant.GetSingletonConstant();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Guide()
        {
            InitializeComponent();
            RefreshCustomVisualizations();
        }

        private void RefreshCustomVisualizations()
        {
            //get my document folder
            this.selected_vis = "rose";
            this.indexOfSelectedRadioButton = 0;

            customViewFolder = Path.Combine(_constant.WiredInFolder, "visualizations\\custom");
            bool folder_exists = Directory.Exists(customViewFolder);
            if (!folder_exists)
            {
                Directory.CreateDirectory(customViewFolder);
            }
            else
            {
                customVisNames = new List<string>();
                foreach (string s in Directory.GetDirectories(customViewFolder))
                {
                    customVisNames.Add(s.Remove(0, customViewFolder.Length + 1));
                    //Console.WriteLine(s.Remove(0, wired_in_folder.Length + 1));
                }                
            }

            tableLayoutPanel.ColumnCount = customVisNames.Count + 2;

            tableLayoutPanel.Controls.Clear();

            RadioButton rb_rose = new RadioButton();
            rb_rose.Name = "rose";
            rb_rose.Text = "Rose";
            rb_rose.Checked = true;
            rb_rose.CheckedChanged += new System.EventHandler(this.vis_changed);
            tableLayoutPanel.Controls.Add(rb_rose);

            RadioButton rb_moon = new RadioButton();
            rb_moon.Name = "moon";
            rb_moon.Text = "Moon";            
            rb_moon.CheckedChanged += new System.EventHandler(this.vis_changed);
            tableLayoutPanel.Controls.Add(rb_moon);

            RadioButton rb_pb = new RadioButton();
            rb_pb.Name = "progressbar";
            rb_pb.Text = "progressbar";
            rb_pb.Checked = false;
            rb_pb.CheckedChanged += new System.EventHandler(this.vis_changed);
            tableLayoutPanel.Controls.Add(rb_pb);

            AppendRadioButtons();

            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            TableLayoutColumnStyleCollection styles = tableLayoutPanel.ColumnStyles;
            foreach (ColumnStyle style in styles)
            {
                style.SizeType = SizeType.AutoSize;                
            }
        }

        private void AppendRadioButtons()
        {
            foreach (string name in customVisNames)
            {
                RadioButton rbtn = new RadioButton();
                rbtn.Name = name;
                rbtn.Text = name;
                rbtn.Checked = false;
                rbtn.CheckedChanged += new System.EventHandler(this.vis_changed);
                tableLayoutPanel.Controls.Add(rbtn);
            }
        }

        private void vis_changed(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                selected_vis = rb.Name;
                indexOfSelectedRadioButton = tableLayoutPanel.Controls.GetChildIndex(rb);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form import = new VisualizationOrganizer();
            if (import.ShowDialog() == DialogResult.OK)
            {
                RefreshCustomVisualizations();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>        
        public string GetSelectedVis()
        {
            return selected_vis;
        }

        public List<string> GetWorkSphereWindowTitles()
        {
            return lbxWhiteList.Items.OfType<string>().ToList();
        }

        public List<string> GetKeywords()
        {
            return listBox_keywords.Items.OfType<string>().ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _constant.WhiteListWinTitles.AddRange(GetWorkSphereWindowTitles());
            _constant.WhiteListKeyWords.AddRange(GetKeywords());

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void crosshair_CrosshairDragged(object sender, EventArgs e)
        {
            updateWindowTitleLabel();
        }

        private void crosshair_CrosshairDragging(object sender, EventArgs e)
        {
            //updateWindowTitleLabel();
        }

        private void updateWindowTitleLabel()
        {
            currentWindow = SystemWindow.FromPointEx(MousePosition.X, MousePosition.Y, false, false);
            lbl_title.Text = currentWindow.Title;           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            this.lbxWhiteList.Items.Add(lbl_title.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selected = this.lbxWhiteList.SelectedIndices;
            foreach (int i in selected)
            {
                this.lbxWhiteList.Items.RemoveAt(i);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (indexOfSelectedRadioButton < 3)
            {
                MessageBox.Show("Cannot delete preset visualizations", "My Application", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                //gBox_Vis.Controls.RemoveAt(indexOfSelectedRadioButton);
                String location = Path.Combine(_constant.WiredInFolder, "visualizations\\custom\\" + this.selected_vis);
                var dir = new DirectoryInfo(location);
                dir.Delete(true);
                RefreshCustomVisualizations();
            }
        }

        private void btnKW_Add_Click(object sender, EventArgs e)
        {
            if (tbox_keyword.Text.Length > 0)
            {
                listBox_keywords.Items.Add(tbox_keyword.Text);
                tbox_keyword.Text = "";
            }
        }

        private void btnKW_Del_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selected = this.listBox_keywords.SelectedIndices;
            foreach (int i in selected)
            {
                this.listBox_keywords.Items.RemoveAt(i);
            }
        }       
    }
}
