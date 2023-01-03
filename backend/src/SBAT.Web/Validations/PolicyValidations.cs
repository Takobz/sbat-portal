using FluentValidation;
using SBAT.Web.Models.Request;

namespace SBAT.Web.Validations
{
    public class CreatePolicyRequestValidation : AbstractValidator<CreatePolicyRequest>
    {
        public CreatePolicyRequestValidation()
        {
            RuleFor(cpr => cpr.MainMemberUserName).NotEmpty();
            RuleFor(cpr => cpr.MainMember).SetValidator(new MemberRequestValidation());
        }
    }

    public class MemberRequestValidation : AbstractValidator<MemberRequest>
    {
        public MemberRequestValidation()
        {
            RuleFor(mr => mr.FirstNames).NotEmpty();
            RuleFor(mr => mr.Surname).NotEmpty();
            RuleFor(mr => mr.IdentityNumber).NotEmpty();
            RuleFor(mr => mr.Relationship).IsInEnum();
            RuleFor(mr => mr.StreetLine).NotEmpty();
            RuleFor(mr => mr.SuburbOrTownLine).NotEmpty();
            RuleFor(mr => mr.City).NotEmpty();
            RuleFor(mr => mr.Country).NotEmpty();
            RuleFor(mr => mr.Cellphone).NotEmpty();
            RuleFor(mr => mr.DateOfBirth)
                .NotNull().GreaterThan(DateTime.MinValue);
        }
    }
}