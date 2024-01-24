using Domain.Models;
using MediatR;

namespace Application.Devices.Commands
{
    public class UpdateDevice : IRequest<Device?>
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceBrand { get; set; }
    }
}
