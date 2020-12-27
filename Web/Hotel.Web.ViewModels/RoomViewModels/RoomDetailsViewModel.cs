namespace Hotel.Web.ViewModels.RoomViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class RoomDetailsViewModel : IMapFrom<Room>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string RoomType { get; set; }

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

        public ICollection<string> Pictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Room, RoomDetailsViewModel>()
            .ForMember(x => x.Pictures, cfg => cfg.MapFrom(x => x.Pictures.Select(pic => pic.Url)));
        }
    }
}
