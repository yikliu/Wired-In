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
/// The Globals namespace.
/// </summary>
namespace WiredIn.Globals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #region Enumerations

    /// <summary>
    /// Enum ApplicationSize
    /// </summary>
    public enum ApplicationSize
    {
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
        Full
    }

    /// <summary>
    /// Enum Direction
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// From bad to good
        /// </summary>
        FromBadToGood,
        /// <summary>
        /// From good to bad
        /// </summary>
        FromGoodToBad
    }

    /// <summary>
    /// Enum OperandConditioning
    /// </summary>
    public enum OperandConditioning
    {
        /// <summary>
        /// The reward
        /// </summary>
        Reward,
        /// <summary>
        /// The Punish
        /// </summary>
        Punish
    }

    /// <summary>
    /// Enum State
    /// </summary>
    public enum State
    {
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
        Dormant
    }

    /// <summary>
    /// Enum Visualizer
    /// </summary>
    public enum Visualizer
    {
        /// <summary>
        /// The rose
        /// </summary>
        Rose,
        /// <summary>
        /// The moon
        /// </summary>
        Moon,
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
        Custom,
        /// <summary>
        /// The mirror
        /// </summary>
        Mirror,

        /// <summary>
        /// Clock
        /// </summary>
        Clock,

        NOT_ASSIGNED
    }

    #endregion Enumerations
}