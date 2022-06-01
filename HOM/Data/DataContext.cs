using HOM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HOM.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Hostels> Hostels { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Services> Services { get; set; }
    }
}
