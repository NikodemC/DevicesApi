using MediatR;

namespace Application.Devices.Commands
{
    public class DeleteDevice : IRequest
    {
        public int Id { get; set; }
    }
}
