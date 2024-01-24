using Application.Abstractions;
using Application.Devices.Queries;
using Domain.Models;
using MediatR;

namespace Application.Devices.QueryHandlers
{
    public class GetDeviceByIdHandler : IRequestHandler<GetDeviceById, Device?>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDeviceByIdHandler(IDeviceRepository deviceRepository)
            => _deviceRepository = deviceRepository;

        public async Task<Device?> Handle(GetDeviceById request, CancellationToken cancellationToken) 
            => await _deviceRepository.GetDeviceById(request.Id);
    }
}
