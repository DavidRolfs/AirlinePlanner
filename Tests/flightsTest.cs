using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Planner
{
  [Collection("Planner")]
  public class FlightTest : IDisposable
  {
    public FlightTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Flight.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_TrueForSameDescription_Flight()
    {
      //Arrange, Act
      Flight firstFlight = new Flight("To Denver");
      Flight secondFlight = new Flight("To Denver");

      //Assert
      Assert.Equal(firstFlight, secondFlight);
    }

    [Fact]
    public void Save_FlightSavesToDatabase_FlightList()
    {
      //Arrange
      Flight testFlight = new Flight("To Denver");
      testFlight.Save();

      //Act
      List<Flight> result = Flight.GetAll();
      List<Flight> testList = new List<Flight>{testFlight};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_AssignsIdToObject_id()
    {
      //Arrange
      Flight testFlight = new Flight("to Denver");
      testFlight.Save();

      //Act
      Flight savedFlight = Flight.GetAll()[0];

      int result = savedFlight.GetId();
      int testId = testFlight.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Find_FindsFlightInDatabase_Flight()
    {
      //Arrange
      Flight testFlight = new Flight("to Denver");
      testFlight.Save();

      //Act
      Flight result = Flight.Find(testFlight.GetId());

      //Assert
      Assert.Equal(testFlight, result);
    }
    [Fact]
    public void AddAirport_AddsAirpotToFlight_AirportList()
    {
      Flight testFlight = new Flight("to denver");
      testFlight.Save();

      Airport testAirport = new Airport("Milwaukee");
      testAirport.Save();

      testFlight.AddAirport(testAirport);

      List<Airport> result = testFlight.GetAirports();
      List<Airport> testList = new List<Airport>{testAirport};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetAirports_ReturnsAllFLightAirports_AirportList()
    {
      Flight testFlight = new Flight("to Denver");
      testFlight.Save();

      Airport testAirport1 = new Airport("Milwaukee");
      testAirport1.Save();

      Airport testAirport2 = new Airport("Chicago");
      testAirport2.Save();

      testFlight.AddAirport(testAirport1);
      List<Airport> result = testFlight.GetAirports();
      List<Airport> testList = new List<Airport> {testAirport1};

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Delete_DeletesFlightAssociationsFromDataBase_FlightList()
    {
      Airport testAirport = new Airport("Milwaukee");
      testAirport.Save();

      string testCity = "to Denver";
      Flight testFlight = new Flight(testCity);
      testFlight.Save();

      testFlight.AddAirport(testAirport);
      testFlight.Delete();

      List<Flight> result = testAirport.GetFlights();
      List<Flight> test = new List<Flight>{};

      Assert.Equal(test, result);
    }

    public void Dispose()
    {
      Flight.DeleteAll();
      Airport.DeleteAll();
    }
  }
}
