namespace Hotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;
    using Enums;

    public class RoomReservation : BaseDeletableModel<string>
    {
        public RoomReservation()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string RoomId { get; set; }

        [Required]
        public Room Room { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumberOfGuests { get; set; }

        [Required]
        public int NumberOfNights { get; set; }
    }
}
