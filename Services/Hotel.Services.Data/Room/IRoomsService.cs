namespace Hotel.Services.Data.Room
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Hotel.Web.ViewModels.InputModels.Room;
    using Web.ViewModels.RoomViewModels;

    public interface IRoomsService
    {
        Task<bool> CreateRoomAsync(CreateRoomInputModel input);

        Task<bool> EditRoomAsync(string id, EditRoomViewModel input);

        Task<bool> DeleteRoom(string id);

        Task<IEnumerable<TViewModel>> GetAllRoomsAsync<TViewModel>();

        Task<TViewModel> GetRoomAsync<TViewModel>(string id);

        Task<EditRoomViewModel> GetRoomForEditAsync(string id);

        Task<IEnumerable<TViewModel>> GetAllReservationsAsync<TViewModel>(string userId);

        Task<IEnumerable<TViewModel>> GetAllReservationsAsyncForAdmin<TViewModel>();

        Task<bool> ReserveRoom(ReserveRoomInputModel input);

        Hotel.Data.Models.Room GetRoomById(string id);
    }
}
