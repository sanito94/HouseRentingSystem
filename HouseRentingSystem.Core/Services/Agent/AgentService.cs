using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Infrastructure.Data;
using HouseRentingSystem.Infrastructure.Data.Common;

namespace HouseRentingSystem.Core.Services.Agent
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;

        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }

        public Task<bool> ExistsById(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
