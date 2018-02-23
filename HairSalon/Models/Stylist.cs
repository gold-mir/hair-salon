using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;

        public Stylist(string name)
        {
            _name = name;
            _id = -1;
        }

        public int GetID()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        public void Save()
        {
            if(_id != -1)
            {
                return;
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@StylistName);";

            MySqlParameter insertName = new MySqlParameter();
            insertName.ParameterName = "@StylistName";
            insertName.Value = _name;
            cmd.Parameters.Add(insertName);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            DB.Close(conn);
        }

        public static Stylist[] GetAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Stylist> output = new List<Stylist> ();

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name);
                newStylist._id = id;
                output.Add(newStylist);
            }

            DB.Close(conn);

            return output.ToArray();
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();

            DB.Close(conn);
        }

        public static Stylist GetByID(int id)
        {
            Stylist result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM stylists WHERE (id = {id});";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int localID = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name);
                newStylist._id = localID;
                result = newStylist;
            }
            DB.Close(conn);

            return result;
        }

        public static Stylist[] GetByName(string name)
        {
            List<Stylist> result = new List<Stylist>();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE name = @Name ORDER BY id;";

            MySqlParameter nameParam = new MySqlParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = name;
            cmd.Parameters.Add(nameParam);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string newName = rdr.GetString(1);
                Stylist newStylist = new Stylist(newName);
                newStylist._id = id;
                result.Add(newStylist);
            }

            DB.Close(conn);

            return result.ToArray();
        }
    }
}
