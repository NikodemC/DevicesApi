using System.Text.Json;
using Application.Devices.Commands;
using Application.Devices.Queries;
using DevicesApi.Abstractions;
using DevicesApi.Dtos;
using DevicesApi.Filters;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevicesApi.EndpointDefinitions
{
    public class DeviceEndpointDefinitions : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var devices = app.MapGroup("api/devices");

            devices.MapGet("/{id}", GetDeviceById)
                .WithName(nameof(GetDeviceById));

            devices.MapGet("/", GetAllDevices)
                .WithName(nameof(GetAllDevices));

            devices.MapPost("/", CreateDevice)
                .WithName(nameof(CreateDevice))
                .AddEndpointFilter<DeviceValidationFilter>();

            devices.MapPut("/{id}", UpdateDevice)
                .WithName(nameof(UpdateDevice))
                .AddEndpointFilter<DeviceValidationFilter>();

            devices.MapPatch("/{id}", PatchDevice)
                .WithName(nameof(PatchDevice));

            devices.MapDelete("/{id}", DeleteDevice)
                .WithName(nameof(DeleteDevice));
        }

        private async Task<IResult> GetDeviceById(IMediator mediator, int id)
        {
            var device = await mediator.Send(new GetDeviceById() { Id = id });

            return device == null
                ? TypedResults.NotFound($"Device with id {id} not found.")
                : TypedResults.Ok(device);
        }

        private async Task<IResult> GetAllDevices(IMediator mediator, [FromQuery] string? brand)
        {
            var devices = string.IsNullOrEmpty(brand) 
                ? await mediator.Send(new GetAllDevices())
                : await mediator.Send(new GetAllDevicesByBrand() { Brand = brand });

            return TypedResults.Ok(devices);
        }

        private async Task<IResult> CreateDevice(IMediator mediator, DeviceDto deviceDto)
        {
            var createdDevice = await mediator.Send(new CreateDevice() { Name = deviceDto.Name, Brand = deviceDto.Brand});
            return Results.CreatedAtRoute("GetDeviceById", new { createdDevice.Id }, createdDevice);
        }

        private async Task<IResult> UpdateDevice(IMediator mediator, DeviceDto deviceDto, int id)
        {
            var updatedDevice = await mediator.Send(new UpdateDevice() { Id = id, DeviceName = deviceDto.Name, DeviceBrand = deviceDto.Brand});
            
            return updatedDevice == null 
                ? TypedResults.NotFound($"Device with id {id} not found.") 
                : TypedResults.Ok(updatedDevice);
        }

        private async Task<IResult> PatchDevice(IMediator mediator, [FromBody] JsonElement jsonElement, int id)
        {
            var patchDocument = JsonConvert.DeserializeObject<JsonPatchDocument>(jsonElement.GetRawText());
            if (patchDocument == null) 
                return TypedResults.BadRequest($"Failed to deserialize request body to {nameof(JsonPatchDocument)}");

            var device = await mediator.Send(new GetDeviceById() { Id = id });
            if (device == null) 
                return TypedResults.NotFound($"Device with id {id} not found.");
           
            patchDocument.ApplyTo(device);

            await mediator.Send(new UpdateDevice() { Id = id, DeviceName = device.Name, DeviceBrand = device.Brand });

            return TypedResults.Ok(device);
        }

        private async Task<IResult> DeleteDevice(IMediator mediator, int id)
        {
            await mediator.Send(new DeleteDevice() { Id = id });
            return TypedResults.NoContent();
        }
    }
}
