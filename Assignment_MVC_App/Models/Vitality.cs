namespace Assignment_MVC_App.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Vitality : DbContext
    {
        public Vitality()
            : base("name=Vitality")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Session)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<Assignment_MVC_App.Models.Location> Locations { get; set; }
    }
}
