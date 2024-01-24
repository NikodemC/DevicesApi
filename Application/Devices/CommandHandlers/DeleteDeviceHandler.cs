using Application.Abstractions;
using Application.Devices.Commands;
using MediatR;

namespace Application.Devices.CommandHandlers
{
    public class DeleteDeviceHandler : IRequestHandler<DeleteDevice>
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeleteDeviceHandler(IDeviceRepository deviceRepository) 
            => _deviceRepository = deviceRepository;

        public async Task Handle(DeleteDevice request, CancellationToken cancellationToken) 
            => await _deviceRepository.DeleteDevice(request.Id, cancellationToken);
    }
}
