using FluentValidation;
using SBAT.Web.Models.Request;

namespace SBAT.Web.Validations
{
    public class UserRequestValidation : AbstractValidator<UserRequest>
    {
        public UserRequestValidation()
        {
            RuleFor(ur => ur).Must(HaveValidNames);
            RuleFor(ur => ur.Identity).IsInEnum();
            RuleFor(ur => ur.DateOfBirth).GreaterThan(DateTime.MinValue);
            RuleFor(ur => ur.IdentityNumber).NotEmpty();
        }

        private bool HaveValidNames(UserRequest userRequest)
        {
            return !string.IsNullOrEmpty(userRequest.FirstNames)
                && !string.IsNullOrEmpty(userRequest.Surname);
        }
    }
}