using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Planner
{
//  [Colleciton("Planner")]
  public class AirportTest
  // : IDisposable
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
  }
}
