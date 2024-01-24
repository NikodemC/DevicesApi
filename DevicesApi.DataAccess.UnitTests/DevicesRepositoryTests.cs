using Application.Abstractions;
using AutoFixture.Xunit2;
using DataAccess;
using DataAccess.Repositories;
using Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DevicesApi.DataAccess.UnitTests
{
    public class DevicesRepositoryTests
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly CancellationToken _ct = CancellationToken.None;

        public DevicesRepositoryTests()
        {
            DbContextOptionsBuilder<DeviceDbContext> options = new();
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _deviceRepository = new DeviceRepository(new DeviceDbContext(options.Options));
        }

        [Theory]
        [AutoData]
        public async Task CreateDevice_ShouldPopulateId(Device device)
        {
            await _deviceRepository.CreateDevice(device, _ct);

            device.Id.Should().NotBe(0);
        }

        [Theory]
        [AutoData]
        public async Task CreateDevice_ShouldPopulateCreationTime(Device device)
        {
            // Arrange
            device.CreationTime = default;

            //Act
            var startTime = DateTime.Now;
            await _deviceRepository.CreateDevice(device, _ct);

            //Assert
            device.CreationTime.Should().BeAfter(startTime).And.Subject.Should().BeBefore(DateTime.Now);
        }

        [Fact]
        public async Task GetAllDevices_ShouldReturnListOfDevices()
        {
            // Arrange
            var devices = new List<Device>
            {
                new() { Id = 1, Name = "Device1", Brand = "Brand1" },
                new() { Id = 2, Name = "Device2", Brand = "Brand2" },
            };

            await _deviceRepository.CreateDevice(devices[0], _ct);
            await _deviceRepository.CreateDevice(devices[1], _ct);

            // Act
            var result = await _deviceRepository.GetAllDevices(_ct);

            // Assert
            result.Should().BeEquivalentTo(devices);
        }

        [Fact]
        public async Task GetDeviceById_WithValidId_ShouldReturnDevice()
        {
            // Arrange
            var deviceId = 1;
            var device = new Device { Id = deviceId, Name = "Device1", Brand = "Brand1" };

            await _deviceRepository.CreateDevice(device, _ct);

            // Act
            var result = await _deviceRepository.GetDeviceById(deviceId, _ct);

            // Assert
            result.Should().BeEquivalentTo(device);
        }

        [Fact]
        public async Task GetByBrand_WithValidBrand_ShouldReturnDevices()
        {
            // Arrange
            var devices = new List<Device>
            {
                new() { Id = 1, Name = "Device1", Brand = "Brand1" },
                new() { Id = 2, Name = "Device2", Brand = "Brand1" },
            };

            await _deviceRepository.CreateDevice(devices[0], _ct);
            await _deviceRepository.CreateDevice(devices[1], _ct);

            // Act
            var result = await _deviceRepository.GetByBrand("Brand1", _ct);

            // Assert
            result.Should().BeEquivalentTo(devices);
        }

        [Fact]
        public async Task UpdateDevice_WithValidDevice_ShouldUpdateDevice()
        {
            // Arrange
            var originalDevice = new Device { Id = 1, Name = "OriginalDevice", Brand = "Brand1" };
            await _deviceRepository.CreateDevice(originalDevice, _ct);

            var updatedDevice = new Device { Id = 1, Name = "UpdatedDevice", Brand = "Brand2" };

            // Act
            await _deviceRepository.UpdateDevice(updatedDevice, _ct);

            // Assert
            var result = await _deviceRepository.GetDeviceById(1, _ct);
            result.Name.Should().BeEquivalentTo(updatedDevice.Name);
            result.Brand.Should().BeEquivalentTo(updatedDevice.Brand);
        }

        [Fact]
        public async Task DeleteDevice_WithValidId_ShouldDeleteDevice()
        {
            // Arrange
            var device = new Device { Id = 1, Name = "DeviceToDelete", Brand = "Brand1" };
            await _deviceRepository.CreateDevice(device, _ct);

            // Act
            await _deviceRepository.DeleteDevice(1, _ct);

            // Assert
            var result = await _deviceRepository.GetDeviceById(1, _ct);
            result.Should().BeNull();
        }
    }
}
