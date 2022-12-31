using System.ComponentModel.DataAnnotations;
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

    public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidation()
        {
            RuleFor(rur => rur).Must(HaveValidNames);
            RuleFor(rur => rur).Must(HaveValidEmail);
            RuleFor(rur => rur.IdentityType).IsInEnum();
            RuleFor(rur => rur.IdentityNumber).NotEmpty();
        }

        private bool HaveValidNames(RegisterUserRequest userRequest)
        {
            return !string.IsNullOrEmpty(userRequest.FirstNames)
                && !string.IsNullOrEmpty(userRequest.Surname);
        }

        private bool HaveValidEmail(RegisterUserRequest userRequest)
        {
            return new EmailAddressAttribute().IsValid(userRequest.Email) && !string.IsNullOrEmpty(userRequest.Email);
        }
    }
}