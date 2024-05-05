using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Enumerations;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseRentingSystem.Core.Services
{
	public class HouseService : IHouseService
	{
		private readonly IRepository repository;

		public HouseService(IRepository _repository)
		{
			repository = _repository;
		}

        public async Task<HouseQueryServiceModel> AllAsync(
			string? category = null, 
			string? searchTerm = null, 
			HouseSorting sorting = HouseSorting.Newest, 
			int currentPage = 1, 
			int housesPerPage = 1)
        {
			var houseQuery = repository.AllReadOnly<House>();

			if (!string.IsNullOrWhiteSpace(category))
			{
				houseQuery = houseQuery
					.Where(h => h.Category.Name == category);
			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				houseQuery = houseQuery
					.Where(h =>
					h.Title.ToLower().Contains(searchTerm.ToLower()) ||
					h.Address.ToLower().Contains(searchTerm.ToLower()) ||
					h.Description.ToLower().Contains(searchTerm.ToLower()));
			}

			houseQuery = sorting switch
			{
				HouseSorting.Price => houseQuery
				.OrderBy(h => h.PricePerMonth),
				HouseSorting.NotRentedFirst => houseQuery
				.OrderBy(h => h.RenterId != null)
				.ThenByDescending(h => h.Id),
				_ => houseQuery.OrderByDescending(h => h.Id)
			};

			var houses = houseQuery
				.Skip((currentPage- 1) * housesPerPage)
				.Take(housesPerPage)
				.Select(h => new HouseServiceModel
				{
					Id = h.Id,
					Title = h.Title,
					Address = h.Address,
					ImageUrl = h.ImageUrl,
					IsRented = h.RenterId != null,
					PricePerMonth = h.PricePerMonth,
				})
				.ToList();

			var totalHouses = houseQuery.Count();

			return new HouseQueryServiceModel()
			{
				TotalHousesCount = totalHouses,
				Houses = houses
			};
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

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
			return await repository.AllReadOnly<Category>()
				.Select(c => c.Name)
				.Distinct()
				.ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId)
        {
            return await repository.AllReadOnly<House>()
				.Where(h => h.AgentId == agentId)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByRentnerIdAsync(string rentnerId)
        {
            return await repository.AllReadOnly<House>()
                .Where(h => h.RenterId == rentnerId)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
					IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
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

        public async Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id)
        {
			return await repository.AllReadOnly<House>()
				.Where(h => h.Id == id)
				.Select(h => new HouseDetailsServiceModel
				{
					Id = h.Id,
					Title = h.Title,
					Address = h.Address,
					Description = h.Description,
					ImageUrl = h.ImageUrl,
					PricePerMonth = h.PricePerMonth,
					IsRented = h.RenterId != null,
					Category = h.Category.Name,
					Agent = new AgentServiceModel()
					{
						PhoneNumber = h.Agent.PhoneNumber,
						Email = h.Agent.User.Email
					}
				})
				.FirstAsync();
        }

        public async Task<bool> HouseExistsAsync(int id)
        {
            return await repository.AllReadOnly<House>()
				.AnyAsync(h => h.Id == id);
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
