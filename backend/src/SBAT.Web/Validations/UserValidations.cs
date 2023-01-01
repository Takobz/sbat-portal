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

    public class SignInUserRequestValidation : AbstractValidator<SignInUserRequest>
    {
        public SignInUserRequestValidation()
        {
            RuleFor(siu => siu.Email).NotEmpty();
            RuleFor(siu => siu.Password).NotEmpty();
        }
    }

    public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidation()
        {
            RuleFor(rur => rur).Must(HaveValidNames).WithMessage("FirstNames or Surname can't be null");
            RuleFor(rur => rur).Must(HaveValidEmail).WithMessage("Please provide valid email address");
            RuleFor(rur => rur).Must(HaveNonNullValidPasswords).WithMessage("Password empty or not correctly confirmed");
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

        private bool HaveNonNullValidPasswords(RegisterUserRequest userRequest)
        {
            return !(string.IsNullOrEmpty(userRequest.Password) && string.IsNullOrEmpty(userRequest.ConfirmPassword)) 
                && (userRequest.Password == userRequest.ConfirmPassword);
        }
    }
}