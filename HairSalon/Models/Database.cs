using System;
using HairSalon;
using MySql.Data.MySqlClient;

namespace Application.Models
{
    public static class DB
    {
        public static MySqlConnection Connection()
        {
            return new MySqlConnection(DBConfiguration.ConnectionString);
        }

        public static void Clear()
        {
            
        }
    }
}
