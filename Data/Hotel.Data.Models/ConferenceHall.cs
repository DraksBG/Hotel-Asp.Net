namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;
    using Enums;

    public class ConferenceHall : BaseDeletableModel<string>
    {
        public ConferenceHall()
        {
            Id = Guid.NewGuid().ToString();
            Pictures = new List<Picture>();
            ConferenceHallReservations = new HashSet<ConferenceHallReservation>();
        }

        [Required]
        public ConfHallEventType EventType { get; set; }

        public IList<Picture> Pictures { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }

        [Required]
        [Range(0, 100)]
        public int CurrentCapacity { get; set; }

        [Required]
        [Range(0, 100)]
        public int MaxCapacity { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal Price { get; set; }

        public ICollection<ConferenceHallReservation> ConferenceHallReservations { get; set; }
    }
}
