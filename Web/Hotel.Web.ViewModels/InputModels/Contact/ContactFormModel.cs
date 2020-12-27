namespace Hotel.Web.ViewModels.InputModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class ContactFormModel
    {
        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [MaxLength(20, ErrorMessage = GlobalConstants.UserNameMaxLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [MaxLength(20, ErrorMessage = GlobalConstants.UserNameMaxLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [EmailAddress(ErrorMessage = GlobalConstants.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [MaxLength(30, ErrorMessage = GlobalConstants.ContactFormTitleMaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredField)]
        [MaxLength(300, ErrorMessage = GlobalConstants.ContentMessageMaxLength)]
        public string Content { get; set; }
    }
}
