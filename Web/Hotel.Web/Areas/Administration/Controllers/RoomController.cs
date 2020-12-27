namespace Hotel.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Services.Data.Room;
    using Hotel.Web.ViewModels.InputModels.Room;
    using Hotel.Web.ViewModels.RoomViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RoomController : Controller
    {
        private readonly IRoomsService roomsService;

        public RoomController(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRoomInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.roomsService.CreateRoomAsync(input);

            this.TempData["InfoMessage"] = GlobalConstants.CreateRoomTempDataSuccess;

            return this.RedirectToAction("All");
        }

        [Authorize]
        public async Task<IActionResult> DeleteRoom(string roomId)
        {
            var result = await this.roomsService.DeleteRoom(roomId);

            if (result == true)
            {
                this.TempData["InfoMessage"] = GlobalConstants.DeleteRoomTempDataSuccess;
                return this.RedirectToAction("All");
            }

            return this.NotFound();
        }

        [HttpGet]
        [Authorize]
        [Route("Room/Edit/{roomId}")]
        public async Task<IActionResult> Edit([FromRoute] string roomId)
        {
            var result = await this.roomsService.GetRoomForEditAsync(roomId);

            return this.View(result);
        }

        [HttpPost]
        [Authorize]
        [Route("Room/Edit/{roomId}")]
        public async Task<IActionResult> Edit([FromRoute] string roomId, EditRoomViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.roomsService.EditRoomAsync(roomId, input);
            this.TempData["InfoMessage"] = GlobalConstants.EditRoomTempDataSuccess;
            return this.RedirectToAction("All");
        }
    }
}
