using Domain.Models;

namespace Application.Abstractions
{
    public interface IDeviceRepository
    {
        Task<ICollection<Device>> GetAllDevices();
        Task<Device?> GetDeviceById(int deviceId);
        Task<ICollection<Device>> GetByBrand(string brand);
        Task<Device> CreateDevice(Device device);
        Task<Device?> UpdateDevice(Device device);
        Task DeleteDevice(int deviceId);
    }
}
