using Domain.Models;
using MediatR;

namespace Application.Devices.Queries
{
    public class GetDeviceById : IRequest<Device?>
    {
        public int Id { get; set; }
    }
}
