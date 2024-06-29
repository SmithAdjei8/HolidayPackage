using HolidayPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolidayPack.Services
{
	public class DataService
	{
		public IEnumerable<Flight> GetFlights()
		{
			string filePath = @"./Data/Flights.json"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
			var flightData = File.ReadAllText(filePath);
			return JsonConvert.DeserializeObject<IEnumerable<Flight>>(flightData);
		}

		public IEnumerable<Hotel> GetHotels()
		{
			string filePath = @"./Data/Hotels.json";//Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
			var hotelData = File.ReadAllText(filePath);
			return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(hotelData);
		}
	}
}
