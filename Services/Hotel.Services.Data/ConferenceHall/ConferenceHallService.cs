namespace Hotel.Services.Data.ConferenceHall
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Common;
    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Mapping;
    using Hotel.Web.ViewModels.InputModels.ConferenceHall;
    using Microsoft.EntityFrameworkCore;

    public class ConferenceHallService : IConferenceHallService
    {
        private readonly IDeletableEntityRepository<ConferenceHallReservation> conferenceHallReservationRepository;
        private readonly IDeletableEntityRepository<ConferenceHall> conferenceHallRepository;

        public ConferenceHallService(
            IDeletableEntityRepository<ConferenceHallReservation> conferenceHallReservationRepository,
            IDeletableEntityRepository<ConferenceHall> conferenceHallRepository)
        {
            this.conferenceHallReservationRepository = conferenceHallReservationRepository;
            this.conferenceHallRepository = conferenceHallRepository;
        }

        public async Task<int> GetAllHallsAsync<TViewModel>(ConferenceHallInputModel input)
        {
            var conferenceHall = await conferenceHallRepository.All()
                .Where(x => x.IsDeleted == false && x.EventType == input.EventType)
                .FirstOrDefaultAsync();

            return conferenceHall != null ? conferenceHall.CurrentCapacity : 0;
        }

        public async Task<IEnumerable<TViewModel>> GetAllReservationsAsync<TViewModel>(string userId)
        {
            var result = await conferenceHallReservationRepository
              .All()
              .Where(r => r.IsDeleted != true && r.UserId == userId)
              .To<TViewModel>()
              .ToListAsync();

            var eventDate = await conferenceHallReservationRepository
                .All()
                .Where(x => x.EventDate < DateTime.Now && x.CheckOut < DateTime.Now)
                .ToListAsync();

            var conferenceHalls = await conferenceHallRepository.All().Where(x => x.IsDeleted == false).ToListAsync();

            if (eventDate != null && eventDate.Count > 0)
            {
                foreach (var item in eventDate)
                {
                    conferenceHallReservationRepository.Delete(item);

                    foreach (var hall in conferenceHalls.Where(x => x.CurrentCapacity != x.MaxCapacity))
                    {
                        hall.CurrentCapacity = hall.MaxCapacity;
                    }
                }
            }

            await conferenceHallRepository.SaveChangesAsync();
            await conferenceHallReservationRepository.SaveChangesAsync();

            if (result != null)
            {
                return result;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForConferenceHallGetAllReservations);
        }

        public async Task<IEnumerable<TViewModel>> GetAllReservationsAsyncForAdmin<TViewModel>()
        {
            var result = await conferenceHallReservationRepository
              .All()
              .Where(r => r.IsDeleted != true)
              .To<TViewModel>()
              .ToListAsync();

            var eventDate = await conferenceHallReservationRepository
                .All()
                .Where(x => x.EventDate < DateTime.Now && x.CheckOut < DateTime.Now)
                .ToListAsync();

            var conferenceHalls = await conferenceHallRepository.All().Where(x => x.IsDeleted == false).ToListAsync();

            if (eventDate != null && eventDate.Count > 0)
            {
                foreach (var item in eventDate)
                {
                    conferenceHallReservationRepository.Delete(item);

                    foreach (var hall in conferenceHalls.Where(x => x.CurrentCapacity != x.MaxCapacity))
                    {
                        hall.CurrentCapacity = hall.MaxCapacity;
                    }
                }
            }

            await conferenceHallRepository.SaveChangesAsync();
            await conferenceHallReservationRepository.SaveChangesAsync();

            if (result != null)
            {
                return result;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForConferenceHallGetAllReservationsForAdmin);
        }

        public async Task<bool> ReserveConferenceHall(ConferenceHallInputModel input)
        {
            var conferenceHall = conferenceHallRepository.All()
                .Where(x => x.IsDeleted == false && x.EventType == input.EventType)
                .FirstOrDefault();

            if (conferenceHall != null &&
                input.NumberOfGuests <= conferenceHall.MaxCapacity &&
                input.EventDate.Day >= DateTime.Now.Day)
            {
                var conferenceHallReservation = new ConferenceHallReservation()
                {
                    UserId = input.UserId,
                    PhoneNumber = input.PhoneNumber,
                    NumberOfGuests = input.NumberOfGuests,
                    TotalPrice = 0,
                    EventType = input.EventType,
                    EventDate = input.EventDate,
                    CheckIn = input.CheckIn,
                    CheckOut = input.CheckOut,
                    Message = input.Message,
                    ConferenceHallId = conferenceHall.Id,
                };

                var totalHours = (decimal)(conferenceHallReservation.CheckIn - conferenceHallReservation.CheckOut).TotalHours;

                var price = Math.Abs(conferenceHall.Price * totalHours);

                conferenceHallReservation.TotalPrice = price;

                var allReservationsForDateForHall = conferenceHallReservationRepository
                    .All()
                    .Where(x => x.EventDate == input.EventDate && x.EventType == input.EventType);

                var allReservations = conferenceHallReservationRepository.All().Select(x => x.EventDate).ToList();
                var allReservationsForType = conferenceHallReservationRepository
                    .All().Where(x => x.EventType == input.EventType).ToList();

                if (allReservationsForDateForHall.Count() != 0)
                {
                    foreach (var item in allReservationsForDateForHall)
                    {
                        if (conferenceHallReservation.NumberOfGuests > conferenceHall.CurrentCapacity)
                        {
                            return false;
                        }
                    }
                }

                if (allReservations.Contains(input.EventDate) && allReservationsForType.Count > 0)
                {
                    conferenceHall.CurrentCapacity -= input.NumberOfGuests;
                }
                else
                {
                    conferenceHall.CurrentCapacity = conferenceHall.MaxCapacity;
                    conferenceHall.CurrentCapacity -= input.NumberOfGuests;
                }

                await conferenceHallReservationRepository.AddAsync(conferenceHallReservation);

                await conferenceHallReservationRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
