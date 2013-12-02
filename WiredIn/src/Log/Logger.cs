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
    using System.Text;
    using System.Windows.Forms;

    using WiredIn.DataStructure;
    using WiredIn.Globals;
    using WiredIn.UserActivity;

    /// <summary>
    /// Logger writes user activity + time to log file
    /// </summary>
    public class Logger : IRunner, IDisposable
    {
        #region Fields

        /// <summary>
        /// The bg worker
        /// </summary>
        private BackgroundWorker bgWorker;
        /// <summary>
        /// The buffer string builder
        /// </summary>
        private StringBuilder bufferStringBuilder;
        /// <summary>
        /// The configuration
        /// </summary>
        private ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The current iter
        /// </summary>
        private int currentIter = 0;
        /// <summary>
        /// The full path
        /// </summary>
        private String fullPath;
        /// <summary>
        /// The global timer
        /// </summary>
        private GlobalTimer globalTimer = GlobalTimer.GetGlobalTimer();
        /// <summary>
        /// The is_running
        /// </summary>
        private bool is_running = false;
        /// <summary>
        /// The number of lines in buffer
        /// </summary>
        private int numOfLinesInBuffer = 0;
        /// <summary>
        /// The SQL writer
        /// </summary>
        private SqlWriter sqlWriter;
        /// <summary>
        /// The queue
        /// </summary>
        private Queue<Activity> theQueue;
        /// <summary>
        /// The work idle
        /// </summary>
        private bool workIdle = true;
        /// <summary>
        /// The work sphere
        /// </summary>
        private Worksphere workSphere = Worksphere.GetWorkSphere();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="q">The q.</param>
        public Logger(Queue<Activity> q)
        {
            this.theQueue = q;
            bufferStringBuilder = new StringBuilder();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the context data.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetContextData()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(workSphere.CustomVisualizerName);
            sb.Append("+");
            sb.Append(config.Condition.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
        public bool IsRunning()
        {
            return is_running;
        }

        /// <summary>
        /// Logs the context.
        /// </summary>
        public void LogContext()
        {
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine(GetContextData(), true);
            }
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public void SetUp()
        {
            globalTimer.AttachElapseEvent(this.LoggerTimer_Elapsed);
            PrepareLogFilePath();
            //LogContext();
            if (config.EnableSQLLogging)
            {
                PrepareSQL();
            }

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            is_running = true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            is_running = false;
            DequeueAll();
            WriteBufferToFile();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public void TearDown()
        {
            globalTimer.DetachElapseEvent(this.LoggerTimer_Elapsed);
            FlushAllRemaining();
        }

        /// <summary>
        /// Handles the DoWork event of the bgWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            workIdle = false;
            WriteBufferToFile();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
            }
            else if (e.Cancelled)
            {
            }
            else
            {
                workIdle = true;
            }
        }

        //Deque all and write to string builder
        /// <summary>
        /// Dequeues all.
        /// </summary>
        private void DequeueAll()
        {
            lock (this)
            {
                while (theQueue.Count > 0)
                {
                    Activity a = theQueue.Dequeue();
                    EnqueueBuffer(a);
                }

            }
        }

        /// <summary>
        /// Enqueues the buffer.
        /// </summary>
        /// <param name="ac">The ac.</param>
        private void EnqueueBuffer(Activity ac)
        {
            this.FormatLog(bufferStringBuilder, ac);
            bufferStringBuilder.Append("\n");
            numOfLinesInBuffer++;
        }

        /// <summary>
        /// Flushes all remaining.
        /// </summary>
        private void FlushAllRemaining()
        {
            DequeueAll();
            WriteBufferToFile();
        }

        /// <summary>
        /// Formats the log.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="ac">The ac.</param>
        private void FormatLog(StringBuilder sb, Activity ac)
        {
            DateTime dt = ac.When();
            sb.Append(dt.ToShortDateString() + ", " + dt.ToLongTimeString());
            sb.Append(", ");
            sb.Append(ac.getScore());
            sb.Append(", ");
            sb.Append(ac.StateString);
            sb.Append(",");
            sb.Append(ac.What());
            sb.Append(ac.getStats());
        }

        /// <summary>
        /// Handles the Elapsed event of the LoggerTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LoggerTimer_Elapsed(object sender, EventArgs e)
        {
            if (currentIter >= config.LoggerBufferClockIteration)
            {
                try
                {
                    DequeueAll();
                    TryToDelegateToBgWorker();
               }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                currentIter++;
            }
        }

        /// <summary>
        /// Prepares the log file path.
        /// </summary>
        private void PrepareLogFilePath()
        {
            String parentFolder = Path.Combine(config.WiredInFolder, "log");
            String instance_id =  RunIDKeeper.GetIDKeeper().GetRunID();
            String logFolder = Path.Combine(parentFolder, instance_id);

            DirectoryInfo di = new DirectoryInfo(logFolder);
            if (!di.Exists)
            {
                di.Create();
            }

            string file_name = "activity.csv";
            this.fullPath = Path.Combine(logFolder, file_name);
        }

        /// <summary>
        /// Prepares the SQL.
        /// </summary>
        private void PrepareSQL()
        {
            sqlWriter = new SqlWriter();
            sqlWriter.Prepare(); //prepare the sql statements
        }

        /// <summary>
        /// Tries to delegate to bg worker.
        /// </summary>
        private void TryToDelegateToBgWorker()
        {
            if (bgWorker.IsBusy && !workIdle)
            {
                return;
            }
            if (numOfLinesInBuffer > 50 && theQueue.Count == 0)
            {
                bgWorker.RunWorkerAsync();
                currentIter = 0;
            }
        }

        /// <summary>
        /// Writes the buffer to file.
        /// </summary>
        private void WriteBufferToFile()
        {
            if (bufferStringBuilder.Length > 0)
            {
                using (StreamWriter writer = new StreamWriter(fullPath, true))
                {
                    writer.WriteLine(bufferStringBuilder.ToString(), true);
                    bufferStringBuilder.Length = 0; //clear string buider
                    numOfLinesInBuffer = 0;
                }
            }
        }

        #endregion Methods

        /// <summary>
        /// Dispose
        /// </summary>
         protected virtual void Dispose(bool disposing)
        {
            if (bgWorker != null)
            {
                bgWorker.Dispose();
            }
        }

        /// <summary>
        ///  Disposal
        /// </summary>
         public void Dispose()
         {
             Dispose(true);
             GC.SuppressFinalize(true);
         }
    }
}