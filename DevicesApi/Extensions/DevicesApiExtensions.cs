using Application.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevicesApi.Extensions
{
    public static class DevicesApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            
            builder.AddDbContext();
            builder.AddSwagger();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }

        private static void AddDbContext(this WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("DeviceServiceDb");
            var majorVersion = builder.Configuration.GetValue<int>("DatabaseVersion:Major");
            var minorVersion = builder.Configuration.GetValue<int>("DatabaseVersion:Minor");
            var patchVersion = builder.Configuration.GetValue<int>("DatabaseVersion:Patch");
            var serverVersion = new MySqlServerVersion(new Version(majorVersion, minorVersion, patchVersion));
            builder.Services.AddDbContext<DeviceDbContext>(opt => opt.UseMySql(cs, serverVersion));
        }

        private static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
