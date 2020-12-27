namespace Hotel.Web.ViewModels.InputModels.ConferenceHall
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Models.Enums;
    using Infrastructure;

    public class ConferenceHallInputModel
    {
        public string UserId { get; set; }

        [EmailAddress(ErrorMessage = GlobalConstants.InvalidEmail)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Phone(ErrorMessage = GlobalConstants.InvalidPhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [Range(1, 100, ErrorMessage = GlobalConstants.ConfHallReserveGuestsMax)]
        public int NumberOfGuests { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        public ConfHallEventType EventType { get; set; }

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
    }
}
