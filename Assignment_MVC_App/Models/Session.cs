namespace Assignment_MVC_App.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Session
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Session()
        {
            Bookings = new HashSet<Booking>();
        }

        [Display(Name = "Session ID")]
        public int SessionID { get; set; }

        [Required]
        public string Location { get; set; }

        [Display(Name = "Date")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime SessionDate { get; set; }

        //[DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{h:MM tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time")]
        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:h:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime SessionTime { get; set; }

        [Required]
        [Display(Name = "Details")]
        public string SessionDetails { get; set; }

        public string BookedUser { get; set; }
        
        public string TrainerID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
