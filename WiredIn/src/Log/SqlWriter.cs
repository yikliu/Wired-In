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

using System.Diagnostics;

namespace WiredIn.Log
{
    using System;

    using MySql.Data.MySqlClient;

    /// <summary>
    /// Class SqlWriter.
    /// </summary>
    class SqlWriter : IDisposable
    {
        #region Fields

        /// <summary>
        /// The command
        /// </summary>
        private MySqlCommand cmd = null;
        /// <summary>
        /// The connection
        /// </summary>
        private MySqlConnection conn = null;
        /// <summary>
        /// The score
        /// </summary>
        private MySqlParameter score;
        /// <summary>
        /// The time
        /// </summary>
        private MySqlParameter time;
        /// <summary>
        /// The title
        /// </summary>
        private MySqlParameter title;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Inserts the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="s">The s.</param>
        /// <param name="tm">The tm.</param>
        public void Insert(String t, int s, String tm)
        {
            try
            {
                title.Value = t;
                score.Value = s;
                time.Value = tm;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error: {0}", ex.ToString());
            }
        }

        /// <summary>
        /// Prepares this instance.
        /// </summary>
        public void Prepare()
        {
            try
            {
                string cs = @"<connection string>";
                conn = new MySqlConnection();
                conn.ConnectionString = cs;
                conn.Open();

                cmd = new MySqlCommand();
                cmd.Connection = conn;
                string SQL = "INSERT INTO windows(oldTitle,score,time) VALUES(@oldTitle,@score,@time)";
                cmd.CommandText = SQL;

                title = new MySqlParameter("@oldTitle", MySqlDbType.VarChar);
                cmd.Parameters.Add(title);

                score = new MySqlParameter("@score", MySqlDbType.Int16);
                cmd.Parameters.Add(score);

                time = new MySqlParameter("@time", MySqlDbType.VarChar);
                cmd.Parameters.Add(time);

                cmd.Prepare();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error: {0}", ex.ToString());
            }
        }

        #endregion Methods

        public void Dispose()
        {
            
        }
    }
}