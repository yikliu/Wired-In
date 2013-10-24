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
/// The UI namespace.
/// </summary>
namespace WiredIn.UI
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using MetroFramework.Forms;

    using Newtonsoft.Json;

    using WiredIn.Globals;
    using WiredIn.Log;

    /// <summary>
    /// Class ESMForm.
    /// </summary>
    public partial class ESMForm : MetroForm
    {
        #region Fields

        /// <summary>
        /// The configuration
        /// </summary>
        ConfigVariables config = ConfigVariables.GetConfigVariables();
        /// <summary>
        /// The esm
        /// </summary>
        ESM esm;
        /// <summary>
        /// The j_logger
        /// </summary>
        JSONLogger j_logger;
        /// <summary>
        /// The log file
        /// </summary>
        string logFile;
        /// <summary>
        /// The span
        /// </summary>
        private TimeSpan span;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ESMForm"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public ESMForm(TimeSpan s)
        {
            InitializeComponent();
            this.span = s;
            esm = new ESM();
            esm.Wasted = s;
            DateTime n = DateTime.Now;
            esm.Time = n.ToShortDateString() + " " + n.ToShortTimeString();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbReason control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            esm.Reason = (string) cbReason.SelectedItem;
            tbxReasonForDelay.Text = esm.Reason;
        }

        /// <summary>
        /// Converts the time span to string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        private string ConvertTimeSpanToString(TimeSpan s)
        {
            StringBuilder sbSpan = new StringBuilder();
            if (s.Hours > 0)
            {
                sbSpan.Append(s.Hours.ToString());
                sbSpan.Append(" ");
                sbSpan.Append("hours");
                sbSpan.Append(" ");
            }
            if (s.Minutes > 0)
            {
                sbSpan.Append(s.Minutes.ToString());
                sbSpan.Append(" minutes");
                sbSpan.Append(" ");
            }
            sbSpan.Append(s.Seconds.ToString());
            sbSpan.Append(" seconds");
            return sbSpan.ToString();
        }

        /// <summary>
        /// Handles the Load event of the ESMForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ESMForm_Load(object sender, EventArgs e)
        {
            lblTimeSpan.Text = ConvertTimeSpanToString(this.span);
        }

        /// <summary>
        /// Logs the specified a esm.
        /// </summary>
        /// <param name="aESM">A esm.</param>
        private void Log(ESM aESM)
        {
            string folder = Path.Combine(config.WiredInFolder, "log");
            folder = Path.Combine(folder, RunIDKeeper.GetIDKeeper().GetRunID());
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            logFile = Path.Combine(folder, "esm.json");
            j_logger = new JSONLogger();
            string json_content = JsonConvert.SerializeObject(aESM);
            j_logger.LogThisToHere(json_content, logFile);
        }

        /// <summary>
        /// Handles the Click event of the metroButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void metroButton1_Click(object sender, EventArgs e)
        {
            esm.Wasted = span;
            if (tbxReasonForDelay.Text.Length > 0)
            {
                esm.Reason = tbxReasonForDelay.Text;
            }
            else
            {
                esm.Reason = cbReason.SelectedText;
            }

            esm.VisualizationInfluence = tbInfluence.Value;

            Log(esm);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rbForgot control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rbForgot_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForgot.Checked)
                esm.Aware = "Forgot";
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rbReluctant control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rbReluctant_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReluctant.Checked)
                esm.Aware = "Reluctant";
        }

        #endregion Methods
    }
}