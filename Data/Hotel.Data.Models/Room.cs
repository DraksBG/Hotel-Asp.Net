namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Common.Models;
    using Enums;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    public class Room:BaseDeletableModel<string>
    {
        public Room()
        {
            Id = Guid.NewGuid().ToString();
            Pictures = new HashSet<Picture>();
        }

        [Required]
        [Range(typeof(decimal), "1", "1000")]
        public decimal Price { get; set; }

        [Required]
        [Range(1,10)]
        public int NumberOfBeds { get; set; }

        public bool HasBathroom { get; set; }

        public bool HasRoomService { get; set; }

        public bool HasSeaView { get; set; }

        public bool HasMountainView { get; set; }

        public bool HasWifi { get; set; }

        public bool HasTv { get; set; }

        public bool HasPhone { get; set; }

        public bool HasAirConditioner { get; set; }

        public bool HasHeater { get; set; }

        [Required] public virtual ICollection<Picture> Pictures { get; set; }

        public RoomType RoomType { get; set; }
    }
}
