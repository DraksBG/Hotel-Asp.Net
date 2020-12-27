namespace Hotel.Web.ViewModels.InputModels.Restaurant
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Models.Enums;
    using Services.Mapping;
    using Infrastructure;

    public class RestaurantInputModel : IMapFrom<Hotel.Data.Models.Restaurant>
    {
        [MaxLength(20, ErrorMessage = GlobalConstants.UserNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(20, ErrorMessage = GlobalConstants.UserNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.InvalidEmail)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Phone(ErrorMessage = GlobalConstants.InvalidPhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Range(1, 100, ErrorMessage = GlobalConstants.RestaurantReserveGuestsMax)]
        public int NumberOfGuests { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        public RestaurantEventType EventType { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [DataType(DataType.Date)]
        [DateCheckAnnotation(ErrorMessage = GlobalConstants.CheckDateTimeAttribute)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [DataType(DataType.Time)]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [DataType(DataType.Time)]
        public DateTime CheckOut { get; set; }

        [MaxLength(300, ErrorMessage = GlobalConstants.ContentMessageMaxLength)]
        public string Message { get; set; }

        public string UserId { get; set; }
    }
}
