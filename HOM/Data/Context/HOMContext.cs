using HOM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HOM.Data.Context
{
    public partial class HOMContext : IdentityDbContext<ApplicationUser>
    {
        public HOMContext()
        {
        }

        public HOMContext(DbContextOptions<HOMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Hostel> Hostels { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;
        public virtual DbSet<RoomMembership> RoomMemberships { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
    }
}
