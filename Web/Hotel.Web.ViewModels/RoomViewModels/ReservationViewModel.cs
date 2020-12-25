using System.Collections.Generic;

namespace Hotel.Web.ViewModels.RoomViewModels
{
    public class ReservationViewModel
    {
        public IEnumerable<ReservationsAllViewModel> AllReservations { get; set; }
    }
}
