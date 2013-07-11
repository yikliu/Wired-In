using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using WiredIn.TransitionCommand ;
using System.Windows.Forms;

namespace WiredIn.View
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AbstractView : UserControl
    {
        protected Size componentSize;
        protected Bitmap content;
        
        protected AbstractTransitionCommand transit;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Size GetComponentSize()
        {
            throw new InvalidOperationException("This method must be overridden");
        }

        public virtual AbstractTransitionCommand GetTransitCommand()
        {
            return this.transit;
        }

        public AbstractView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);           
        }

        public virtual void SetDirection(bool goToGood)
        {
            throw new InvalidOperationException("This method must be overridden");
        }

        public virtual int GetCountOfSteps()
        {
            return Constants.Config.STANDARD_STEPS;
        }

        public virtual void UpdateView(bool goToGood)
        {
            throw new InvalidOperationException("This method must be overridden");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual void SetSize(Size s) 
        { 
            this.Size = s; 
        }

        public virtual void SetUp()
        {
            transit.SetUp();
        }

        public virtual void TearDown() 
        {
            transit.TearDown();
        }

        public virtual void Pause() 
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Don't paint background
        }

        public virtual int GetScore()
        {
            throw new InvalidOperationException("This method must be overriden");
        }       
    }
}
