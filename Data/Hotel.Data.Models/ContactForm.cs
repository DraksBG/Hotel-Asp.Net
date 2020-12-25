using Hotel.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hotel.Data.Models
{
    public class ContactForm : BaseDeletableModel<string>
    {
        public ContactForm()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }
    }
}
