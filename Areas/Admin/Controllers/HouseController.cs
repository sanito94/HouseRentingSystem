using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Models.Admin;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
    public class HouseController : AdminBaseController
    {
        private readonly IHouseService houseService;

        private readonly IAgentService agentService;

        public HouseController(
            IHouseService _houseService,
            IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;
        }
        public async Task<IActionResult> Mine()
        {
            var userId = User.Id();
            int agentId = await agentService.GetAgentId(userId) ?? 0;
            var myHouses = new MyHousesViewModel()
            {
                AddedHouses = await houseService.AllHousesByAgentIdAsync(agentId),
                RentedHouses = await houseService.AllHousesByRentnerIdAsync(userId)
            };

            return View(myHouses);
        }
    }
}
