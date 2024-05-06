using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Exceptions;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static HouseRentingSystem.Core.Constants.MessageConstants;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : BaseController
    {
		private readonly IHouseService houseService;
		private readonly IAgentService agentService;
        private readonly ILogger logger;

        public HouseController(
			IHouseService _houseService,
			IAgentService _agentService,
            ILogger<HouseController> _logger)
		{
			houseService = _houseService;
			agentService = _agentService;
			logger = _logger;
		}


		[AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
			var queryResult = await houseService.AllAsync(
				query.Category,
				query.SearchTerm,
				query.Sorting,
				query.CurrentPage,
				query.HousesPerPage);

			query.TotalHousesCount = queryResult.TotalHousesCount;
			query.Houses = queryResult.Houses;

			var houseCategories = await houseService.AllCategoriesNamesAsync();
			query.Categories = houseCategories;

            return View(query);
        }



		public async Task<IActionResult> Mine()
		{

			var userId = User.Id();
			IEnumerable<HouseServiceModel> model;

			if (await agentService.ExistsByIdAsync(userId))
			{
				int currentAgentId = await agentService.GetAgentId(userId) ?? 0;
				model = await houseService.AllHousesByAgentIdAsync(currentAgentId);
			}
			else
			{
				model = await houseService.AllHousesByRentnerIdAsync(userId);
			}

			return View(model);
		}

		public async Task<IActionResult> Details(int id)
		{
			if (await houseService.HouseExistsAsync(id) == false)
			{
				return BadRequest();
			}

			var houseModel = await houseService.HouseDetailsByIdAsync(id);

			return View(houseModel);
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
			if (await houseService.HouseExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await houseService.HasAgentWithIdAsync(id, User.Id()) == false)
			{
				return Unauthorized();
			}

			var model = await houseService.GetHouseFormModelByIdAsync(id);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, HouseFormModel model)
		{
            if (await houseService.HouseExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            if (await houseService.CategoryExistsAsync(model.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

			await houseService.EditAsync(model, id);

            return RedirectToAction(nameof(Details), new { id });
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (await houseService.HouseExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await houseService.HasAgentWithIdAsync(id, User.Id()) == false)
			{
				return Unauthorized();
			}

			var house = await houseService.HouseDetailsByIdAsync(id);

			var model = new HouseDetailsViewModel()
			{
				Id = id,
				Address = house.Address,
				ImageUrl = house.ImageUrl,
				Title = house.Title,
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, HouseDetailsViewModel model)
		{
			if (await houseService.HouseExistsAsync(id) == false)
			{
				return BadRequest();
			}

			if (await houseService.HasAgentWithIdAsync(model.Id, User.Id()) == false)
			{
				return Unauthorized();
			}

			await houseService.DeleteAsync(model.Id);

			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> Rent(int id)
		{
            if (await houseService.HouseExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await agentService.ExistsByIdAsync(User.Id()))
            {
                return Unauthorized();
            }

            if (await houseService.IsRentedAsync(id))
            {
                return BadRequest();
            }

            await houseService.RentAsync(id, User.Id());

            return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> Leave(int id)
		{
            if (await houseService.HouseExistsAsync(id) == false)
            {
                return BadRequest();
            }

            try
            {
                await houseService.LeaveAsync(id, User.Id());

                TempData[UserMessageSuccess] = "You have left the house!";
            }
            catch (UnauthorizedActionException uae)
            {
                logger.LogError(uae, "HouseController/Leave");

                return Unauthorized();
            }

            return RedirectToAction(nameof(All));
		}
	}
}
