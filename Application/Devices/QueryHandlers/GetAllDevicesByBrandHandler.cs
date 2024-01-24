using Application.Abstractions;
using Application.Devices.Queries;
using Domain.Models;
using MediatR;

namespace Application.Devices.QueryHandlers
{
    public class GetAllDevicesByBrandHandler : IRequestHandler<GetAllDevicesByBrand, ICollection<Device>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetAllDevicesByBrandHandler(IDeviceRepository deviceRepository)
            => _deviceRepository = deviceRepository;

        public async Task<ICollection<Device>> Handle(GetAllDevicesByBrand request, CancellationToken cancellationToken) 
            => await _deviceRepository.GetByBrand(request.Brand);
    }
}
