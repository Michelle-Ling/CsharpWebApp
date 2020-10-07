namespace Assignment_MVC_App.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int BookingID { get; set; }

        public int SessionID { get; set; }

        [Required]
        public string UserID { get; set; }

        public virtual Session Session { get; set; }
    }
}
