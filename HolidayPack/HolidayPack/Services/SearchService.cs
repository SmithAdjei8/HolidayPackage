using HolidayPack.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPack.Services
{
	public class SearchService
	{
		private IEnumerable<Flight> _flights;
		private IEnumerable<Hotel> _hotels;

		public SearchService(IEnumerable<Flight> flights, IEnumerable<Hotel> hotels)
		{
			_flights = flights;
			_hotels = hotels;
		}

		public IEnumerable<SrchResults> Results(string jsonParams)
		{
			var matchedFlights = new List<Flight>();


			var customerQuery = JsonConvert.DeserializeObject<SrchValues>(jsonParams);

			var londonAirports = new string[] { "LGW", "LTN" };

			if (customerQuery != null)
			{
				if (customerQuery.DepartingFrom.ToUpper() == "ANY LONDON AIRPORT")
				{
					matchedFlights = _flights
					.Where(x => x.Departure_Date == customerQuery.DepartureDate && londonAirports.Contains(x.From) && x.To == customerQuery.TravellingTo)
					.OrderBy(x => x.Price)
					.ToList();
				}
				else if (customerQuery.DepartingFrom.ToUpper() == "ANY AIRPORT")
				{
					matchedFlights = _flights
					.Where(x => x.Departure_Date == customerQuery.DepartureDate && x.To == customerQuery.TravellingTo)
					.OrderBy(x => x.Price)
					.ToList();
				}
				else
				{
					matchedFlights = _flights
					.Where(x => x.Departure_Date == customerQuery.DepartureDate && x.From == customerQuery.DepartingFrom && x.To == customerQuery.TravellingTo)
					.OrderBy(x => x.Price)
					.ToList();
				}

				var selectedFlight = matchedFlights.Take(1);

				var matchedHotels = _hotels
					.Where(x => x.Local_Airports == customerQuery.TravellingTo && x.Arrival_Date <= customerQuery.DepartureDate && x.Nights >= customerQuery.Duration)
					.OrderBy(x => x.Nights)
					.ToList()
					.Take(1);

				var packagesAvailable = from flight in selectedFlight
										from hotel in matchedHotels
										let totalNightsPrice = hotel.Price_Per_Night * customerQuery.Duration
										let totalPrice = flight.Price + totalNightsPrice
										select new SrchResults
										{
											FlightFound = flight,
											HotelFound = hotel,
											TotalPrice = totalPrice,
											TotNightsPrice = totalNightsPrice,
										};

				packagesAvailable = packagesAvailable.OrderBy(p => p.TotalPrice).ToList();
				return packagesAvailable.Take(1);
			}
			else
				return null;
		}
	}
}
