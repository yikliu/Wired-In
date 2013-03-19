using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WiredIn.View
{
    class ImageView : AbstractView
    {        
        private System.ComponentModel.IContainer components;
        
        private string good = "bad";

        private double aspectRatio = 0.0;

        public ImageView()
        {            
            countNumberOfFiles();
            if (Constants.Config.OPERAND_CONDITION == Constants.operant_condition.punish)
            {
                currentID = numOfPics - 2; // when punish THe last picture is best one
            }
            else
            {
                currentID = numOfPics / 2; // when reward initial pic was chosen as middle one.
            }            
        }
    
        public Image getImageByID(int id){                       
            string path = Application.StartupPath + "//more_pics//" + id + ".jpg";
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

        public override void setUp() { }

        public override void tearDown() { }

        public override void pause() { }

       
        public override void updateView(bool goToGood)
        {            
            if (!goToGood && currentID >= 30)
            {
                currentID--;
                good = "bad";                
            }else if(goToGood && (currentID < numOfPics - 1)){
                currentID++;
                good = "good";                
            }
            RefreshView();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(this.BackColor);
            if (this.content != null)
            {
                int w = this.Size.Width;
                int h = (int)(this.Size.Width * this.aspectRatio);
                int start_h = (this.Size.Height - h) / 2;  
                e.Graphics.DrawImage(this.content, 0, start_h, w, h);
            }

            String str;
            if (Constants.Config.LabelImageNum)
            {
                
                str = "Pic:" + currentID + "(" + good + ")";
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
            // ImageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Name = "ImageView";
            this.ResumeLayout(false);
        }
               
    }
}
