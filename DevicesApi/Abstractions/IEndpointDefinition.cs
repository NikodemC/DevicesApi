namespace DevicesApi.Abstractions
{
    public interface IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app);
    }
}
