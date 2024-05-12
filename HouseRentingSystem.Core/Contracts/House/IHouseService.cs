using HouseRentingSystem.Core.Enumerations;
using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts.House
{
    public interface IHouseService
	{
		Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<int> Create(HouseFormModel model, int agentId);

        Task<HouseQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNamesAsync();

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);

        Task<IEnumerable<HouseServiceModel>> AllHousesByRentnerIdAsync(string rentnerId);

        Task<bool> HouseExistsAsync(int id);

        Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id);

        Task EditAsync(HouseFormModel model, int houseId);

        Task<bool> HasAgentWithIdAsync(int houseId, string userId);

        Task<HouseFormModel?> GetHouseFormModelByIdAsync(int id);

        Task DeleteAsync(int houseId);

        Task<bool> IsRentedAsync(int houseId);

        Task<bool> IsRentedByIUserWithIdAsync(int houseId, string userId);

        Task RentAsync(int houseId, string userId);

        Task LeaveAsync(int houseId, string userId);

        Task<IEnumerable<HouseServiceModel>> GetUnApprovedAsync();

        Task ApproveHouseAsync(int houseId);
    }
}
