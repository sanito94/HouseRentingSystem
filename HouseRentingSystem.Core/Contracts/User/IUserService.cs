using HouseRentingSystem.Core.Models.Admin.User;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserServiceModel>> AllAsync();
    }
}
