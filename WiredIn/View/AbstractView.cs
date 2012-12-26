using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiredIn.View
{
    public partial class AbstractView : UserControl
    {
        protected Size componentSize;
        protected Bitmap content;
        protected int numOfPics;
        protected int currentID = 0;     

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

        public virtual void setSize(Size s) 
        { 
            this.Size = s; 
        }

        public virtual void setUp()
        { }

        public virtual void tearDown() { }

        public virtual void pause() { }

        protected void countNumberOfFiles()
        {
            String path = Application.StartupPath + "//more_pics//";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            numOfPics = dir.GetFiles().Length;
        }
           

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Don't paint background
        }
    }
}
