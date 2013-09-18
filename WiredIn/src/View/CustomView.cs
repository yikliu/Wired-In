using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using WiredIn.TransitionCommand;
using WiredIn.Constants;

namespace WiredIn.View
{
    public partial class CustomView : AbstractView
    {
        private string name;
        private string imageFolder;
        private Dictionary<int, Image> images;
        private int currentId = 1;

        private SingletonConstant _constant = SingletonConstant.GetSingletonConstant();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CustomView()
        {
            InitializeComponent();
            transit = new FewStepTransitCommand(this);
        }

        /// <summary>
        /// CustomView Constructor
        /// </summary>
        /// <param name="name">name of the custom vis</param> 
        public CustomView(string name) : this()
        {
            this.name = name;

            imageFolder = Path.Combine(_constant.WiredInFolder, "visualizations\\" + name);
            
            if (Directory.Exists(imageFolder))
            {
                string[] files = Directory.GetFiles(imageFolder);
                int count = files.Length;
                if (count != 3)
                {
                    throw new Exception("Not 3 images found");
                }

                images = new Dictionary<int, Image>();

                foreach (string file in files)
                {
                    //Console.WriteLine(Path.GetFileName(file));
                    Image img = Image.FromFile(file);
                    int id = Int32.Parse( Path.GetFileNameWithoutExtension(file).Substring(5));                    
                    images.Add(id, img);
                }
                pictureBox1.Image = images[currentId];
            }
        }

        public override Size GetComponentSize()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void UpdateView(bool goToGood)
        {
            int newId = -1;
            if (goToGood)
            {
                newId = currentId + 1 > 2 ? 2 : currentId + 1;
            }
            else
            {
                newId = currentId - 1 < 0 ? 0 : currentId - 1;
            }
            if (newId != currentId && newId >= 0)
            {
                currentId = newId;
                pictureBox1.Image = images[currentId];
            }
        }

        public override void SetSize(Size s)
        {
            this.Size = s;
        }

        public override void SetUp()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void TearDown()
        {
            
        }

        public override void Pause()
        {
            throw new Exception("The method or operation is not implemented.");
        }

       
        public override int GetScore()
        {
            return 0;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {            
            using (Font myFont = new Font("Arial", 14))
            {
                e.Graphics.DrawString(currentId.ToString(), myFont, Brushes.Green, new Point(2, 2));
            }
        }
    }
}
