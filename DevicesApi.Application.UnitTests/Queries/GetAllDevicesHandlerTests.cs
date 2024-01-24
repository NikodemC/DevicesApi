using Application.Abstractions;
using Application.Devices.Queries;
using Application.Devices.QueryHandlers;
using Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DevicesApi.Application.UnitTests.Queries
{
    public class GetAllDevicesHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllDevices()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new GetAllDevicesHandler(deviceRepository);

            var getAllDevicesQuery = new GetAllDevices();
            var cancellationToken = CancellationToken.None;

            var expectedDevices = new List<Device>
            {
                new() { Id = 1, Name = "Device1", Brand = "Brand1" },
                new() { Id = 2, Name = "Device2", Brand = "Brand2" }
            };

            deviceRepository.GetAllDevices(cancellationToken).Returns(expectedDevices);

            // Act
            var result = await handler.Handle(getAllDevicesQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(expectedDevices);
        }
    }
}
