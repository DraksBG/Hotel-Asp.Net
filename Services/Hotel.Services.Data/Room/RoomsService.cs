namespace Hotel.Services.Data.Room
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Common;
    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Cloudinary;
    using Picture;
    using Mapping;
    using Hotel.Web.ViewModels.InputModels.Room;
    using Web.ViewModels.RoomViewModels;
    using Microsoft.EntityFrameworkCore;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> repository;
        private readonly IDeletableEntityRepository<RoomReservation> roomReservationRepository;
        private readonly ICloudinaryService cloudinaryService;

        public RoomsService(
            IDeletableEntityRepository<Room> repository,
            IDeletableEntityRepository<RoomReservation> roomReservationRepository,
            ICloudinaryService cloudinaryService)
        {
            this.repository = repository;
            this.roomReservationRepository = roomReservationRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<bool> CreateRoomAsync(CreateRoomInputModel input)
        {
            string folderName = "room_images";

            var pictureUrls = input.Pictures
                .Select(async x =>
                    await cloudinaryService.UploadPhotoAsync(x, x.FileName, folderName))
                .Select(
                    x => 
                    x.Result)
                .ToList();

            var room = new Room
            {
                RoomType = input.RoomType,
                Price = input.Price,
                NumberOfBeds = input.NumberOfBeds,
                HasBathroom = input.HasBathroom,
                HasRoomService = input.HasRoomService,
                HasSeaView = input.HasSeaView,
                HasMountainView = input.HasMountainView,
                HasWifi = input.HasWifi,
                HasTv = input.HasTv,
                HasPhone = input.HasPhone,
                HasAirConditioner = input.HasAirConditioner,
                HasHeater = input.HasHeater,
                Pictures = pictureUrls.Select(x => new Picture { Url = x })
                .ToList(),
            };

            if (room != null && room.Price > 0)
            {
                await repository.AddAsync(room);
                var result = await repository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRoomCreate);
        }

        public async Task<bool> DeleteRoom(string id)
        {
            var room = repository
                .All()
                .FirstOrDefault(r => r.Id == id);

            if (room != null)
            {
                repository
                    .Delete(room);

                int result = await repository.SaveChangesAsync();
                return result > 0;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRoomDelete);
        }

        public async Task<bool> EditRoomAsync(string id, EditRoomViewModel input)
        {
            var currentRoom = GetRoomById(id);

            if (currentRoom != null)
            {
                currentRoom.RoomType = input.RoomType;
                currentRoom.Price = input.Price;
                currentRoom.NumberOfBeds = input.NumberOfBeds;
                currentRoom.HasWifi = input.HasWifi;
                currentRoom.HasAirConditioner = input.HasAirConditioner;
                currentRoom.HasBathroom = input.HasBathroom;
                currentRoom.HasHeater = input.HasHeater;
                currentRoom.HasMountainView = input.HasMountainView;
                currentRoom.HasPhone = input.HasPhone;
                currentRoom.HasRoomService = input.HasRoomService;
                currentRoom.HasSeaView = input.HasSeaView;
                currentRoom.HasTv = input.HasTv;

                repository.Update(currentRoom);

                await repository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRoomEdit);
        }

        public async Task<IEnumerable<TViewModel>> GetAllReservationsAsync<TViewModel>(string userId)
        {
            var reservations = await roomReservationRepository
            .All()
            .Where(r => r.IsDeleted == false
            && r.UserId == userId
            && r.CheckOut > DateTime.Now) // UtcNow
            .To<TViewModel>()
            .ToListAsync();

            var reservationCheckOut = await roomReservationRepository.All().Where(x => x.CheckOut < DateTime.Now).ToListAsync();

            if (reservationCheckOut.Any())
            {
                foreach (var res in reservationCheckOut)
                {
                    roomReservationRepository.Delete(res);

                    await roomReservationRepository.SaveChangesAsync();
                }
            }

            if (reservations != null)
            {
                return reservations;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRoomGetAllReservations);
        }

        public async Task<IEnumerable<TViewModel>> GetAllReservationsAsyncForAdmin<TViewModel>()
        {
            var reservations = await roomReservationRepository
            .All()
            .Where(r => r.IsDeleted == false && r.CheckOut > DateTime.Now) // UtcNow
            .To<TViewModel>()
            .ToListAsync();

            var reservationCheckOut = await roomReservationRepository.All().Where(x => x.CheckOut < DateTime.Now).ToListAsync();

            if (reservationCheckOut.Any())
            {
                foreach (var res in reservationCheckOut)
                {
                    roomReservationRepository.Delete(res);

                    await roomReservationRepository.SaveChangesAsync();
                }
            }

            if (reservations != null)
            {
                return reservations;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRoomGetAllReservationsForAdmin);
        }

        public async Task<IEnumerable<TViewModel>> GetAllRoomsAsync<TViewModel>()
            => await repository
            .All()
            .Where(r => r.IsDeleted != true)
            .OrderBy(p => p.Price)
            .To<TViewModel>()
            .ToListAsync();

        public async Task<TViewModel> GetRoomAsync<TViewModel>(string id)
            => await repository
            .All()
            .Where(r => r.Id == id && r.IsDeleted != true)
            .To<TViewModel>()
            .FirstOrDefaultAsync();

        public async Task<EditRoomViewModel> GetRoomForEditAsync(string id)
        {
            var room = GetRoomById(id);

            if (room != null)
            {
                var result = new EditRoomViewModel()
                {
                    RoomType = room.RoomType,
                    Price = room.Price,
                    NumberOfBeds = room.NumberOfBeds,
                    HasWifi = room.HasWifi,
                    HasAirConditioner = room.HasAirConditioner,
                    HasBathroom = room.HasBathroom,
                    HasHeater = room.HasHeater,
                    HasMountainView = room.HasMountainView,
                    HasPhone = room.HasPhone,
                    HasRoomService = room.HasRoomService,
                    HasSeaView = room.HasSeaView,
                    HasTv = room.HasTv,
                };

                return result;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRoomSearchForEdit);
        }

        public async Task<bool> ReserveRoom(ReserveRoomInputModel input)
        {
            var room = repository.All()
                .FirstOrDefault(r => r.Id == input.RoomId);

            if (room != null && input.CountOfPeople <= room.NumberOfBeds)
            {
                var reservation = new RoomReservation()
                {
                    UserId = input.UserId,
                    RoomId = room.Id,
                    RoomType = room.RoomType,
                    PhoneNumber = input.PhoneNumber,
                    CheckIn = input.CheckIn,
                    CheckOut = input.CheckOut,
                    TotalPrice = 0,
                    NumberOfGuests = input.CountOfPeople,
                    NumberOfNights = 0,
                };

                var days = (reservation.CheckOut - reservation.CheckIn).TotalDays;

                var totalPrice = ((decimal)days * room.Price) * reservation.NumberOfGuests;

                reservation.NumberOfNights = (int)days;

                reservation.TotalPrice = totalPrice;

                await roomReservationRepository.AddAsync(reservation);

                int result = await roomReservationRepository.SaveChangesAsync();

                if (reservation.NumberOfNights <= 0)
                {
                    return false;
                }

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionForRoomReservation);
        }

        public Room GetRoomById(string id)
            => repository.All()?.FirstOrDefault(x => x.Id == id);
    }
}
