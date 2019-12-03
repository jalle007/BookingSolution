using Microsoft.EntityFrameworkCore;

namespace BookingApi.Models
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CapacityProvider> CapacityProviders { get; set; }

    }
}
