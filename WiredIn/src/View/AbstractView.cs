using System;
using System.Drawing;
using System.Windows.Forms;
using WiredIn.Constants;
using WiredIn.TransitionCommand;

namespace WiredIn.View
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AbstractView : UserControl
    {
        protected Size componentSize;
        protected Bitmap content;

        private SingletonConstant _constant = SingletonConstant.GetSingletonConstant();
        
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
            return _constant.StandardSteps;
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
