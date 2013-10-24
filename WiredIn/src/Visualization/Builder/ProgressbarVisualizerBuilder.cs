/**
 * WiredIn - Visual Reminder of Suspended Tasks
 *
 * The MIT License (MIT)
 * Copyright (c) 2012 Yikun Liu, https://github.com/yikliu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
/// <summary>
/// The Builder namespace.
/// </summary>
namespace WiredIn.Visualization.Builder
{
    using WiredIn.Visualization.Transit;
    using WiredIn.Visualization.View;
    using WiredIn.Visualization.Visualizer;

    /// <summary>
    /// Class ProgressbarVisualizerBuilder.
    /// </summary>
    class ProgressbarVisualizerBuilder : AbstractBuilder
    {
        #region Methods

        /// <summary>
        /// Builds the transit.
        /// </summary>
        public override void BuildTransit()
        {
            visualizer.SetTransit(new ManyStepTransit());
        }

        /// <summary>
        /// Builds the view.
        /// </summary>
        public override void BuildView()
        {
            visualizer.SetView(new ProgressBarView());
        }

        /// <summary>
        /// Creates the visualizer.
        /// </summary>
        public override void CreateVisualizer()
        {
            this.visualizer = new ProgressbarVisualizer();
        }

        #endregion Methods
    }
}