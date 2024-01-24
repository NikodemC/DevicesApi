using Application.Abstractions;
using Application.Devices.Queries;
using Domain.Models;
using MediatR;

namespace Application.Devices.QueryHandlers
{
    public class GetAllDevicesHandler : IRequestHandler<GetAllDevices, ICollection<Device>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetAllDevicesHandler(IDeviceRepository deviceRepository)
            => _deviceRepository = deviceRepository;

        public async Task<ICollection<Device>> Handle(GetAllDevices request, CancellationToken cancellationToken)
            => await _deviceRepository.GetAllDevices(cancellationToken);
    }
}
