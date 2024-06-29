using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPack.Models
{
	public class SrchResults
	{
		public Flight FlightFound { get; set; }
		public Hotel HotelFound { get; set; }
		public decimal TotNightsPrice { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
