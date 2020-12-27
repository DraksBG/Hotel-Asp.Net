namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Common.Models;

   public class Restaurant : BaseDeletableModel<string>
    {
        [Required] public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100)]
        public int CurrentCapacity { get; set; }

        [Required]
        [Range(0, 100)]
        public int MaxCapacity { get; set; }

        public IList<Picture> Pictures { get; set; }
        
        public virtual ICollection<RestaurantReservation> RestaurantReservations { get; set; }
    }
}
