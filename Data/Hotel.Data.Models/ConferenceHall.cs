namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;
    using Hotel.Data.Models.Enums;

    public class ConferenceHall : BaseDeletableModel<string>
    {
        public ConferenceHall()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Pictures = new List<Picture>();
            this.ConferenceHallReservations = new HashSet<ConferenceHallReservation>();
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
