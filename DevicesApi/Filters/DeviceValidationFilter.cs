using DevicesApi.Dtos;

namespace DevicesApi.Filters
{
    public class DeviceValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var device = context.GetArgument<DeviceDto>(1);
            if (string.IsNullOrEmpty(device.Name))
                return await Task.FromResult(Results.BadRequest("Name cannot be empty"));
            if(string.IsNullOrEmpty(device.Brand))
                return await Task.FromResult(Results.BadRequest("Brand cannot be empty"));
            return await next(context);
        }
    }
}
