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
            throw new InvalidOperationException("This method must be overriden");
        }

        public virtual void setSize(Size s) 
        { 
            this.Size = s; 
        }

        public virtual void jump()
        {
            if (Constants.Config.OPERAND_CONDITION == Constants.operand_condition.reward && inBadRange())
            {
                jumpFromBadRange();
            }
            else if (Constants.Config.OPERAND_CONDITION == Constants.operand_condition.punish && inGoodRange())
            {
                jumpFromGoodRange();
            }            
        }

        public  bool inBadRange()
        {
            return currentID <= 450;
        }

        public  bool inGoodRange()
        {
            return currentID >= 800;
        }

        public  void jumpFromGoodRange()
        {
            if (inGoodRange())
                currentID = 700;
        }

        public  void jumpFromBadRange()
        {
            if (inBadRange())
                currentID = 600;
        }

        public virtual void setUp()
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        public virtual void tearDown() 
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        public virtual void pause() 
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        /// <summary>
        /// Keep this function in abstract class is to get the unified number of total images as 
        /// the full score for all different visualizations. So increment in different views will be comparable. 
        /// i.e. one image increment is equivilent of one progress bar increment
        /// </summary>
        protected void countNumberOfFiles()
        {
            String path = Application.StartupPath + "\\..\\pics\\";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            numOfPics = dir.GetFiles().Length;
        }
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Don't paint background
        }

        public virtual int getScore()
        {
            return -1;
        } 
       
        

    }
}
