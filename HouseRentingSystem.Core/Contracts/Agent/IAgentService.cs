using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Contracts.Agent
{
    public interface IAgentService
    {
        Task<bool> ExistsById(string userId);
    }
}
