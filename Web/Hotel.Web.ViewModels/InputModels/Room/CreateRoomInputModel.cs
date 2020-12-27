namespace Hotel.Web.ViewModels.InputModels.Room
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Http;

    public class CreateRoomInputModel
    {
        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        public RoomType RoomType { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Range(typeof(decimal), "1", "1000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Range(1, 10, ErrorMessage = GlobalConstants.NumberOfBedsRange)]
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

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [DataType(DataType.Upload)]
        public ICollection<IFormFile> Pictures { get; set; }
    }
}
