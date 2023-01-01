using FluentValidation;

namespace SBAT.Web.Helpers
{
    public static class IValidatorExtensions
    {
        public static List<string> ValidateModelAndGetErrorMessages<T>(this IValidator<T> validator, T model)
        {
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return errors;
            }
            
            return new List<string>();
        }
    }
}