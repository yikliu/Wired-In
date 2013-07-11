using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace WiredIn.Log
{
    class SqlWriter
    {
        private MySqlConnection conn = null;
        private MySqlCommand cmd = null;
        private MySqlParameter title;
        private MySqlParameter score;
        private MySqlParameter time;

        public void Prepare()
        {
            try
            {
                string cs = @"server=54.214.0.224;userid=root;password=678jaesy;database=wiredin";
                conn = new MySqlConnection();
                conn.ConnectionString = cs;
                conn.Open();

                cmd = new MySqlCommand();
                cmd.Connection = conn;
                string SQL = "INSERT INTO windows(title,score,time) VALUES(@title,@score,@time)";
                cmd.CommandText = SQL;

                title = new MySqlParameter("@title", MySqlDbType.VarChar);
                cmd.Parameters.Add(title);

                score = new MySqlParameter("@score", MySqlDbType.Int16);
                cmd.Parameters.Add(score);

                time = new MySqlParameter("@time", MySqlDbType.VarChar);
                cmd.Parameters.Add(time);

                cmd.Prepare();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }  
        }

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
                Console.WriteLine("Error: {0}", ex.ToString());
            }
        }
    }
}
