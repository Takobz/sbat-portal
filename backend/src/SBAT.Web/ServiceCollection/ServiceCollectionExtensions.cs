using AutoMapper;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Core.Services;
using SBAT.Web.Models.Response;

namespace SBAT.Web.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMappings(this IServiceCollection service)
        {
            service.AddSingleton(new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserResponse>();
            }).CreateMapper());
        }
        public static void AddServices(this IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
        }
    }
}