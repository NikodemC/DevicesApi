using Application.Abstractions;
using Application.Devices.Commands;
using Domain.Models;
using MediatR;

namespace Application.Devices.CommandHandlers
{
    public class CreateDeviceHandler : IRequestHandler<CreateDevice, Device>
    {
        private readonly IDeviceRepository _deviceRepository;

        public CreateDeviceHandler(IDeviceRepository deviceRepository)
            => _deviceRepository = deviceRepository;

        public async Task<Device> Handle(CreateDevice request, CancellationToken cancellationToken)
        {
            var device = new Device() { Name = request.Name, Brand = request.Brand};
            return await _deviceRepository.CreateDevice(device);
        }
    }
}
