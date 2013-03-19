using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WiredIn.View
{
    public partial class ProgressBarView : AbstractView
    {
        public static int full = 1000;
        public ProgressBarView()
        {
            InitializeComponent();
            this.bar.Value = full;
            countNumberOfFiles();
            currentID = numOfPics;       
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.bar == null)
            {
                return;
            }
            this.bar.Width = (int)(this.ClientSize.Width * 0.8);

            if (this.bar.Width < this.ClientSize.Width)
            {
                this.bar.Left = (this.ClientSize.Width - this.bar.Width) / 2;
            }
            if (this.bar.Height < this.ClientSize.Height)
            {
                this.bar.Top = (this.ClientSize.Height - this.bar.Height) / 2;
            }
        }

        public override void setUp() {}

        public override void tearDown() {}

        public override void pause() {}

        protected override void OnPaintBackground(PaintEventArgs e)
        {
           this.BackColor = System.Drawing.SystemColors.ControlLightLight;
        }

        public override void updateView(bool g)
        {
            if (!g && currentID != 0)
            {
                currentID--;                
            }
            else if (g && (currentID != numOfPics))
            {
                currentID++;
            }
            if (numOfPics != 0)
            {
                double r = (double)currentID / (double)numOfPics;
                this.bar.Value = (int)(r * full);               
            }            
            this.bar.Invalidate();            
        }       
    }
}
