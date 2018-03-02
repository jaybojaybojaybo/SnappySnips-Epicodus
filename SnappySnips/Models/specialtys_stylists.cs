using System.Collections.Generic;
using System;
using SnappySnips;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace SnappySnips.Models
{
  public class specialtys_stylists
  {
    private int _id;
    private int _specialty_id;
    private int _stylist_id;

    public specialtys_stylists(int id, int specialty, int stylist)
    {
      _id = id;
      _specialty_id = specialty;
      _stylist_id = stylist;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE specialtys_stylists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
