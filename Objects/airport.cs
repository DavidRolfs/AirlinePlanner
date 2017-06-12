using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Planner
{
  public class Airport
  {
    private int _id;
    private string _name;

    public Airport(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }

    public override bool Equals(System.Object otherAirport)
    {
      if (!(otherAirport is Airport))
      {
        return false;
      }
      else
      {
        Airport newAirport = (Airport) otherAirport;
        bool idEquality = this.GetId() == newAirport.GetId();
        bool nameEquality = this.GetName() == newAirport.GetName();
        return (idEquality && nameEquality);
      }
    }
    public int GetId()
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
    public static List<Airport> GetAll()
    {
      List<Airport> allAirports = new List<Airport>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM airport;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int airportId = rdr.GetInt32(0);
        string airportName = rdr.GetString(1);
        Airport newAirport = new Airport(airportName, airportId);
        allAirports.Add(newAirport);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAirports;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO airport (name) OUTPUT INSERTED.id VALUES (@AirportName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AirportName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Airport Find(int id)
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM airport WHERE id = @AirportId;", conn);
    SqlParameter airportIdParameter = new SqlParameter();
    airportIdParameter.ParameterName = "@AirportId";
    airportIdParameter.Value = id.ToString();
    cmd.Parameters.Add(airportIdParameter);
    SqlDataReader rdr = cmd.ExecuteReader();

    int foundAirportId = 0;
    string foundAirportDescription = null;

    while(rdr.Read())
    {
      foundAirportId = rdr.GetInt32(0);
      foundAirportDescription = rdr.GetString(1);
    }
    Airport foundAirport = new Airport(foundAirportDescription, foundAirportId);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return foundAirport;
  }

  public void Delete()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("DELETE FROM airport WHERE id = @AirportId; DELETE FROM airport_tasks WHERE airport_id = @AirportId;", conn);
        SqlParameter airportIdParameter = new SqlParameter();
        airportIdParameter.ParameterName = "@AirportId";
        airportIdParameter.Value = this.GetId();

        cmd.Parameters.Add(airportIdParameter);
        cmd.ExecuteNonQuery();

        if (conn != null)
        {
          conn.Close();
        }
      }





    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM airport;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
