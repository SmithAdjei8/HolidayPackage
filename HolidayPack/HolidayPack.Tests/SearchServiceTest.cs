using HolidayPack.Services;

namespace HolidayPack.Tests
{
	public class SearchServiceTest
	{
		[Fact]
		public void Test_Holiday_Srch()
		{
			//Arrange
			DataService ds = new DataService();
			var flights = ds.GetFlights();
			var hotels = ds.GetHotels();

			var serviceAvail = new SearchService(flights, hotels);

			//Act
			var packageFound = serviceAvail.Results(@"{
									DepartingFrom: 'Any Airport',
									TravellingTo: 'LPA',
									DepartureDate: '2022/11/10',
									Duration: 14
									}");

			//Assert
			packageFound.ToList();
		}
	}
}