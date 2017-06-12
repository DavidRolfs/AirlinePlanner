using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Planner
{
  [Collection("Planner")]
  public class AirportTest : IDisposable
  {
    public AirportTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_AirportEmptyAtFirst()
    {
      int result = Airport.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Airport firstAirport = new Airport("Milwaukee");
      Airport secondAirport = new Airport("Milwaukee");

      Assert.Equal(firstAirport, secondAirport);
    }
    [Fact]
    public void Test_Save_SavesAirportToDatabase()
    {
      Airport testAirport = new Airport("Milwaukee");
      testAirport.Save();

      List<Airport> result = Airport.GetAll();
      List<Airport> testList = new List<Airport>{testAirport};

      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      Airport.DeleteAll();
    }
  }
}
