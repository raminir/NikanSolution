using Models;
using System.Data.Entity;

namespace Infrastructure
{
    public class QueueDbContext : DbContext
    {
        public QueueDbContext() : base("name=QueueManagementConnectionString")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<TicketInRooms> TicketInRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
