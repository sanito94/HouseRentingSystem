using HouseRentingSystem.Core.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
	[Authorize]
    public class HouseController : Controller
    {
		[AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View(new AllHousesQueryModel()
            {

            });
        }

        [Authorize]
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

		[Authorize]
		public async Task<IActionResult> Add()
		{
			return View(new HouseFormModel()
			{

			});
		}

		[HttpPost]
		public async Task<IActionResult> Add(HouseFormModel house)
		{
			return RedirectToAction(nameof(Details), new { id = "1" });
		}

		[Authorize]
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

		[Authorize]
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
