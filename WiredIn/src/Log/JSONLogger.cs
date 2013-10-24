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
/// The Log namespace.
/// </summary>
namespace WiredIn.Log
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class JSONLogger.
    /// </summary>
    class JSONLogger
    {
        #region Fields

        /// <summary>
        /// The bg worker
        /// </summary>
        private BackgroundWorker bgWorker;
        /// <summary>
        /// The json_file_location
        /// </summary>
        private string json_file_location;
        /// <summary>
        /// The json_to_be_written
        /// </summary>
        private string json_to_be_written;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JSONLogger"/> class.
        /// </summary>
        public JSONLogger()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Logs the this to here.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="location">The location.</param>
        public void LogThisToHere(string str, string location)
        {
            json_to_be_written = str;
            json_file_location = location;
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the bgWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Log(this.json_to_be_written, this.json_file_location);
        }

        /// <summary>
        /// Logs the specified json_in_string.
        /// </summary>
        /// <param name="json_in_string">The json_in_string.</param>
        /// <param name="file_location">The file_location.</param>
        private void Log(string json_in_string, string file_location)
        {
            using (StreamWriter writer = new StreamWriter(file_location, true))
            {
                writer.WriteLine(json_in_string);
            }
        }

        #endregion Methods
    }
}