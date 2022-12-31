using AutoMapper;
using FluentValidation;
using SBAT.Infrastructure.Identity;
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
                cfg.CreateMap<ApplicationUser, UserResponse>();
                cfg.CreateMap<UserRequest, ApplicationUser>();
                #endregion
            }).CreateMapper());
        }
        
        public static void AddServices(this IServiceCollection service)
        {
            //to be implemented
        }

        public static void AddModelValidations(this IServiceCollection service)
        {
            service.AddTransient<IValidator<UserRequest>, UserRequestValidation>();
        }
    }
}