using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using SBAT.Web.Models;

namespace SBAT.Web.SBATValidation 
{
    public class SBATValidationAttribute<T> : Attribute, IAsyncActionFilter where T : BaseDTO
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach(var argument in context.ActionArguments.Values.Where(v => v is BaseDTO))
            {
                var model  = argument as T;
                var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();

                var validationResult = validator.Validate(model ?? throw new ArgumentNullException(nameof(model)));
                if (!validationResult.IsValid)
                {
                    var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                    throw new ValidationException(string.Join("/n", validationErrors));
                }
            }

            await next();
        }
    }
}