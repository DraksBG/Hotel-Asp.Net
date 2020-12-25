﻿using System;
using System.Collections.Generic;
using System.Text;
using Hotel.Data.Models;
using Hotel.Services.Mapping;

namespace Hotel.Web.ViewModels.Restaurant
{
    public class RestaurantAllViewModel : IMapFrom<RestaurantReservation>
    {
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
