namespace Hotel.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Common;
    using Hotel.Services.Data.Room;
    using ViewModels.InputModels.Room;
    using ViewModels.RoomViewModels;
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
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRoomInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await roomsService.CreateRoomAsync(input);

            TempData["InfoMessage"] = GlobalConstants.CreateRoomTempDataSuccess;

            return RedirectToAction("All");
        }

        [Authorize]
        public async Task<IActionResult> DeleteRoom(string roomId)
        {
            var result = await roomsService.DeleteRoom(roomId);

            if (result == true)
            {
                TempData["InfoMessage"] = GlobalConstants.DeleteRoomTempDataSuccess;
                return RedirectToAction("All");
            }

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        [Route("Room/Edit/{roomId}")]
        public async Task<IActionResult> Edit([FromRoute] string roomId)
        {
            var result = await roomsService.GetRoomForEditAsync(roomId);

            return View(result);
        }

        [HttpPost]
        [Authorize]
        [Route("Room/Edit/{roomId}")]
        public async Task<IActionResult> Edit([FromRoute] string roomId, EditRoomViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await roomsService.EditRoomAsync(roomId, input);
            TempData["InfoMessage"] = GlobalConstants.EditRoomTempDataSuccess;
            return RedirectToAction("All");
        }
    }
}
