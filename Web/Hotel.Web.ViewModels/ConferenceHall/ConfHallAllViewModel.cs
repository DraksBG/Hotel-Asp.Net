namespace Hotel.Web.ViewModels.ConferenceHall
{
    using System;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class ConfHallAllViewModel : IMapFrom<ConferenceHallReservation>, IMapFrom<Data.Models.ConferenceHall>
    {
        public string ConferenceHallId { get; set; }

        public ApplicationUser User { get; set; }

        public string PhoneNumber { get; set; }

        public string EventType { get; set; }

        public int NumberOfGuests { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
