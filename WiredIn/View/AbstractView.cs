using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wired_In.View
{
    public partial class AbstractView : UserControl
    {
        protected Size componentSize;
        protected Bitmap content;

        public virtual Size getComponentSize()
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        public AbstractView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);           
        }

        public virtual void updateView(bool goToGood)
        {
        }

        public virtual void setUp()
        { }

        public virtual void tearDown() { }

        public virtual void pause() { }

           

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Don't paint background
        }
    }
}
