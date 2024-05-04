using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
	public class AgentController : BaseController
	{
		private readonly IAgentService agentService;

		public AgentController(IAgentService _agentService)
		{
			agentService = _agentService;
		}

		public async Task<IActionResult> Become()
		{
			var model = new BecomeAgentFormModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel model)
		{
            if (await agentService.ExistsByIdAsync(User.Id()))
            {
                return BadRequest();
            }

			if (await agentService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number already exists. Enter another one");
            }

            if (await agentService.UserHasRentsAsync(User.Id()))
            {
                ModelState.AddModelError("Error", "You should have no rents to become an Agent");
            }

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await agentService.CreateAsync(User.Id(), model.PhoneNumber);


            return RedirectToAction(nameof(HouseController.All), "House");
		}
	}
}
