using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
    public class Specialty
    {
        private string _name;
        private int _id;

        public Specialty(string name, int id = -1)
        {
            _name = name;
            _id = id;
        }

        public String GetName()
        {
            return _name;
        }

        public int GetID()
        {
            return _id;
        }

        public Stylist[] GetAllStylists()
        {
            if(_id == -1)
            {
                throw new Exception("Can't perform this operation without saving first.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT stylists.* FROM stylists JOIN stylists_specialties ON stylists.id = stylists_specialties.stylist_id WHERE stylists_specialties.specialty_id = {_id};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Stylist> output = new List<Stylist> ();

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name, id);
                output.Add(newStylist);
            }

            DB.Close(conn);

            return output.ToArray();
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
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@Name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@Name";
            name.Value = _name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            DB.Close(conn);
        }

        public void Delete()
        {
            if(_id == -1)
            {
                throw new Exception("Can't delete a specialty that hasn't been saved to the database.");
            } else {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"DELETE FROM specialties WHERE id = {_id};";
                cmd.ExecuteNonQuery();

                DB.Close(conn);
                _id = -1;
            }
        }

        public static Specialty GetByID(int searchID)
        {
            Specialty result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM specialties WHERE (id = {searchID});";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                result = new Specialty(name, id);
            }
            DB.Close(conn);

            return result;
        }

        public static Specialty[] GetAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Specialty> output = new List<Specialty> ();

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);

                output.Add(new Specialty(name, id));
            }

            DB.Close(conn);

            return output.ToArray();
        }
    }
}
