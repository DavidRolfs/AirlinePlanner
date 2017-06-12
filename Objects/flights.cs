using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Planner
{
  public class Flight
  {
    private int _id;
    private string _city;

    public Flight(string city, int Id = 0)
    {
      _id = Id;
      _city = city;
    }

    public override bool Equals(System.Object otherFlight)
    {
        if (!(otherFlight is Flight))
        {
          return false;
        }
        else {
          Flight newFlight = (Flight) otherFlight;
          bool idEquality = this.GetId() == newFlight.GetId();
          bool descriptionEquality = this.GetCity() == newFlight.GetCity();
          return (idEquality && descriptionEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetCity()
    {
      return _city;
    }
    public void SetDescription(string newCity)
    {
      _city = newCity;
    }
    public static List<Flight> GetAll()
    {
      List<Flight> AllFlights = new List<Flight>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        string flightCity = rdr.GetString(1);
        Flight newFlight = new Flight(flightCity, flightId);
        AllFlights.Add(newFlight);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllFlights;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (arrival_city) OUTPUT INSERTED.id VALUES (@Flightname)", conn);

      SqlParameter descriptionParam = new SqlParameter();
      descriptionParam.ParameterName = "@Flightname";
      descriptionParam.Value = this.GetCity();

      cmd.Parameters.Add(descriptionParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM flights;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Flight Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights WHERE id = @FlightId", conn);
      SqlParameter flightIdParameter = new SqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = id.ToString();
      cmd.Parameters.Add(flightIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundFlightId = 0;
      string foundFlightDescription = null;

      while(rdr.Read())
      {
        foundFlightId = rdr.GetInt32(0);
        foundFlightDescription = rdr.GetString(1);
      }
      Flight foundFlight = new Flight(foundFlightDescription, foundFlightId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundFlight;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM flights WHERE id = @FlightId; DELETE FROM airport_flights WHERE flight_id = @FlightId;", conn);
      SqlParameter flightIdParameter = new SqlParameter();
      flightIdParameter.ParameterName = "@FlightId";
      flightIdParameter.Value = this.GetId();

      cmd.Parameters.Add(flightIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
