namespace Hotel.Services.Data.ConferenceHall
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Hotel.Web.ViewModels.InputModels.ConferenceHall;

    public interface IConferenceHallService
    {
        Task<bool> ReserveConferenceHall(ConferenceHallInputModel input);

        Task<IEnumerable<TViewModel>> GetAllReservationsAsync<TViewModel>(string userId);

        Task<IEnumerable<TViewModel>> GetAllReservationsAsyncForAdmin<TViewModel>();

        Task<int> GetAllHallsAsync<TViewModel>(ConferenceHallInputModel input);
    }
}
