using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
	public class HouseService : IHouseService
	{
		private readonly IRepository repository;

		public HouseService(IRepository _repository)
		{
			repository = _repository;
		}

		public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
		{
			return await repository
				.AllReadOnly<House>()
				.OrderByDescending(h => h.Id)
				.Select(h => new HouseIndexServiceModel
				{
					Id = h.Id,
					Title = h.Title,
					ImageUrl = h.ImageUrl,
				})
				.Take(3)
				.ToListAsync();
		}
	}
}
