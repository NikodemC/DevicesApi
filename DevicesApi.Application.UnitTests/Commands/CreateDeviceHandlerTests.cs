using Application.Abstractions;
using Application.Devices.CommandHandlers;
using Application.Devices.Commands;
using Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DevicesApi.Application.UnitTests.Commands
{
    public class CreateDeviceHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateDevice()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new CreateDeviceHandler(deviceRepository);

            var createDeviceCommand = new CreateDevice { Name = "TestDevice", Brand = "TestBrand" };
            var cancellationToken = CancellationToken.None;

            // Act
            await handler.Handle(createDeviceCommand, cancellationToken);

            // Assert
            await deviceRepository.Received(1).CreateDevice(Arg.Any<Device>(), cancellationToken);
        }

        [Fact]
        public async Task Handle_ShouldReturnCreatedDevice()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new CreateDeviceHandler(deviceRepository);

            var createDeviceCommand = new CreateDevice { Name = "TestDevice", Brand = "TestBrand" };
            var createdDevice = new Device { Id = 1, Name = "TestDevice", Brand = "TestBrand" };

            deviceRepository.CreateDevice(Arg.Any<Device>(), Arg.Any<CancellationToken>()).Returns(createdDevice);

            // Act
            var result = await handler.Handle(createDeviceCommand, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(createdDevice);
        }
    }
}
