using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Models.Agent;
using Microsoft.AspNetCore.Authorization;
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
			return View(new BecomeAgentFormModel
			{

			});
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel agent)
		{
			return RedirectToAction(nameof(HouseController.All), "House");
		}
	}
}
