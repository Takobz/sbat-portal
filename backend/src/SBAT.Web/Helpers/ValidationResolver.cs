using FluentValidation;

namespace SBAT.Web.Helpers
{
    public interface IValidationResolver
    {
        public IValidator<T> GetValidator<T>();
    }

    public class ValidationResolver : IValidationResolver
    {
        private readonly IServiceProvider _serviceProvider;
        public ValidationResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IValidator<T> GetValidator<T>()
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if(validator == null)
            {
                throw new ArgumentException(nameof(IValidator<T>));
            }

            return validator;
        }
    }
}