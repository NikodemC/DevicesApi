using Application.Abstractions;
using Application.Devices.Queries;
using Application.Devices.QueryHandlers;
using Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DevicesApi.Application.UnitTests.Queries
{
    public class GetDeviceByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnDeviceById()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new GetDeviceByIdHandler(deviceRepository);

            var getDeviceByIdQuery = new GetDeviceById { Id = 1 };
            var cancellationToken = CancellationToken.None;

            var expectedDevice = new Device { Id = 1, Name = "TestDevice", Brand = "TestBrand" };
            deviceRepository.GetDeviceById(getDeviceByIdQuery.Id, cancellationToken).Returns(expectedDevice);

            // Act
            var result = await handler.Handle(getDeviceByIdQuery, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(expectedDevice);
        }

        [Fact]
        public async Task Handle_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new GetDeviceByIdHandler(deviceRepository);

            var getDeviceByIdQuery = new GetDeviceById { Id = 2 };
            var cancellationToken = CancellationToken.None;

            deviceRepository.GetDeviceById(getDeviceByIdQuery.Id, cancellationToken).Returns((Device?)null);

            // Act
            var result = await handler.Handle(getDeviceByIdQuery, cancellationToken);

            // Assert
            result.Should().BeNull();
        }
    }
}
