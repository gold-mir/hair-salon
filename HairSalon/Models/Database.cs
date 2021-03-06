using System;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public static class DB
    {
        public static MySqlConnection Connection()
        {
            return new MySqlConnection(DBConfiguration.ConnectionString);
        }

        public static void Close(MySqlConnection conn)
        {
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
