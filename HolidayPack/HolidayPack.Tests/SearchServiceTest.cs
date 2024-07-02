using HolidayPack.Services;
using FluentAssertions;

namespace HolidayPack.Tests
{
	public class SearchServiceTest
	{
		private readonly DataService _dataService;
		private readonly SearchService _searchService;

		public SearchServiceTest()
		{
			_dataService = new DataService();
			_searchService = new SearchService(_dataService.GetFlights(), _dataService.GetHotels());
		}

		[Fact]
		public void Test_Holiday_Srch_Any_Airport()
		{
			//Arrange

			//Act
			var packageFound = _searchService.Results(@"{
									DepartingFrom: 'Any Airport',
									TravellingTo: 'LPA',
									DepartureDate: '2022/11/10',
									Duration: 14
									}");

			//Assert
			Assert.NotNull(packageFound);
			Assert.Single(packageFound);
			Assert.Equal(7, packageFound.First().FlightFound.Id);
			Assert.Equal(6, packageFound.First().HotelFound.Id);
		}

		[Fact]
		public void Test_Holiday_Srch_Any_London_Airport()
		{
			//Arrange

			//Act
			var packageFound = _searchService.Results(@"{
									DepartingFrom: 'Any London Airport',
									TravellingTo: 'PMI',
									DepartureDate: '2023/06/15',
									Duration: 10
									}");

			//Assert
			Assert.NotNull(packageFound);
			Assert.Single(packageFound);
			Assert.Equal(6, packageFound.First().FlightFound.Id);
			Assert.Equal(5, packageFound.First().HotelFound.Id);
		}

		[Fact]
		public void Test_Holiday_Srch_Specific_Airport()
		{
			//Arrange

			//Act
			var packageFound = _searchService.Results(@"{
									DepartingFrom: 'MAN',
									TravellingTo: 'AGP',
									DepartureDate: '2023/07/01',
									Duration: 7
									}");

			//Assert
			Assert.NotNull(packageFound);
			Assert.Single(packageFound);
			Assert.Equal(2, packageFound.First().FlightFound.Id);
			Assert.Equal(9, packageFound.First().HotelFound.Id);
		}
	}
}