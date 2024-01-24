using Application.Devices.Queries;
using DevicesApi.Dtos;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NSubstitute;
using System.Net;
using System.Net.Http.Json;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Application.Devices.Commands;
using AutoFixture.Xunit2;
using DevicesApi.Api.UnitTests.Helpers;

namespace DevicesApi.Api.UnitTests
{
    public class DevicesApiEndpointTests : IClassFixture<InMemoryDbApplication>
    {
        private readonly InMemoryDbApplication _factory;

        public DevicesApiEndpointTests(InMemoryDbApplication factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetDeviceById_ValidId_ReturnsOk()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<GetDeviceById>()).Returns(new Device() {Id = 1, Name="Name", Brand="Brand"});
            var client = _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(mediator);
            })).CreateClient();

            // Act
            var response = await client.GetAsync("/api/devices/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetDeviceById_NotValidId_ReturnsNotFound()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<GetDeviceById>()).Returns((Device?)null);
            var client = _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(mediator);
            })).CreateClient();

            // Act
            var response = await client.GetAsync("/api/devices/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task GetAllDevices_ReturnsOk(List<Device> devices)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<GetAllDevices>()).Returns(devices);
            var client = _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(mediator);
            })).CreateClient();

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
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<UpdateDevice>()).Returns(new Device() { Id = 1, Name = "Name", Brand = "Brand" });
            var client = _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(mediator);
            })).CreateClient();

            // Act
            var response = await client.PutAsJsonAsync("/api/devices/1", new DeviceDto("TestName", "TestBrand"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateDevice_NoDevice_ReturnsNotFound()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<UpdateDevice>()).Returns((Device?)null);
            var client = _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(mediator);
            })).CreateClient();

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
