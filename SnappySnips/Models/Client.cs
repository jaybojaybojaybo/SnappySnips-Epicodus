using System.Collections.Generic;
using System;
using SnappySnips;
using MySql.Data.MySqlClient;

namespace SnappySnips.Models
{
  public class Client
  {
    private int _id;
    private string _name;

    public Client(string name, int Id = 0)
    {
      _id = Id;
      _name = name;
    }

    public void SetName(string name)

    {
      _name = name;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetId(int id)
    {
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Client newClient = new Client(name, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allClients;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name) VALUES (@name);";

      cmd.Parameters.Add(new MySqlParameter("@name", _name));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchId;";

     cmd.Parameters.Add(new MySqlParameter("@searchId", id));

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int clientId = 0;
     string clientName = "";

     while (rdr.Read())
     {
       clientId = rdr.GetInt32(0);
       clientName = rdr.GetString(1);
     }

     Client foundClient = new Client(clientName, clientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundClient;
    }

    public void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd1 = conn.CreateCommand() as MySqlCommand;
     cmd1.CommandText = @"TRUNCATE TABLE stylists;";
     cmd1.ExecuteNonQuery();

     var cmd2 = conn.CreateCommand() as MySqlCommand;
     cmd2.CommandText = @"TRUNCATE TABLE clients;";
     cmd2.ExecuteNonQuery();

     var cmd3 = conn.CreateCommand() as MySqlCommand;
     cmd3.CommandText = @"TRUNCATE TABLE specialtys;";
     cmd3.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _id));
      cmd.Parameters.Add(new MySqlParameter("@newName", newName));

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddStylist(Stylist newStylist)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO clients_stylists (stylist_id, client_id) VALUES (@StylistId, @ClientId);";

        cmd.Parameters.Add(new MySqlParameter("@StylistId", newStylist.GetId()));
        cmd.Parameters.Add(new MySqlParameter("@ClientId", _id));

        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public List<Stylist> GetStylists()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT stylists.* FROM clients
          JOIN clients_stylists ON (clients.id = clients_stylists.client_id)
          JOIN stylists ON (clients_stylists.stylist_id = stylists.id)
          WHERE clients.id = @ClientId;";

        cmd.Parameters.Add(new MySqlParameter("@ClientId", _id));

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        List<Stylist> clients = new List<Stylist> {};
        while(rdr.Read())
        {
          int stylistId = rdr.GetInt32(0);
          string stylistName = rdr.GetString(1);
          System.DateTime hireDate = rdr.GetDateTime(2);
          Stylist foundStylist = new Stylist(stylistName, hireDate, stylistId);
          clients.Add(foundStylist);
        }
        rdr.Dispose();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return clients;
    }
  }
}
