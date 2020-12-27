namespace Hotel.Web.ViewModels.ConferenceHall
{
    using System.Collections.Generic;

    public class ConfHallViewModel
    {
        public IEnumerable<ConfHallAllViewModel> AllReservations { get; set; }
    }
}
