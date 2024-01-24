using Application.Abstractions;
using Application.Devices.CommandHandlers;
using Application.Devices.Commands;
using NSubstitute;
using Xunit;

namespace DevicesApi.Application.UnitTests.Commands
{
    public class DeleteDeviceHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteDevice()
        {
            // Arrange
            var deviceRepository = Substitute.For<IDeviceRepository>();
            var handler = new DeleteDeviceHandler(deviceRepository);

            var deleteDeviceCommand = new DeleteDevice { Id = 1 };
            var cancellationToken = CancellationToken.None;

            // Act
            await handler.Handle(deleteDeviceCommand, cancellationToken);

            // Assert
            await deviceRepository.Received(1).DeleteDevice(deleteDeviceCommand.Id, cancellationToken);
        }
    }
}
