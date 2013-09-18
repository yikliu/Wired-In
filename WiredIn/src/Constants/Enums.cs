// ***********************************************************************
// Assembly         : WiredIn
// Author           : yikliu
// Created          : 09-08-2013
//
// Last Modified By : yikliu
// Last Modified On : 09-08-2013
// ***********************************************************************
// <copyright file="Enums.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiredIn.Constants
{
    /// <summary>
    /// Enum ApplicationSize
    /// </summary>
    public enum ApplicationSize {
        /// <summary>
        /// The small
        /// </summary>
        Small,
        /// <summary>
        /// The medium
        /// </summary>
        Medium,
        /// <summary>
        /// The full
        /// </summary>
        Full };

    /// <summary>
    /// Enum Visualization
    /// </summary>
    public enum Visualization {
        /// <summary>
        /// The many step images
        /// </summary>
        ManyStepImages,
        /// <summary>
        /// The progressbar
        /// </summary>
        Progressbar,
        /// <summary>
        /// The empty
        /// </summary>
        Empty,
        /// <summary>
        /// The custom
        /// </summary>
        Custom };

    /// <summary>
    /// Enum OperandCondition
    /// </summary>
    public enum OperandCondition {
        /// <summary>
        /// The reward
        /// </summary>
        reward,
        /// <summary>
        /// The punish
        /// </summary>
        punish };

    /// <summary>
    /// Enum State
    /// </summary>
    public enum State {
        /// <summary>
        /// The good
        /// </summary>
        Good,
        /// <summary>
        /// The bad
        /// </summary>
        Bad,
        /// <summary>
        /// The dormant
        /// </summary>
        Dormant };    
}
