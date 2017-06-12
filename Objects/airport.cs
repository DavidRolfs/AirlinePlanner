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






  }
}
