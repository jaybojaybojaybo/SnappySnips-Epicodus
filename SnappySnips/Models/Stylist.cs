using System.Collections.Generic;
using System;
using SnappySnips;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace SnappySnips.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private System.DateTime _hireDate;

    public Stylist(string name, System.DateTime hireDate, int Id = 0)
    {
      _id = Id;
      _name = name;
      _hireDate = hireDate;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetHireDate(DateTime hireDate)
    {
      _hireDate = hireDate;
    }

    public DateTime GetHireDate()
    {
      return _hireDate;
    }

    public void SetId(int id)
    {
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        bool hireDateEquality = (this.GetHireDate() == newStylist.GetHireDate());
        return (idEquality && nameEquality && hireDateEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        System.DateTime hireDate = rdr.GetDateTime(2);
        Stylist newStylist = new Stylist(name, hireDate, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allStylists;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name, hireDate) VALUES (@name, @hireDate);";

      cmd.Parameters.Add(new MySqlParameter("@name", _name));
      cmd.Parameters.Add(new MySqlParameter("@hireDate", _hireDate));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Stylist Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM stylists WHERE id = @searchId;";

     cmd.Parameters.Add(new MySqlParameter("@searchId", id));

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int stylistId = 0;
     string stylistName = "";
     System.DateTime stylistHireDate = System.DateTime.Today;

     while (rdr.Read())
     {
       stylistId = rdr.GetInt32(0);
       stylistName = rdr.GetString(1);
       stylistHireDate = rdr.GetDateTime(2);
     }

     Stylist foundStylist = new Stylist(stylistName, stylistHireDate, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundStylist;
    }

    public void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @searchId;";

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

    public void EditName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";

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

    public void EditHireDate(DateTime newHireDate)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET hireDate = @newHireDate WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _id));
      cmd.Parameters.Add(new MySqlParameter("@newHireDate", newHireDate));

      cmd.ExecuteNonQuery();
      _hireDate = newHireDate;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddClient(Client newClient)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients_stylists (client_id, stylist_id) VALUES (@ClientId, @StylistId);";

      cmd.Parameters.Add(new MySqlParameter("@ClientId", newClient.GetId()));
      cmd.Parameters.Add(new MySqlParameter("@StylistId", _id));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public List<Client> GetClients()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT clients.* FROM stylists
        JOIN clients_stylists ON (stylists.id = clients_stylists.stylist_id)
        JOIN clients ON (clients_stylists.client_id = clients.id)
        WHERE stylists.id = @StylistId;";

      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = _id;
      cmd.Parameters.Add(stylistIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<Client> stylists = new List<Client> {};
      while(rdr.Read())
      {
          int clientId = rdr.GetInt32(0);
          string clientName = rdr.GetString(1);
          Client foundClient = new Client(clientName, clientId);
          stylists.Add(foundClient);

      }
      rdr.Dispose();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return stylists;
    }

    public void AddSpecialty(Specialty newSpecialty)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO specialtys_stylists (specialty_id, stylist_id) VALUES (@SpecialtyId, @StylistId);";

        cmd.Parameters.Add(new MySqlParameter("@SpecialtyId", newSpecialty.GetId()));
        cmd.Parameters.Add(new MySqlParameter("@StylistId", _id));

        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public List<Specialty> GetSpecialtys()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT specialtys.* FROM stylists
          JOIN specialtys_stylists ON (stylists.id = specialtys_stylists.stylist_id)
          JOIN specialtys ON (specialtys_stylists.specialty_id = specialtys.id)
          WHERE stylists.id = @StylistId;";

        MySqlParameter stylistIdParameter = new MySqlParameter();
        stylistIdParameter.ParameterName = "@StylistId";
        stylistIdParameter.Value = _id;
        cmd.Parameters.Add(stylistIdParameter);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        List<Specialty> stylists = new List<Specialty> {};
        while(rdr.Read())
        {
            int specialtyId = rdr.GetInt32(0);
            string specialtyName = rdr.GetString(1);
            Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);
            stylists.Add(foundSpecialty);

        }
        rdr.Dispose();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return stylists;
    }
  }
}
