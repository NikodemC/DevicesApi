using Domain.Models;
using MediatR;

namespace Application.Devices.Queries
{
    public class GetAllDevices : IRequest<ICollection<Device>>
    {
    }
}
