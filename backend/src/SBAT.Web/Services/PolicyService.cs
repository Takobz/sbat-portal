using AutoMapper;
using SBAT.Core.Entities;
using SBAT.Infrastructure.Data.Repos;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;
using SBAT.Web.Services.Common;

namespace SBAT.Web.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IMapper _mapper;

        public PolicyService(IPolicyRepository policyRepository, IMapper mapper) 
        {
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public ServiceResponse<CreatePolicyResponse> CreatePolicy(Policy policy)
        {
            var createdPolicy = _policyRepository.CreatePolicy(policy);
            if (createdPolicy == null)
            {
                var errors = new List<string>() { $"Failed to create policy" };
                return ServiceResponse<CreatePolicyResponse>.CreateServiceResponse(default, Code.Conflict, errors);
            }

            var policyResponse = _mapper.Map<CreatePolicyResponse>(createdPolicy);    
            return ServiceResponse<CreatePolicyResponse>.CreateServiceResponse(policyResponse, Code.Success, new List<string>());
        }

        public ServiceResponse<ICollection<GetPolicyResponse>> GetMemeberPolicies(string principalUsername)
        {
            var userPolicies = _policyRepository.GetPolicies(principalUsername);
            var userPoliciesResponse = userPolicies.Select(p => _mapper.Map<GetPolicyResponse>(p));
            return ServiceResponse<ICollection<GetPolicyResponse>>.CreateServiceResponse(userPoliciesResponse.ToList(), Code.Success, new List<string>());
        }

        public ServiceResponse<GetPolicyResponse> GetPolicy(string policyNumber)
        {
            var policy = _policyRepository.GetPolicy(policyNumber);
            if (policy is null)
                return ServiceResponse<GetPolicyResponse>.CreateServiceResponse(default, Code.NotFound, new List<string>());

            var response = _mapper.Map<GetPolicyResponse>(policy);
            return ServiceResponse<GetPolicyResponse>.CreateServiceResponse(response, Code.Success, new List<string>());
        }

        public ServiceResponse<GetPolicyResponse> AddMemeberToPolicy(string policyNumber, CreateMemberRequest createMember)
        {
            var policy = _policyRepository.GetPolicy(policyNumber);
            if (policy is null)
            {
                var errors = new List<string>() { $"Couldn't find policy: {policyNumber}" }; 
                return ServiceResponse<GetPolicyResponse>.CreateServiceResponse(default, Code.NotFound, new List<string>());
            }

            var memeber = _mapper.Map<Member>(createMember);
            _policyRepository.AddMember(policyNumber, memeber);

            policy = _policyRepository.GetPolicy(policyNumber);
            var responsePolicy = _mapper.Map<GetPolicyResponse>(policy);
            return ServiceResponse<GetPolicyResponse>.CreateServiceResponse(responsePolicy, Code.Success, new List<string>());
        }
    }

    public interface IPolicyService 
    {
        ServiceResponse<CreatePolicyResponse> CreatePolicy(Policy policy);
        ServiceResponse<ICollection<GetPolicyResponse>> GetMemeberPolicies(string principalUsername);
        ServiceResponse<GetPolicyResponse> AddMemeberToPolicy(string policyNumber, CreateMemberRequest createMember);
        ServiceResponse<GetPolicyResponse> GetPolicy(string policyNumber);
    }
}