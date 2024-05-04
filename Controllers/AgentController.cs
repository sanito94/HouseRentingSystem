using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Mvc;
using static HouseRentingSystem.Core.Constants.MessageConstants;

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
            if (await agentService.ExistsByIdAsync(User.Id()))
            {
                return BadRequest();
            }

            var model = new BecomeAgentFormModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel model)
		{

			if (await agentService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
            }

            if (await agentService.UserHasRentsAsync(User.Id()))
            {
                ModelState.AddModelError("Error", HasRents);
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
