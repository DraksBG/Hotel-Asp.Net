﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Web.ViewModels.InputModels.Restaurant;

namespace Hotel.Services.Data.Restaurant
{
    public interface IRestaurantService
    {
        Task<bool> ReserveRestaurant(RestaurantInputModel input);

        Task<IEnumerable<TViewModel>> GetAllReservationsAsync<TViewModel>(string userId);

        Task<IEnumerable<TViewModel>> GetAllReservationsAsyncForAdmin<TViewModel>();

        int GetRemainingCapacity();
    }
}
