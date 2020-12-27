namespace Hotel.Services.Data.Restaurant
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Common;
    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Mapping;
    using Hotel.Web.ViewModels.InputModels.Restaurant;
    using Microsoft.EntityFrameworkCore;

    public class RestaurantsService : IRestaurantService
    {
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;
        private readonly IDeletableEntityRepository<RestaurantReservation> restaurantReservationRepository;

        public RestaurantsService(
            IDeletableEntityRepository<Restaurant> restaurantRepository,
            IDeletableEntityRepository<RestaurantReservation> restaurantReservationRepository)
        {
            this.restaurantRepository = restaurantRepository;
            this.restaurantReservationRepository = restaurantReservationRepository;
        }

        public async Task<IEnumerable<TViewModel>> GetAllReservationsAsync<TViewModel>(string userId)
        {
            var result = await restaurantReservationRepository
              .All()
              .Where(r => r.IsDeleted != true && r.UserId == userId)
              .To<TViewModel>()
              .ToListAsync();

            var datesBeforeToday = await restaurantReservationRepository
                .All()
                .Where(x => x.EventDate < DateTime.Now && x.CheckOut < DateTime.Now)
                .ToListAsync();

            var restaurants = await restaurantRepository.All().Where(x => x.IsDeleted == false).ToListAsync();

            if (datesBeforeToday != null && datesBeforeToday.Count > 0)
            {
                foreach (var item in datesBeforeToday)
                {
                    restaurantReservationRepository.Delete(item);

                    foreach (var rest in restaurants.Where(x => x.CurrentCapacity != x.MaxCapacity))
                    {
                        rest.CurrentCapacity = rest.MaxCapacity;
                    }
                }
            }

            await restaurantRepository.SaveChangesAsync();
            await restaurantReservationRepository.SaveChangesAsync();

            if (result != null)
            {
                return result;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRestaurantGetAllReservations);
        }

        public async Task<IEnumerable<TViewModel>> GetAllReservationsAsyncForAdmin<TViewModel>()
        {
            var result = await restaurantReservationRepository
              .All()
              .Where(r => r.IsDeleted != true)
              .To<TViewModel>()
              .ToListAsync();

            var datesBeforeToday = await restaurantReservationRepository
                .All()
                .Where(x => x.EventDate < DateTime.Now && x.CheckOut < DateTime.Now)
                .ToListAsync();

            var restaurants = await restaurantRepository.All().Where(x => x.IsDeleted == false).ToListAsync();

            if (datesBeforeToday != null && datesBeforeToday.Count > 0)
            {
                foreach (var item in datesBeforeToday)
                {
                    restaurantReservationRepository.Delete(item);

                    foreach (var rest in restaurants.Where(x => x.CurrentCapacity != x.MaxCapacity))
                    {
                        rest.CurrentCapacity = rest.MaxCapacity;
                    }
                }
            }

            await restaurantRepository.SaveChangesAsync();
            await restaurantReservationRepository.SaveChangesAsync();

            if (result != null)
            {
                return result;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRestaurantGetAllReservationsForAdmin);
        }

        public async Task<bool> ReserveRestaurant(RestaurantInputModel input)
        {
            var restaurant = restaurantRepository.All().FirstOrDefault(x => x.IsDeleted == false);

            if (restaurant != null && input.NumberOfGuests <= restaurant.MaxCapacity && input.EventDate.Day >= DateTime.Now.Day)
            {
                var restaurantReservation = new RestaurantReservation()
                {
                    UserId = input.UserId,
                    RestaurantId = restaurant.Id,
                    NumberOfGuests = input.NumberOfGuests,
                    PhoneNumber = input.PhoneNumber,
                    EventType = input.EventType,
                    EventDate = input.EventDate,
                    CheckIn = input.CheckIn,
                    CheckOut = input.CheckOut,
                    TotalPrice = 0,
                    Message = input.Message,
                };

                var totalHours = (decimal)(restaurantReservation.CheckIn - restaurantReservation.CheckOut).TotalHours;

                var price = Math.Abs(restaurant.Price * totalHours);

                restaurantReservation.TotalPrice = price;

                var allReservationsForDate = restaurantReservationRepository.All().Where(x => x.EventDate == input.EventDate);

                var allReservations = restaurantReservationRepository.All().Select(x => x.EventDate).ToList();

                if (allReservationsForDate.Count() != 0)
                {
                    foreach (var item in allReservationsForDate)
                    {
                        if (restaurantReservation.NumberOfGuests > restaurant.CurrentCapacity)
                        {
                            return false;
                        }
                    }
                }

                if (allReservations.Contains(input.EventDate))
                {
                    restaurant.CurrentCapacity -= input.NumberOfGuests;
                }
                else
                {
                    restaurant.CurrentCapacity = restaurant.MaxCapacity;
                    restaurant.CurrentCapacity -= input.NumberOfGuests;
                }

                await restaurantReservationRepository.AddAsync(restaurantReservation);

                await restaurantReservationRepository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRestaurantReservation);
        }

        public int GetRemainingCapacity()
        => restaurantRepository.All().First().CurrentCapacity;
    }
}
