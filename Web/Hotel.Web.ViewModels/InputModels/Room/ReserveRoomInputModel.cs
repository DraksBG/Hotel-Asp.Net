namespace Hotel.Web.ViewModels.InputModels.Room
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Common;
    using Services.Mapping;
    using Infrastructure;

    public class ReserveRoomInputModel : IHaveCustomMappings
    {
        [MaxLength(20, ErrorMessage = GlobalConstants.UserNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Range(1, 10, ErrorMessage = GlobalConstants.CountOfPeopleInRoomLength)]
        public int CountOfPeople { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [DataType(DataType.Date)]
        [DateCheckAnnotation(ErrorMessage = GlobalConstants.CheckDateTimeAttribute)]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [DataType(DataType.Date)]
        [DateCheckAnnotation(ErrorMessage = GlobalConstants.CheckDateTimeAttribute)]
        public DateTime CheckOut { get; set; }

        [MaxLength(300, ErrorMessage = GlobalConstants.ContentMessageMaxLength)]
        public string Message { get; set; }

        public string RoomId { get; set; }

        public string UserId { get; set; }

        public ICollection<string> Pictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            _ = configuration.CreateMap<Data.Models.Room, ReserveRoomInputModel>()
            .ForMember(x => x.Pictures, cfg => cfg.MapFrom(x => x.Pictures.Select(pic => pic.Url)));
        }
    }
}
