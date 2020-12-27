namespace Hotel.Web.ViewModels.RoomViewModels
{
    using System.Collections.Generic;

    public class ReservationViewModel
    {
        public IEnumerable<ReservationsAllViewModel> AllReservations { get; set; }
    }
}
