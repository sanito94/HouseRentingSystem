﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Contracts.Agent
{
    public interface IAgentService
    {
        Task<bool> ExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserHasRentsAsync(string userId);

        Task CreateAsync(string userId, string phoneNumber);

        Task<int?> GetAgentId(string agentId);
    }
}
