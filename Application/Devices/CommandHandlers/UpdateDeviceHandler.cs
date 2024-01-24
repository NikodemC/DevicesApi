using Application.Abstractions;
using Application.Devices.Commands;
using Domain.Models;
using MediatR;

namespace Application.Devices.CommandHandlers
{
    public class UpdateDeviceHandler : IRequestHandler<UpdateDevice, Device?>
    {
        private readonly IDeviceRepository _deviceRepository;

        public UpdateDeviceHandler(IDeviceRepository deviceRepository) 
            => _deviceRepository = deviceRepository;

        public async Task<Device?> Handle(UpdateDevice request, CancellationToken cancellationToken)
        {
            var device = new Device() { Id = request.Id, Brand = request.DeviceBrand, Name = request.DeviceName };
            var updatedDevice = await _deviceRepository.UpdateDevice(device);
            return updatedDevice;
        }
    }
}
