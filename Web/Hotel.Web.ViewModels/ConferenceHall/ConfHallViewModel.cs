using System.Collections.Generic;

namespace Hotel.Web.ViewModels.ConferenceHall
{
    public class ConfHallViewModel
    {
        public IEnumerable<ConfHallAllViewModel> AllReservations { get; set; }
    }
}
