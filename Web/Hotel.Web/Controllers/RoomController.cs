namespace Hotel.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Room;
    using Hotel.Services.Data.User;
    using Hotel.Web.ViewModels.InputModels.Room;
    using Hotel.Web.ViewModels.RoomViewModels;
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
            var rooms = await this.roomsService
                .GetAllRoomsAsync<RoomsAllViewModel>();

            var roomView = new RoomViewModel()
            {
                AllRooms = rooms,
            };

            return this.View(roomView);
        }

        [HttpGet]
        [Route("Room/Details/{roomId}")]
        public async Task<IActionResult> Details([FromRoute] string roomId)
        {
            var room = await this.roomsService.GetRoomAsync<RoomDetailsViewModel>(roomId);

            return this.View(room);
        }

        [Authorize]
        [HttpGet]
        [Route("Room/Reserve/{roomId}")]
        public async Task<IActionResult> Reserve([FromRoute] string roomId)
        {
            var inputModel = new ReserveRoomInputModel();

            var pictures = await this.picturesRepository.All().Where(x => x.RoomId == roomId).ToListAsync();

            inputModel.Pictures = pictures.Select(x => x.Url).ToList();
            inputModel.PhoneNumber = await this.usersService.GetUserPhoneNumberAsync(this.User);
            inputModel.CheckIn = DateTime.Now;
            inputModel.CheckOut = DateTime.Now;

            return this.View(inputModel);
        }

        [HttpPost]
        [Route("Room/Reserve/{roomId}")]
        public async Task<IActionResult> Reserve([FromRoute] string roomId, ReserveRoomInputModel input)
        {
            var pictures = await this.picturesRepository.All().Where(x => x.RoomId == roomId).ToListAsync();
            var room = this.roomsService.GetRoomById(roomId);

            if (!this.ModelState.IsValid || room.NumberOfBeds < input.CountOfPeople)
            {
                this.ModelState.AddModelError("CountOfPeople", GlobalConstants.EnterValidNumberOfGuestsError + room.NumberOfBeds);
                input.Pictures = pictures.Select(x => x.Url).ToList();
                return this.View(input);
            }

            var userId = await this.usersService.GetUserIdAsync(this.User);

            input.UserId = userId;
            input.RoomId = roomId;

            var result = await this.roomsService.ReserveRoom(input);

            if (result == false)
            {
                this.ModelState.AddModelError("NumberOfNights", GlobalConstants.EnterAtleastOneNightStandsError);
                input.Pictures = pictures.Select(x => x.Url).ToList();
                return this.View(input);
            }

            this.TempData["InfoMessage"] = GlobalConstants.ReserveRoomTempDataSuccess;

            return this.Redirect("/Room/Reservations");
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var userId = await this.usersService.GetUserIdAsync(this.User);

            var reservations = await this.roomsService
                .GetAllReservationsAsync<ReservationsAllViewModel>(userId);

            var reservationView = new ReservationViewModel()
            {
                AllReservations = reservations,
            };

            return this.View(reservationView);
        }
    }
}
