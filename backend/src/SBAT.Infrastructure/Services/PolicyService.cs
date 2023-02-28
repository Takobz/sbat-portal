using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Data;

namespace SBAT.Infrastructure.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;
        public PolicyService(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public IEnumerable<Policy> GetUserPolicies(string userName)
        {
            //this can be better, idl this :( - FIX!
            return _policyRepository.GetPolicies(userName);
        }

        public Policy? CreatePolicy(Policy policy)
        {
            //make service have generic return object.
            return _policyRepository.CreatePolicy(policy);
        }
    }
}