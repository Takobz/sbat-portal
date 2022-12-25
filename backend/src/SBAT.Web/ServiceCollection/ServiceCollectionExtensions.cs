using AutoMapper;
using FluentValidation;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Core.Services;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;
using SBAT.Web.Validations;

namespace SBAT.Web.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMappings(this IServiceCollection service)
        {
            service.AddSingleton(new MapperConfiguration(cfg => {
                #region User
                cfg.CreateMap<User, UserResponse>();
                cfg.CreateMap<UserRequest, User>();
                #endregion
            }).CreateMapper());
        }
        public static void AddServices(this IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
        }

        public static void AddModelValidations(this IServiceCollection service)
        {
            service.AddTransient<IValidator<UserRequest>, UserRequestValidation>();
        }
    }
}