using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;

using System.Windows.Forms;

using WiredIn.TransitionCommand;

namespace WiredIn.View
{
    public partial class ProgressBarView : AbstractView
    {
        int numOfSteps;
        int currentStep;
        public ProgressBarView()
        {
            InitializeComponent();

            this.transit = new ManyStepTransitCommand(this);

            numOfSteps = GetCountOfSteps();
            this.bar.Value = numOfSteps;
            currentStep = 2 * (numOfSteps / 3);       
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

        public override void SetUp() {}

        public override void TearDown() {}

        public override void Pause() {}

        protected override void OnPaintBackground(PaintEventArgs e)
        {
           this.BackColor = System.Drawing.SystemColors.ControlLightLight;
        }

        public override int GetScore()
        {
            return currentStep;
        }

        public override void UpdateView(bool g)
        {
            if (!g && currentStep >= 30)
            {
                currentStep--;                
            }
            else if (g && (currentStep != numOfSteps))
            {
                currentStep++;
            }
            if (numOfSteps != 0)
            {
                double r = (double)currentStep / (double)numOfSteps;
                this.bar.Value = (int)(r * numOfSteps);               
            }            
            this.bar.Invalidate();            
        }
    }
}
