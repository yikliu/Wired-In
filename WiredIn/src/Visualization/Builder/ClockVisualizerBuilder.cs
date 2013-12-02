using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WiredIn.Visualization.Visualizer;
using WiredIn.Visualization.Transit;
using WiredIn.Visualization.View;

namespace WiredIn.Visualization.Builder
{
    class ClockVisualizerBuilder : AbstractBuilder
    {

        public override void BuildTransit()
        {
            this.visualizer.SetTransit(new ClockTransit());
        }

        public override void BuildView()
        {
            this.visualizer.SetView(new ClockView());
        }

        public override void CreateVisualizer()
        {
            this.visualizer = new ClockVisualizer();
        }
    }
}
