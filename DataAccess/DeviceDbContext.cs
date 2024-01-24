using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Device> Devices { get; set; }
    }
}
