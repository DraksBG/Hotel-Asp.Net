namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Common.Models;

   public class Picture : BaseDeletableModel<string>
    {
        public Picture()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        [Required]
        public string Url { get; set; }

        public string RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
