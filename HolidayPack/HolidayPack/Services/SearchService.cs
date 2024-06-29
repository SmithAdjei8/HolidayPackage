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
			var customerQuery = JsonConvert.DeserializeObject<SrchValues>(jsonParams);

			//if (customerQuery == null)
			//{
			//	foreach(var param in customerQuery)
			//	{
			//		param1 = param.DepartingFrom;
			//	}
			//}

			var matchedFlights = _flights
				.Where(x => x.Departure_Date == customerQuery.DepartureDate && x.From == customerQuery.DepartingFrom && x.To == customerQuery.TravellingTo)
				.ToList();

			var matchedHotels = _hotels
				.Where(x => x.Local_Airports == customerQuery.TravellingTo && x.Arrival_Date <= customerQuery.DepartureDate && x.Nights >= customerQuery.Duration)
				.ToList();

			var packagesAvailable = from flight in matchedFlights
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

			return packagesAvailable.OrderBy(p => p.TotalPrice).ToList();
		}
	}
}
