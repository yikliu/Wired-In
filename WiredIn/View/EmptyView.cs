using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WiredIn.View
{
    public partial class EmptyView : AbstractView
    {
        public EmptyView()
        {
            InitializeComponent();
            countNumberOfFiles();
            currentID = 2 * (numOfPics / 3);     
        }

        public EmptyView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            
            countNumberOfFiles();
            currentID = 2 * (numOfPics / 3);     
        }

        public override void setUp() {}

        public override void tearDown() {}

        public override void pause() {}

        public override int getScore()
        {
            return currentID;
        }

        public override void updateView(bool g)
        {
            if (!g && currentID >= 30)
            {
                currentID--;                
            }
            else if (g && (currentID != numOfPics))
            {
                currentID++;
            }                   
        }
    }
}
