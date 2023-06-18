using SBAT.App.Models.Settings;
using SBAT.App.Services;

namespace SBAT.App.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSBATOptions(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<SbatApiOptions>(configuration.GetSection(SbatApiOptions.SbatApiOption));

            return services;
        }

        public static IServiceCollection AddSBATAppServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddTransient<ISBATApiService, SBATApiService>();
            return services;
        }
    }
}
