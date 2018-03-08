using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;

        public Stylist(string name, int id = -1)
        {
            _name = name;
            _id = id;
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
            if(_id == -1)
            {
                _name = newName;
            } else {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"UPDATE stylists SET name = @Name WHERE id = {_id};";

                MySqlParameter insertName = new MySqlParameter();
                insertName.ParameterName = "@Name";
                insertName.Value = newName;
                cmd.Parameters.Add(insertName);

                cmd.ExecuteNonQuery();

                DB.Close(conn);
            }
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

        public void Delete()
        {
            if(_id == -1)
            {
                throw new Exception("Can't delete a stylist that hasn't been saved to the database.");
            } else {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"DELETE FROM stylists WHERE id = {_id};";
                cmd.ExecuteNonQuery();

                DB.Close(conn);
                _id = -1;
            }
        }

        public Client[] GetClients()
        {
            return Client.GetClientsOfStylist(this);
        }

        public void AddSpecialty(Specialty specialty)
        {
            if(specialty.GetID() == -1 || _id == -1)
            {
                throw new Exception("Can't add a specialty unless both objects are saved;");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES ({_id}, {specialty.GetID()});";

            cmd.ExecuteNonQuery();

            DB.Close(conn);
        }

        public Specialty[] GetSpecialties()
        {
            if(_id == -1)
            {
                throw new Exception("Can't perform this operation without saving first.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT specialties.* FROM specialties JOIN stylists_specialties ON specialties.id = stylists_specialties.specialty_id WHERE stylists_specialties.stylist_id = {_id};";

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
