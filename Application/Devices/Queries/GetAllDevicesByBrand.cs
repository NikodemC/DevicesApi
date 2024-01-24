using Domain.Models;
using MediatR;

namespace Application.Devices.Queries
{
    public class GetAllDevicesByBrand : IRequest<ICollection<Device>>
    {
        public string Brand { get; set; }
    }
}
