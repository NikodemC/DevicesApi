using Domain.Models;
using MediatR;

namespace Application.Devices.Commands
{
    public class CreateDevice : IRequest<Device>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
