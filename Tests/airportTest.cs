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

    [Fact]
    public void Test_Save_AssignsIdToCategoryObject()
    {
      Airport testAirport = new Airport("Milwaukee");
      testAirport.Save();

      Airport savedAirport = Airport.GetAll()[0];

      int result = savedAirport.GetId();
      int testId = testAirport.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsAirportInDatabase()
    {
      Airport testAirport = new Airport("Milwaukee");
      testAirport.Save();

      Airport foundAirport = Airport.Find(testAirport.GetId());

      Assert.Equal(testAirport, foundAirport);
    }



    public void Dispose()
    {
      Airport.DeleteAll();
    }
  }
}
