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

    [Fact]
    public void Delete_DeletesAirportFromDatabase_AirportList()
    {
      Airport testAirport1 = new Airport("Milwaukee");
      testAirport1.Save();

      Airport testAirport2 = new Airport("Chicago");
      testAirport2.Save();

      testAirport1.Delete();
      List<Airport> resultAirport = Airport.GetAll();
      List<Airport> testAirportList = new List<Airport>{testAirport2};

      Assert.Equal(testAirportList, resultAirport);
    }

    [Fact]
    public void Test_AddFLight_AddsFlightToAirport()
    {
      Airport testAirport = new Airport("Milwaukee");

      Flight testFlight = new Flight("to Denver");
      testFlight.Save();

      Flight testFlight2 = new Flight("to Chicago");
      testFlight2.Save();

      testAirport.AddFlight(testFlight);
      testAirport.AddFlight(testFlight2);

      List<Flight> result = testAirport.GetFlights();
      List<Flight> testList = new List<Flight>{testFlight, testFlight2};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetFlights_ReturnAllAirportFlights_FlightList()
    {
      Airport testAirport = new Airport("Milwaukee");
      testAirport.Save();

      Flight testFlight1 = new Flight("to Denver");
      testFlight1.Save();

      Flight testFlight2 = new Flight("to Chicago");
      testFlight2.Save();

      testAirport.AddFlight(testFlight1);
      List<Flight> savedFLights = testAirport.GetFlights();
      List<Flight> testList = new List<Flight> {testFlight1};

      Assert.Equal(testList, savedFLights);
    }



    public void Dispose()
    {
      Airport.DeleteAll();
      Flight.DeleteAll();
    }
  }
}
