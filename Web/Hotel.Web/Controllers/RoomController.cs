namespace Hotel.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Common;
    using Hotel.Data.Common.Repositories;
    using Data.Models;
    using Hotel.Services.Data.Room;
    using Hotel.Services.Data.User;
    using ViewModels.InputModels.Room;
    using ViewModels.RoomViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class RoomController : Controller
    {
        private readonly IRoomsService roomsService;
        private readonly IUsersService usersService;
        private readonly IDeletableEntityRepository<Picture> picturesRepository;

        public RoomController(
            IRoomsService roomsService,
            IUsersService usersService,
            IDeletableEntityRepository<Picture> picturesRepository)
        {
            this.roomsService = roomsService;
            this.usersService = usersService;
            this.picturesRepository = picturesRepository;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var rooms = await roomsService
                .GetAllRoomsAsync<RoomsAllViewModel>();

            var roomView = new RoomViewModel()
            {
                AllRooms = rooms,
            };

            return View(roomView);
        }

        [HttpGet]
        [Route("Room/Details/{roomId}")]
        public async Task<IActionResult> Details([FromRoute] string roomId)
        {
            var room = await roomsService.GetRoomAsync<RoomDetailsViewModel>(roomId);

            return View(room);
        }

        [Authorize]
        [HttpGet]
        [Route("Room/Reserve/{roomId}")]
        public async Task<IActionResult> Reserve([FromRoute] string roomId)
        {
            var inputModel = new ReserveRoomInputModel();

            var pictures = await picturesRepository.All().Where(x => x.RoomId == roomId).ToListAsync();

            inputModel.Pictures = pictures.Select(x => x.Url).ToList();
            inputModel.PhoneNumber = await usersService.GetUserPhoneNumberAsync(User);
            inputModel.CheckIn = DateTime.Now;
            inputModel.CheckOut = DateTime.Now;

            return View(inputModel);
        }

        [HttpPost]
        [Route("Room/Reserve/{roomId}")]
        public async Task<IActionResult> Reserve([FromRoute] string roomId, ReserveRoomInputModel input)
        {
            var pictures = await picturesRepository.All().Where(x => x.RoomId == roomId).ToListAsync();
            var room = roomsService.GetRoomById(roomId);

            if (!ModelState.IsValid || room.NumberOfBeds < input.CountOfPeople)
            {
                ModelState.AddModelError("CountOfPeople", GlobalConstants.EnterValidNumberOfGuestsError + room.NumberOfBeds);
                input.Pictures = pictures.Select(x => x.Url).ToList();
                return View(input);
            }

            var userId = await usersService.GetUserIdAsync(User);

            input.UserId = userId;
            input.RoomId = roomId;

            var result = await roomsService.ReserveRoom(input);

            if (result == false)
            {
                ModelState.AddModelError("NumberOfNights", GlobalConstants.EnterAtleastOneNightStandsError);
                input.Pictures = pictures.Select(x => x.Url).ToList();
                return View(input);
            }

            TempData["InfoMessage"] = GlobalConstants.ReserveRoomTempDataSuccess;

            return Redirect("/Room/Reservations");
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var userId = await usersService.GetUserIdAsync(User);

            var reservations = await roomsService
                .GetAllReservationsAsync<ReservationsAllViewModel>(userId);

            var reservationView = new ReservationViewModel()
            {
                AllReservations = reservations,
            };

            return View(reservationView);
        }
    }
}
