using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
    public class Stylist
    {
        public string Name {get; set;}
        public int ID {get;}

        public Stylist(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public static Stylist[] GetAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT * FROM stylists;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Stylist> output = new List<Stylist> ();

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                output.Add(new Stylist(name, id));
            }

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }

            return output.ToArray();
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "DELETE FROM stylists;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
