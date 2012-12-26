using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManagedWinapi.Windows;
//using Wired_In;
using WiredIn.Constants;

namespace WiredIn
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            switch (Constants.Config.APP_SIZE)
            {
                case app_size.full:
                    radioButton3.Checked = true;
                    break;
                case app_size.small:
                    radioButton1.Checked = true;
                    break;
                case app_size.medium:
                    radioButton2.Checked = true;
                    break;
            }
            switch (Constants.Config.VIS_IMAGE)
            {
                case imagery.flower:
                    radioButton4.Checked = true;
                    break;
                case imagery.progressbar:
                    radioButton5.Checked = true;
                    break;                
            }

            checkBox1.Checked = Config.TOPMOST;            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Config.APP_SIZE = app_size.small;
            }
            if (radioButton2.Checked)
            {
                Config.APP_SIZE = app_size.medium;
            }
            if (radioButton3.Checked)
            {
                Config.APP_SIZE = app_size.full;
            }
            if (radioButton4.Checked)
            {
                Config.VIS_IMAGE = imagery.flower;
            }
            if (radioButton5.Checked)
            {
                Config.VIS_IMAGE = imagery.progressbar;
            }

            Config.TOPMOST = checkBox1.Checked;
            Config.WHITE_WIN = whiteList.Items.OfType<String>().ToList();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void crosshair1_CrosshairDragged(object sender, EventArgs e)
        {
            update();
        }

        private void crosshair1_CrosshairDragging(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
            SystemWindow sw = SystemWindow.FromPointEx(MousePosition.X, MousePosition.Y, false, false);
            lbl_title.Text = sw.Title;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.whiteList.Items.Add(lbl_title.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selected = this.whiteList.SelectedIndices;
            foreach (int i in selected)
            {
                this.whiteList.Items.RemoveAt(i);
            }         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        


    }
}
