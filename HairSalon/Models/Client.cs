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

        public Client(string name, Stylist stylist)
        {
            _name = name;
            _stylistID = stylist.GetID();
            _id = -1;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public int GetID()
        {
            return _id;
        }

        public Stylist GetStylist()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static Client GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
