using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Planner
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["flights"] = _ => {
        List<Flight> AllFlights = Flight.GetAll();
        return View["flights.cshtml", AllFlights];
      };
      Get["/airports"] = _ => {
        List<Airport> AllAirports = Airport.GetAll();
        return View["airports.cshtml", AllAirports];
      };
      //Create new Flight
      Get["/flights/new"]= _ => {
        return View["flights_form.cshtml"];
      };
      Post["/flights/new"]= _ => {
        Flight newFLight = new Flight(Request.Form["flight-city"]);
        newFLight.Save();
        return View["success.cshtml"];
      };
      //Create new Airport
      Get["/airports/new"]= _ => {
        return View["airports_form.cshtml"];
      };
      Post["/airports/new"]= _ => {
        Airport newAirport = new Airport(Request.Form["airport-city"]);
        newAirport.Save();
        return View["success.cshtml"];
      };

    }
  }
}
