using Domain.Models;

namespace Application.Abstractions
{
    public interface IDeviceRepository
    {
        Task<ICollection<Device>> GetAllDevices(CancellationToken cancellationToken);
        Task<Device?> GetDeviceById(int deviceId, CancellationToken cancellationToken);
        Task<ICollection<Device>> GetByBrand(string brand, CancellationToken cancellationToken);
        Task<Device> CreateDevice(Device device, CancellationToken cancellationToken);
        Task<Device?> UpdateDevice(Device device, CancellationToken cancellationToken);
        Task DeleteDevice(int deviceId, CancellationToken cancellationToken);
    }
}
