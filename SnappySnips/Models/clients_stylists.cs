using System.Collections.Generic;
using System;
using SnappySnips;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace SnappySnips.Models
{
  public class clients_stylists
  {
    private int _id;
    private int _client_id;
    private int _stylist_id;

    public clients_stylists(int id, int client, int stylist)
    {
      _id = id;
      _client_id = client;
      _stylist_id = stylist;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE clients_stylists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
