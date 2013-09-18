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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiredIn.UI.RadioButtonTile
{
    /// <summary>
    /// Class MyTile
    /// </summary>
    public partial class MyTile : MetroFramework.Controls.MetroTile, Observer
    {
        /// <summary>
        /// The selected
        /// </summary>
        public bool Selected = false;

        private ChosenVisualization visCenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyTile"/> class.
        /// </summary>
        public MyTile()
        {
            InitializeComponent();                      
        }

        public void SetVisualizationCenter(ChosenVisualization v)
        {
            if (null == visCenter)
            {
                visCenter = v;
            }            
        }

        public void Register()
        {
            visCenter.Register(this.Name, this);
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
            this.Refresh();
            if (Selected)
            {
                visCenter.VisualizationSelected(this.Name);
            }
         }

        public void Update(bool b)
        {
            Selected = b;
            this.UseTileImage = Selected;
            this.Refresh();
        }
}
}
