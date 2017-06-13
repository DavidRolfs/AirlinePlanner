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
      Get["flights/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Flight SelectedFlight = Flight.Find(parameters.id);
        List<Airport> FlightAirport = SelectedFlight.GetAirports();
        List<Airport> AllAirports = Airport.GetAll();
        model.Add("flight", SelectedFlight);
        model.Add("flightAirport", FlightAirport);
        model.Add("allAirports", AllAirports);
        return View["flight.cshtml", model];
      };
      Get["airports/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Airport SelectedAirport = Airport.Find(parameters.id);
        List<Flight> AirportFlight = SelectedAirport.GetFlights();
        List<Flight> AllFlights = Flight.GetAll();
        model.Add("airport", SelectedAirport);
        model.Add("airportFlight", AirportFlight);
        model.Add("allFlights", AllFlights);
        return View["airport.cshtml", model];
      };
      Post["flights/add_airport"] = _ => {
        Airport airport = Airport.Find(Request.Form["airport-id"]);
        Flight flight = Flight.Find(Request.Form["flight-id"]);
        flight.AddAirport(airport);
        return View["success.cshtml"];
      };
      Post["airports/add_flight"]= _ => {
        Airport airport = Airport.Find(Request.Form["airport-id"]);
        Flight flight = Flight.Find(Request.Form["flight-id"]);
        airport.AddFlight(flight);
        return View["success.cshtml"];
      };

    }
  }
}
