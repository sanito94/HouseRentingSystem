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

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository
				.AllReadOnly<Category>()
				.Select(c => new HouseCategoryServiceModel
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await repository.AllReadOnly<Category>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> Create(HouseFormModel model, int agentId)
        {
			var house = new House()
			{
				Title = model.Title,
				Address = model.Address,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				PricePerMonth = model.PricePerMonth,
				CategoryId = model.CategoryId,
				AgentId = agentId
			};

			await repository.AddAsync(house);
			await repository.SaveChangesAsync();

			return house.Id;
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
