using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using WiredIn.TransitionCommand ;

namespace WiredIn.View
{
    /// <summary>
    /// Class ManyStepsView
    /// </summary>
    class ManyStepsView : AbstractView
    {        
        private string statusString = "bad";
        int numOfPics = 0;
        int currentID = 0;

        private double aspectRatio = 0.0;

        private string viewName;
        private string fileLocation;

        public ManyStepsView(String viewName)
        {
            this.viewName = viewName;
            this.fileLocation = Path.Combine(Constants.Config.WIREDIN_FOLDER, "visualizations\\" + viewName);
            transit = new ManyStepTransitCommand(this);
            numOfPics = GetCountOfSteps();

            ((ManyStepTransitCommand)transit).SpeedAdjustFactor = (double)Constants.Config.STANDARD_STEPS / numOfPics;

            currentID = 2 * (numOfPics / 3);  
        }

        /// <summary>
        /// Gets the count of steps.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int GetCountOfSteps()
        {            
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(fileLocation);
            return dir.GetFiles().Length;
        }

        public Image getImageByID(int id){                       
            string path = fileLocation + "\\"+id + ".jpg";
            Image img = Image.FromFile(path);
            this.aspectRatio = (double)img.Height / (double)img.Width;
            return img;
        }
         
        public void disposeImage(Bitmap p)
        {
            if (p != null)
            {
                p.Dispose();
            }
        }

        public static Bitmap CreateFastBitmap(Image src)
        {
            Bitmap dest = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);            
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.DrawImage(src, 0, 0, src.Width, src.Height);                
            }           
            return dest;
        }

        private delegate void RefreshViewDelegate();

        public void RefreshView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new RefreshViewDelegate(RefreshView));
            }
            else
            {
                disposeImage(this.content);
                this.content = CreateFastBitmap(getImageByID(currentID));
                this.Refresh();
            }
        }

        public override void SetUp()
        {
            base.SetUp();
         }

        public override void TearDown() 
        {
            base.TearDown();
            
        }

        public override void Pause()
        {

        }

        public override int GetScore()
        {
            return currentID;
        }

        public override void UpdateView(bool goToGood)
        {            
            if (!goToGood && currentID >= 30)
            {
                currentID--;
                statusString = "bad";                
            }else if(goToGood && (currentID < numOfPics - 1)){
                currentID++;
                statusString = "good";                
            }
            RefreshView();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(this.BackColor);

            if (this.content != null) //get the correct drawing coordinates, keep aspect ratio
            {
                int w = this.Size.Width;
                int h = (int)(this.Size.Width * this.aspectRatio);
                int start_h = (this.Size.Height - h) / 2;  
                e.Graphics.DrawImage(this.content, 0, start_h, w, h);
            }

            String str;
            if (Constants.Config.LabelImageNum)
            {                
                str = "Pic:" + currentID + "(" + statusString;
                if (Constants.Config.CONDITION == Constants.OperandCondition.punish)
                    str += ", punish)";
                else
                    str += ", reward)";

                using (Font myFont = new Font("Arial", 13))
                {
                    e.Graphics.DrawString(str, myFont, Brushes.Yellow, new Point(2, 2));
                }  
            }                     
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ManyStepsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Name = "ManyStepsView";
            this.ResumeLayout(false);
        }
    
    }
}
