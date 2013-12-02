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
/// The View namespace.
/// </summary>
namespace WiredIn.Visualization.View
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using WiredIn.Globals;

    /// <summary>
    /// Class AbstractView
    /// </summary>
    public partial class AbstractView : UserControl
    {
        #region Fields

        /// <summary>
        /// The config
        /// </summary>
        protected ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The current state
        /// </summary>
        protected Globals.State curState = State.Bad;
        /// <summary>
        /// The view name
        /// </summary>
        protected string viewName;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractView" /> class.
        /// </summary>
        public AbstractView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.UserPaint |
                        ControlStyles.DoubleBuffer,
            true);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="s">The s.</param>
        public void ChangeState(Globals.State s)
        {
            curState = s;
        }

        /// <summary>
        /// Gets the size of the component.
        /// </summary>
        /// <returns>Size.</returns>
        /// <exception cref="System.InvalidOperationException">This method must be overridden</exception>
        public virtual Size GetComponentSize()
        {
            throw new InvalidOperationException("This method must be overridden");
        }

        /// <summary>
        /// Gets the count of steps.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public virtual int GetCountOfSteps()
        {
            return config.StandardSteps;
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.InvalidOperationException">This method must be overriden</exception>
        public virtual int GetScore()
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        /// <summary>
        /// Moves the view.
        /// </summary>
        /// <param name="goToGood">if set to <c>true</c> [go to good].</param>
        /// <exception cref="System.InvalidOperationException">This method must be overridden</exception>
        public virtual void MoveView(bool goToGood)
        {
            throw new InvalidOperationException("This method must be overridden");
        }

        /// <summary>
        /// Sets the direction.
        /// </summary>
        /// <param name="goToGood">if set to <c>true</c> [go to good].</param>
        /// <exception cref="System.InvalidOperationException">This method must be overridden</exception>
        public virtual void SetDirection(bool goToGood)
        {
            throw new InvalidOperationException("This method must be overridden");
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="n">The n.</param>
        public void SetName(string n)
        {
            this.viewName = n;
        }

        /// <summary>
        /// Sets the size.
        /// </summary>
        /// <param name="s">The s.</param>
        public virtual void SetSize(Size s)
        {
            this.Size = s;
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">This method must be overriden</exception>
        public virtual void SetUp()
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">This method must be overriden</exception>
        public virtual void TearDown()
        {
            throw new InvalidOperationException("This method must be overriden");
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Don't paint background
        }

        #endregion Methods
    }
}