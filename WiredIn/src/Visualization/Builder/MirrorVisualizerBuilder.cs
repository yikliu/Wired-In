using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiredIn.Visualization.Visualizer;
using WiredIn.Visualization.Transit;
using WiredIn.Visualization.View;

namespace WiredIn.Visualization.Builder
{
    class MirrorVisualizerBuilder : AbstractBuilder
    {

        public override void BuildTransit()
        {
            if (null != this.visualizer)
                this.visualizer.SetTransit(new MirrorTransit());
        }

        public override void BuildView()
        {
            if (null != this.visualizer)
                this.visualizer.SetView(new MirrorView());
        }

        public override void CreateVisualizer()
        {
            this.visualizer = new MirrorVisualizer();
        }
    }
}
