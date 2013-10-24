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

    /// <summary>
    /// Class RunInstance.
    /// </summary>
    class RunInstance
    {
        #region Fields

        /// <summary>
        /// The estimated business
        /// </summary>
        int estimatedBusiness;
        /// <summary>
        /// The estimated energy
        /// </summary>
        int estimatedEnergy;
        /// <summary>
        /// The estimated stresslevel
        /// </summary>
        int estimatedStresslevel;
        /// <summary>
        /// The expected difficulty
        /// </summary>
        int expectedDifficulty;
        /// <summary>
        /// The expected familarity
        /// </summary>
        int expectedFamilarity;
        /// <summary>
        /// The expected time on hour
        /// </summary>
        int expectedTimeOnHour;
        /// <summary>
        /// The location
        /// </summary>
        string location;
        /// <summary>
        /// The noisy level
        /// </summary>
        int noisyLevel;
        /// <summary>
        /// The number of to do items
        /// </summary>
        int numOfToDoItems;
        /// <summary>
        /// The task description
        /// </summary>
        string taskDescription;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the estimated business.
        /// </summary>
        /// <value>The estimated business.</value>
        public int EstimatedBusiness
        {
            get { return estimatedBusiness; }
            set { estimatedBusiness = value; }
        }

        /// <summary>
        /// Gets or sets the estimated energy.
        /// </summary>
        /// <value>The estimated energy.</value>
        public int EstimatedEnergy
        {
            get { return estimatedEnergy; }
            set { estimatedEnergy = value; }
        }

        /// <summary>
        /// Gets or sets the estimated stresslevel.
        /// </summary>
        /// <value>The estimated stresslevel.</value>
        public int EstimatedStresslevel
        {
            get { return estimatedStresslevel; }
            set { estimatedStresslevel = value; }
        }

        /// <summary>
        /// Gets or sets the expected difficulty.
        /// </summary>
        /// <value>The expected difficulty.</value>
        public int ExpectedDifficulty
        {
            get { return expectedDifficulty; }
            set { expectedDifficulty = value; }
        }

        /// <summary>
        /// Gets or sets the expected familarity.
        /// </summary>
        /// <value>The expected familarity.</value>
        public int ExpectedFamilarity
        {
            get { return expectedFamilarity; }
            set { expectedFamilarity = value; }
        }

        /// <summary>
        /// Gets or sets the expected time on hour.
        /// </summary>
        /// <value>The expected time on hour.</value>
        public int ExpectedTimeOnHour
        {
            get { return expectedTimeOnHour; }
            set { expectedTimeOnHour = value; }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Gets or sets the noisy level.
        /// </summary>
        /// <value>The noisy level.</value>
        public int NoisyLevel
        {
            get { return noisyLevel; }
            set { noisyLevel = value; }
        }

        /// <summary>
        /// Gets or sets the number of to do items.
        /// </summary>
        /// <value>The number of to do items.</value>
        public int NumOfToDoItems
        {
            get { return numOfToDoItems; }
            set { numOfToDoItems = value; }
        }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>The task description.</value>
        public string TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value; }
        }

        #endregion Properties
    }
}