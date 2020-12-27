namespace Hotel.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Services.Data.ConferenceHall;
    using Hotel.Services.Data.User;
    using Hotel.Web.ViewModels.ConferenceHall;
    using Hotel.Web.ViewModels.InputModels.ConferenceHall;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ConferenceHallController : Controller
    {
        private readonly IConferenceHallService conferenceHallService;
        private readonly IUsersService usersService;

        public ConferenceHallController(IConferenceHallService conferenceHallService, IUsersService usersService)
        {
            this.conferenceHallService = conferenceHallService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Reserve()
        {
            var inputModel = new ConferenceHallInputModel
            {
                PhoneNumber = await this.usersService.GetUserPhoneNumberAsync(this.User),
                EventDate = DateTime.Now,
            };

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ConferenceHallInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = await this.usersService.GetUserIdAsync(this.User);

            input.UserId = userId;

            var result = await this.conferenceHallService.ReserveConferenceHall(input);

            var capacity = await this.conferenceHallService
                .GetAllHallsAsync<ConfHallAllViewModel>(input);

            if (result == false)
            {
                this.ModelState.AddModelError("EventDate", GlobalConstants.FreeSeatsForHallError + capacity);
                return this.View(input);
            }

            this.TempData["InfoMessage"] = GlobalConstants.ReserveConferenceHallTempDataSuccess;

            return this.Redirect("/ConferenceHall/Reservations");
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var userId = await this.usersService.GetUserIdAsync(this.User);

            var reservations = await this.conferenceHallService
                .GetAllReservationsAsync<ConfHallAllViewModel>(userId);

            var reservationView = new ConfHallViewModel()
            {
                AllReservations = reservations,
            };

            return this.View(reservationView);
        }
    }
}
