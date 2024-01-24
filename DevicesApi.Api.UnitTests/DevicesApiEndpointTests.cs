using Application.Devices.Commands;
using Application.Devices.Queries;
using AutoFixture.Xunit2;
using DevicesApi.Api.UnitTests.Helpers;
using DevicesApi.Dtos;
using Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace DevicesApi.Api.UnitTests
{
    public class DevicesApiEndpointTests : IClassFixture<InMemoryDbApplication>
    {
        private readonly InMemoryDbApplication _factory;
        private readonly IMediator _mediator;

        public DevicesApiEndpointTests(InMemoryDbApplication factory)
        {
            _factory = factory;
            _mediator = Substitute.For<IMediator>();
        }

        private HttpClient CreateClientWithMediator()
        {
            return _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(_mediator);
            })).CreateClient();
        }

        [Fact]
        public async Task GetDeviceById_ValidId_ReturnsOk()
        {
            // Arrange
            _mediator.Send(Arg.Any<GetDeviceById>()).Returns(new Device { Id = 1, Name = "Name", Brand = "Brand" });
            var client = CreateClientWithMediator();

            // Act
            var response = await client.GetAsync("/api/devices/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDeviceById_NotValidId_ReturnsNotFound()
        {
            // Arrange
            _mediator.Send(Arg.Any<GetDeviceById>()).Returns((Device?)null);
            var client = CreateClientWithMediator();

            // Act
            var response = await client.GetAsync("/api/devices/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task GetAllDevices_ReturnsOk(List<Device> devices)
        {
            // Arrange
            _mediator.Send(Arg.Any<GetAllDevices>()).Returns(devices);
            var client = CreateClientWithMediator();

            // Act
            var response = await client.GetAsync("/api/devices");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateDevice_ValidDeviceDto_ReturnsCreated()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/api/devices", new DeviceDto("TestName", "TestBrand"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task UpdateDevice_ValidDeviceDto_ReturnsOk()
        {
            // Arrange
            _mediator.Send(Arg.Any<UpdateDevice>()).Returns(new Device { Id = 1, Name = "Name", Brand = "Brand" });
            var client = CreateClientWithMediator();

            // Act
            var response = await client.PutAsJsonAsync("/api/devices/1", new DeviceDto("TestName", "TestBrand"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateDevice_NoDevice_ReturnsNotFound()
        {
            // Arrange
            _mediator.Send(Arg.Any<UpdateDevice>()).Returns((Device?)null);
            var client = CreateClientWithMediator();

            // Act
            var response = await client.PutAsJsonAsync("/api/devices/1", new DeviceDto("TestName", "TestBrand"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteDevice_ValidId_ReturnsNoContent()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/devices/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
