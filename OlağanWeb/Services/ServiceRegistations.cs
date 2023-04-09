using WebSite.Application.Abstractions.TokenAbs;


namespace OlağanWeb.Services
{
    public static class ServiceRegistations
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenServices.TokenHandler>();

        }
    }
}
