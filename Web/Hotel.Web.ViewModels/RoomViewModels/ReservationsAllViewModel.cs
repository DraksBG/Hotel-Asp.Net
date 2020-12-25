using System;
using Hotel.Data.Models;
using Hotel.Services.Mapping;

namespace Hotel.Web.ViewModels.RoomViewModels
{
    public class ReservationsAllViewModel : IMapFrom<RoomReservation>
    {
        public ApplicationUser User { get; set; }

        public string PhoneNumber { get; set; }

        public string RoomType { get; set; }

        public int NumberOfGuests { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
