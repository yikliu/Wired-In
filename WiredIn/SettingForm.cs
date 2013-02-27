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
        MainForm m_parent;
        public SettingForm(MainForm parent)
        {
            InitializeComponent();
            m_parent = parent;
            setUIValues();     
        }

        //set UI values according to global settings
        private void setUIValues()
        {
            switch (Constants.Config.APP_SIZE)
            {
                case app_size.full:
                    rbFullScreen.Checked = true;
                    break;
                case app_size.small:
                    rbSizeSmall.Checked = true;
                    break;
            }
            switch (Constants.Config.VIS_IMAGE)
            {
                case imagery.flower:
                    rdbFlower.Checked = true;
                    break;
                case imagery.progressbar:
                    rdbProgbar.Checked = true;
                    break;
            }

            switch (Constants.Config.OPERAND_CONDITION)
            {
                case operant_condition.reward:
                    rbReward.Checked = true;
                    break;
                case operant_condition.punish:
                    rbPunishment.Checked = true;
                    break;
            }
            cbTopMost.Checked = Config.TOPMOST;
        }

        private void button1_Click(object sender, EventArgs e)
        {           
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSizeSmall.Checked)
            {
                Config.APP_SIZE = app_size.small;
                m_parent.switchAppSize();
            }           
        }        

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFullScreen.Checked)
            {
                Config.APP_SIZE = app_size.full;
                m_parent.switchAppSize();
            }           
        }

        private void rbReward_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReward.Checked)
            {
                Config.OPERAND_CONDITION = operant_condition.reward;
            }
        }

        private void rbPunishment_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPunishment.Checked)
            {
                Config.OPERAND_CONDITION = operant_condition.punish;
            }
        }

        private void rdbProgbar_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProgbar.Checked)
            {
                Config.VIS_IMAGE = imagery.progressbar;
                m_parent.createView();
            }
        }

        private void rdbFlower_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFlower.Checked)
            {
                Config.VIS_IMAGE = imagery.flower;
                m_parent.createView();
            }
        }

    }
}
