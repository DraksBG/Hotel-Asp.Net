namespace Hotel.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Common;
    using Hotel.Data.Common.Repositories;
    using Data.Models;
    using Hotel.Services.Data.Restaurant;
    using Hotel.Services.Data.User;
    using ViewModels.InputModels.Restaurant;
    using ViewModels.Restaurant;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RestaurantController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IRestaurantService restaurantService;

        public RestaurantController(IUsersService usersService, IRestaurantService restaurantService)
        {
            this.usersService = usersService;
            this.restaurantService = restaurantService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Reserve()
        {
            var inputModel = new RestaurantInputModel
            {
                PhoneNumber = await usersService.GetUserPhoneNumberAsync(User),
                EventDate = DateTime.Now,
            };

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(RestaurantInputModel input)
        {
            var remainingCapacity = restaurantService.GetRemainingCapacity();

            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var userId = await usersService.GetUserIdAsync(User);

            input.UserId = userId;

            var result = await restaurantService.ReserveRestaurant(input);

            if (result == false)
            {
                ModelState.AddModelError("EventDate", GlobalConstants.FreeSeatsForRestaurantError + remainingCapacity);
                return View(input);
            }

            TempData["InfoMessage"] = GlobalConstants.ReserveRestaurantTempDataSuccess;

            return Redirect("/Restaurant/Reservations");
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var userId = await usersService.GetUserIdAsync(User);

            var reservations = await restaurantService
                .GetAllReservationsAsync<RestaurantAllViewModel>(userId);

            var reservationView = new RestaurantViewModel()
            {
                AllReservations = reservations,
            };

            return View(reservationView);
        }
    }
}
