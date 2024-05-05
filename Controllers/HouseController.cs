using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : BaseController
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


		[AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {

            return View(query);
        }



		public async Task<IActionResult> Mine()
		{
			return View(new AllHousesQueryModel()
			{

			});
		}

		public async Task<IActionResult> Details(int id)
		{
			return View(new HouseDetailsViewModel()
			{

			});
		}

		public async Task<IActionResult> Add()
		{
            if (await agentService.ExistsByIdAsync(User.Id()) == false)
            {
				return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            var model = new HouseFormModel();

            model.Categories = await houseService.AllCategoriesAsync();

            return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(HouseFormModel model)
		{
			if (await houseService.CategoryExistsAsync(model.CategoryId) == false)
			{
				this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await houseService.AllCategoriesAsync();

				return View(model);
			}

			var agentId = await agentService.GetAgentId(User.Id());

			var newHouseId = await houseService.Create(model, agentId ?? 0);

			return RedirectToAction(nameof(Details), new { id = newHouseId });
		}

		public async Task<IActionResult> Edit(int id)
		{
			return View(new HouseFormModel()
			{

			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, HouseFormModel house)
		{
			return RedirectToAction(nameof(Details), new { id = "1" });
		}

		public async Task<IActionResult> Delete(int id)
		{
			return View(new HouseDetailsViewModel()
			{

			});
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, HouseDetailsViewModel house)
		{
			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> Rent(int id)
		{
			return RedirectToAction(nameof(Mine));
		}

		[HttpPost]
		public async Task<IActionResult> Leave(int id)
		{
			return RedirectToAction(nameof(Mine));
		}
	}
}
