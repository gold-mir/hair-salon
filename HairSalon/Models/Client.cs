using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
    public class Client
    {
        private int _stylistID;
        private int _id;
        private string _name;

        public Client(string name, Stylist stylist, int id = -1)
        {
            _name = name;
            _stylistID = stylist.GetID();
            _id = id;
        }

        private Client(string name, int stylistID)
        {
            _name = name;
            _stylistID = stylistID;
            _id = -1;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            if(_id == -1)
            {
                _name = name;
            } else {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"UPDATE clients SET name = @Name WHERE id = {_id};";

                MySqlParameter insertName = new MySqlParameter();
                insertName.ParameterName = "@Name";
                insertName.Value = name;
                cmd.Parameters.Add(insertName);

                cmd.ExecuteNonQuery();

                DB.Close(conn);
            }
            _name = name;
        }

        public int GetID()
        {
            return _id;
        }

        public Stylist GetStylist()
        {
            Stylist result = null;
            if(_stylistID == -1)
            {
                throw new Exception("Stylist id is -1. Did you forget to save it first?");
            }

            try
            {
                result = Stylist.GetByID(_stylistID);
            } catch (MySqlException ex)
            {
                if(ex.Message.StartsWith("Cannot add or update a child row: a foreign key constraint fails"))
                {
                    throw new Exception("Provided Stylist ID is not a real id");
                } else
                {
                    throw ex;
                }
            }
            return result;
        }

        public void Save()
        {
            if(_id != -1)
            {
                return;
            } else if (_stylistID == -1)
            {
                throw new Exception("Cannot save client becasue stylist id is -1. Did you forget to save it first?");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"INSERT INTO clients (name, stylist_id) VALUES (@Name, {_stylistID});";

            MySqlParameter insertName = new MySqlParameter();
            insertName.ParameterName = "@Name";
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
                throw new Exception("Can't delete a client that hasn't been saved.");
            } else {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"DELETE FROM clients WHERE id = {_id};";
                cmd.ExecuteNonQuery();

                DB.Close(conn);
                _id = -1;
            }
        }

        public void SetStylist(Stylist newStylist)
        {
            if(newStylist.GetID() == -1)
            {
                throw new Exception("Can't add an unsaved stylist to a client.");
            } else if (_id == -1)
            {
                _stylistID = newStylist.GetID();
            } else {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"UPDATE clients SET stylist_id = {newStylist.GetID()} WHERE id = {_id};";

                cmd.ExecuteNonQuery();

                DB.Close(conn);

                _stylistID = newStylist.GetID();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();

            DB.Close(conn);
        }

        public static Client[] GetAll()
        {
            List<Client> result  = new List<Client>();
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string newName = rdr.GetString(1);
                int newStylistId = rdr.GetInt32(2);

                Client newClient = new Client(newName, newStylistId);
                newClient._id = newID;
                result.Add(newClient);
            }

            DB.Close(conn);

            return result.ToArray();
        }

        public static Client GetByID(int id)
        {
            Client result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM clients WHERE id = {id};";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string newName = rdr.GetString(1);
                int newStylistId = rdr.GetInt32(2);

                Client newClient = new Client(newName, newStylistId);
                newClient._id = newID;
                result = newClient;
            }

            return result;
        }

        public static Client[] GetClientsOfStylist(Stylist owner)
        {

            List<Client> result = new List<Client>();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM clients WHERE stylist_id = {owner.GetID()};";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string newName = rdr.GetString(1);
                int newStylistID = rdr.GetInt32(2);

                Client newClient = new Client(newName, newStylistID);
                newClient._id = newID;
                result.Add(newClient);
            }

            DB.Close(conn);

            return result.ToArray();
        }
    }
}
