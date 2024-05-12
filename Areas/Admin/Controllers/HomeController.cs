using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IHouseService houseService;

        private readonly IAgentService agentService;

        public HomeController(
            IHouseService _houseService,
            IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ForReview()
        {
            var model = await houseService.GetUnApprovedAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ForReview(int houseId)
        {
            await houseService.ApproveHouseAsync(houseId);

            return RedirectToAction(nameof(ForReview));
        }
    }
}
