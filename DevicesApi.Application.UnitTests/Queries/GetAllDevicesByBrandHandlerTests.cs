using Application.Abstractions;
using Application.Devices.Queries;
using Application.Devices.QueryHandlers;
using Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DevicesApi.Application.UnitTests.Queries
{
    public class GetAllDevicesByBrandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnDevicesByBrand()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new GetAllDevicesByBrandHandler(deviceRepository);

            var getAllDevicesByBrandQuery = new GetAllDevicesByBrand { Brand = "TestBrand" };
            var cancellationToken = CancellationToken.None;

            var expectedDevices = new List<Device>
            {
                new() { Id = 1, Name = "Device1", Brand = "TestBrand" },
                new() { Id = 2, Name = "Device2", Brand = "TestBrand" }
            };

            deviceRepository.GetByBrand(getAllDevicesByBrandQuery.Brand, cancellationToken).Returns(expectedDevices);

            // Act
            var result = await handler.Handle(getAllDevicesByBrandQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(expectedDevices);
        }
    }
}
