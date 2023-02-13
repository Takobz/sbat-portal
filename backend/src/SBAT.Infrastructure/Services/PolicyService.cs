using SBAT.Core.Entities;
using SBAT.Core.Interfaces;

namespace SBAT.Infrastructure.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<Policy> _policyRepository;
        public PolicyService(IRepository<Policy> policyRepository)
        {
            _policyRepository = policyRepository;
        }
        
        public Policy? GetPolicyByPolicyNumber(string policyNumber)
        {
            return _policyRepository.Get(p => p.PolicyNumber == policyNumber);
        }

        public Policy? GetPolicyById(int policyId)
        {
            return _policyRepository.GetById(policyId);
        }

        public IEnumerable<Policy> GetUserPolicies(string userName)
        {
            return _policyRepository.List(p => p.PrincipalMemberUserName == userName);
        }

        public Policy? CreatePolicy(Policy policy)
        {
            var policyNumber = GeneratePolicyNumber();
            policy.CreatePolicyNumber(policyNumber);
            policy.CreateMainMember(policy.PrincipalMemberUserName);

            _policyRepository.Add(policy);
            return _policyRepository.Get(p => p.PolicyNumber == policyNumber);
        }

        public void ModifyPolicy(Policy policy)
        {
            _policyRepository.Update(policy);
        }

        public void DeletePolicy(Policy policy)
        {
            _policyRepository.Delete(policy);
        }

        public string GeneratePolicyNumber()
        {
            var count = _policyRepository.List()
                .Count();
            
            //TODO: add logic to create PolicyNum: ESF001
            return count.ToString();
        }
    }
}