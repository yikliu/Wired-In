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
        }

        public EmptyView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
             
        }

        public override void SetUp() {}

        public override void TearDown() {}

        public override void Pause() {}

        public override int GetScore()
        {
            return -1;
        }

        public override void UpdateView(bool g)
        {
                               
        }
    }
}
