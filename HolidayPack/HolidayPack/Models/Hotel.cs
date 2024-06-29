using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPack.Models
{
	public class Hotel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Arrival_Date { get; set; }
		public decimal Price_Per_Night { get; set; }
		public string Local_Airports { get; set; }
		public int Nights { get; set; }
	}
}
