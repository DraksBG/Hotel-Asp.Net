namespace Hotel.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Common;
    using Hotel.Services.Data.ConferenceHall;
    using Hotel.Services.Data.User;
    using ViewModels.ConferenceHall;
    using ViewModels.InputModels.ConferenceHall;
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
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Reserve()
        {
            var inputModel = new ConferenceHallInputModel
            {
                PhoneNumber = await usersService.GetUserPhoneNumberAsync(User),
                EventDate = DateTime.Now,
            };

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ConferenceHallInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var userId = await usersService.GetUserIdAsync(User);

            input.UserId = userId;

            var result = await conferenceHallService.ReserveConferenceHall(input);

            var capacity = await conferenceHallService
                .GetAllHallsAsync<ConfHallAllViewModel>(input);

            if (result == false)
            {
                ModelState.AddModelError("EventDate", GlobalConstants.FreeSeatsForHallError + capacity);
                return View(input);
            }

            TempData["InfoMessage"] = GlobalConstants.ReserveConferenceHallTempDataSuccess;

            return Redirect("/ConferenceHall/Reservations");
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var userId = await usersService.GetUserIdAsync(User);

            var reservations = await conferenceHallService
                .GetAllReservationsAsync<ConfHallAllViewModel>(userId);

            var reservationView = new ConfHallViewModel()
            {
                AllReservations = reservations,
            };

            return View(reservationView);
        }
    }
}
