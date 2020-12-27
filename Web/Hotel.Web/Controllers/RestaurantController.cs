namespace Hotel.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Restaurant;
    using Hotel.Services.Data.User;
    using Hotel.Web.ViewModels.InputModels.Restaurant;
    using Hotel.Web.ViewModels.Restaurant;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RestaurantController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IRestaurantService restaurantService;
        

        public RestaurantController(
            IUsersService usersService,
            IRestaurantService restaurantService,
            IDeletableEntityRepository<RestaurantReservation> restaurantReservationRepository)
        {
            this.usersService = usersService;
            this.restaurantService = restaurantService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Reserve()
        {
            var inputModel = new RestaurantInputModel
            {
                PhoneNumber = await this.usersService.GetUserPhoneNumberAsync(this.User),
                EventDate = DateTime.Now,
            };

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(RestaurantInputModel input)
        {
            var remainingCapacity = this.restaurantService.GetRemainingCapacity();

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = await this.usersService.GetUserIdAsync(this.User);

            input.UserId = userId;

            var result = await this.restaurantService.ReserveRestaurant(input);

            if (result == false)
            {
                this.ModelState.AddModelError("EventDate", GlobalConstants.FreeSeatsForRestaurantError + remainingCapacity);
                return this.View(input);
            }

            this.TempData["InfoMessage"] = GlobalConstants.ReserveRestaurantTempDataSuccess;

            return this.Redirect("/Restaurant/Reservations");
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var userId = await this.usersService.GetUserIdAsync(this.User);

            var reservations = await this.restaurantService
                .GetAllReservationsAsync<RestaurantAllViewModel>(userId);

            var reservationView = new RestaurantViewModel()
            {
                AllReservations = reservations,
            };

            return this.View(reservationView);
        }
    }
}
