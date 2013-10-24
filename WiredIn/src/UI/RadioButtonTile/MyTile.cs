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
#region Header

// ***********************************************************************
// Assembly         : WiredIn
// Author           : yikliu
// Created          : 09-05-2013
//
// Last Modified By : yikliu
// Last Modified On : 09-05-2013
// ***********************************************************************
// <copyright file="MyTile.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

#endregion Header

/// <summary>
/// The RadioButtonTile namespace.
/// </summary>
namespace WiredIn.UI.RadioButtonTile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Class MyTile
    /// </summary>
    public partial class MyTile : MetroFramework.Controls.MetroTile, Observer
    {
        #region Fields

        /// <summary>
        /// The selected
        /// </summary>
        public bool Selected = false;

        /// <summary>
        /// The tile subject
        /// </summary>
        private RadioButtonTileSubject tileSubject;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyTile" /> class.
        /// </summary>
        public MyTile()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.DarkCyan;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Registers this instance.
        /// </summary>
        public void Register()
        {
            tileSubject.Register(this.Name, this);
        }

        /// <summary>
        /// Sets the tile subject.
        /// </summary>
        /// <param name="v">The v.</param>
        public void SetTileSubject(RadioButtonTileSubject v)
        {
            if (null == tileSubject)
            {
                tileSubject = v;
            }
        }

        /// <summary>
        /// Updates the specified b.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        public void Update(bool b)
        {
            Selected = b;
            this.UseTileImage = Selected;
            this.UseCustomBackColor = Selected;
            this.Refresh();
        }

        /// <summary>
        /// Raises the <see cref="E:Click" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Selected = !Selected;
            this.UseTileImage = Selected;
            this.UseCustomBackColor = Selected;

            this.Refresh();
            if (Selected)
            {
                tileSubject.TileSelected(this.Name);
            }
        }

        #endregion Methods
    }
}