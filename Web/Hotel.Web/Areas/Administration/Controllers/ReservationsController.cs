using System.Threading.Tasks;
using Hotel.Services.Data.ConferenceHall;
using Hotel.Services.Data.Restaurant;
using Hotel.Services.Data.Room;
using Hotel.Web.ViewModels.ConferenceHall;
using Hotel.Web.ViewModels.Restaurant;
using Hotel.Web.ViewModels.RoomViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Areas.Administration.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IRestaurantService restaurantService;
        private readonly IConferenceHallService conferenceHallService;
        private readonly IRoomsService roomsService;

        public ReservationsController(
            IRestaurantService restaurantService,
            IConferenceHallService conferenceHallService,
            IRoomsService roomsService)
        {
            this.restaurantService = restaurantService;
            this.conferenceHallService = conferenceHallService;
            this.roomsService = roomsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllRoomReservations()
        {
            var reservations = await this.roomsService
                .GetAllReservationsAsyncForAdmin<ReservationsAllViewModel>();

            var reservationView = new ReservationViewModel()
            {
                AllReservations = reservations,
            };

            return this.View("~/Views/Room/Reservations.cshtml", reservationView);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllRestaurantReservations()
        {
            var reservations = await this.restaurantService
                .GetAllReservationsAsyncForAdmin<RestaurantAllViewModel>();

            var reservationView = new RestaurantViewModel()
            {
                AllReservations = reservations,
            };

            return this.View("~/Views/Restaurant/Reservations.cshtml", reservationView);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllConferenceHallReservations()
        {
            var reservations = await this.conferenceHallService
                 .GetAllReservationsAsyncForAdmin<ConfHallAllViewModel>();

            var reservationView = new ConfHallViewModel()
            {
                AllReservations = reservations,
            };

            return this.View("~/Views/ConferenceHall/Reservations.cshtml", reservationView);
        }
    }
}
