using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
    public class Stylist
    {
        protected int id;
        private string name;

        public Stylist(string name)
        {
            this.name = name;
            this.id = -1;
        }

        public int GetID()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string newName)
        {
            name = newName;
        }

        public void Save()
        {
            throw new NotImplementedException();
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
                Stylist newStylist = new Stylist(name);
                newStylist.id = id;
                output.Add(newStylist);
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

        public static Stylist GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public static Stylist[] GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
