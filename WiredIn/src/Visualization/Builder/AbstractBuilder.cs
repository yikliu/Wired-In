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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WiredIn.Analyzer;
    using WiredIn.Visualization.Transit;
    using WiredIn.Visualization.View;
    using WiredIn.Visualization.Visualizer;

    /// <summary>
    /// Class AbstractBuilder.
    /// </summary>
    abstract class AbstractBuilder
    {
        #region Fields

        /// <summary>
        /// The visualizer
        /// </summary>
        protected AbstractVisualizer visualizer;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Builds this instance.
        /// </summary>
        public void Build()
        {
            CreateVisualizer();
            BuildView();
            BuildTransit();
        }

        /// <summary>
        /// Builds the transit.
        /// </summary>
        public abstract void BuildTransit();

        /// <summary>
        /// Builds the view.
        /// </summary>
        public abstract void BuildView();

        /// <summary>
        /// Creates the visualizer.
        /// </summary>
        public abstract void CreateVisualizer();

        /// <summary>
        /// Gets the visualizer.
        /// </summary>
        /// <returns>AbstractVisualizer.</returns>
        /// <exception cref="System.Exception">Visualizer Not Built</exception>
        public virtual AbstractVisualizer GetVisualizer()
        {
            if (null == visualizer || null == visualizer.GetTransit() || null == visualizer.GetView())
            {
                throw new Exception("Visualizer Not Built");
            }

            return this.visualizer;
        }

        #endregion Methods
    }
}