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
      Flight testFlight = new Flight("Mow the lawn");
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
      Flight testFlight = new Flight("Mow the lawn");
      testFlight.Save();

      //Act
      Flight result = Flight.Find(testFlight.GetId());

      //Assert
      Assert.Equal(testFlight, result);
    }

public void Dispose()
    {
      Flight.DeleteAll();
      Airport.DeleteAll();
    }
  }
}
