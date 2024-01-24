using Application.Abstractions;
using Application.Devices.CommandHandlers;
using Application.Devices.Commands;
using Domain.Models;
using NSubstitute;
using Xunit;

namespace DevicesApi.Application.UnitTests.Commands
{
    public class UpdateDeviceHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateDevice()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new UpdateDeviceHandler(deviceRepository);

            var updateDeviceCommand = new UpdateDevice { Id = 1, DeviceName = "UpdatedName", DeviceBrand = "UpdatedBrand" };
            var cancellationToken = CancellationToken.None;

            var originalDevice = new Device { Id = 1, Name = "OriginalName", Brand = "OriginalBrand" };
            deviceRepository.GetDeviceById(originalDevice.Id, cancellationToken).Returns(originalDevice);

            // Act
            var result = await handler.Handle(updateDeviceCommand, cancellationToken);

            // Assert
            await deviceRepository.Received(1).UpdateDevice(Arg.Any<Device>(), cancellationToken);
        }

    }
}
