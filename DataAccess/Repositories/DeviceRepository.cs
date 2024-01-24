using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceDbContext _context;

        public DeviceRepository(DeviceDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Device>> GetAllDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task<Device?> GetDeviceById(int deviceId)
        {
            return await _context.Devices.FirstOrDefaultAsync(p => p.Id == deviceId);
        }

        public async Task<ICollection<Device>> GetByBrand(string brand)
        {
            return await _context.Devices.Where(d => d.Brand.Equals(brand, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }

        public async Task<Device> CreateDevice(Device device)
        {
            device.CreationTime = DateTime.Now;
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<Device?> UpdateDevice(Device device)
        {
            var deviceToUpdate = await _context.Devices.FirstOrDefaultAsync(p => p.Id == device.Id);

            if (deviceToUpdate != null)
            {
                deviceToUpdate.Brand = device.Brand;
                deviceToUpdate.Name = device.Name;
                await _context.SaveChangesAsync();
            }
            return deviceToUpdate;
        }

        public async Task DeleteDevice(int deviceId)
        {
            var deviceToDelete = await _context.Devices.FirstOrDefaultAsync(p => p.Id == deviceId);

            if (deviceToDelete != null)
            {
                _context.Devices.Remove(deviceToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
