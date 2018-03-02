using System.Collections.Generic;
using System;
using SnappySnips;
using MySql.Data.MySqlClient;

namespace SnappySnips.Models
{
  public class Specialty
  {
    private int _id;
    private string _name;

    public Specialty(string name, int Id = 0)
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

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());
        bool nameEquality = (this.GetName() == newSpecialty.GetName());
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialtys = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialtys;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(name, id);
        allSpecialtys.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allSpecialtys;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialtys (name) VALUES (@name);";

      cmd.Parameters.Add(new MySqlParameter("@name", _name));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Specialty Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM specialtys WHERE id = @searchId;";

     cmd.Parameters.Add(new MySqlParameter("@searchId", id));

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int specialtyId = 0;
     string specialtyName = "";

     while (rdr.Read())
     {
       specialtyId = rdr.GetInt32(0);
       specialtyName = rdr.GetString(1);
     }

     Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundSpecialty;
    }

    public void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialtys WHERE id = @searchId;";

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
      cmd.CommandText = @"UPDATE specialtys SET name = @newName WHERE id = @searchId;";

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
        cmd.CommandText = @"INSERT INTO specialtys_stylists (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

        cmd.Parameters.Add(new MySqlParameter("@StylistId", newStylist.GetId()));
        cmd.Parameters.Add(new MySqlParameter("@SpecialtyId", _id));

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
        cmd.CommandText = @"SELECT stylists.* FROM specialtys
          JOIN specialtys_stylists ON (specialtys.id = specialtys_stylists.specialty_id)
          JOIN stylists ON (specialtys_stylists.stylist_id = stylists.id)
          WHERE specialtys.id = @SpecialtyId;";

        cmd.Parameters.Add(new MySqlParameter("@SpecialtyId", _id));

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        List<Stylist> specialtys = new List<Stylist> {};
        while(rdr.Read())
        {
          int stylistId = rdr.GetInt32(0);
          string stylistName = rdr.GetString(1);
          System.DateTime hireDate = rdr.GetDateTime(2);
          Stylist foundStylist = new Stylist(stylistName, hireDate, stylistId);
          specialtys.Add(foundStylist);
        }
        rdr.Dispose();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return specialtys;
    }
  }
}
