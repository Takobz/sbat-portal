using SBAT.Core.Entities;

namespace SBAT.Core.Interfaces
{
    public interface IPolicyService
    {
        Policy? GetPolicyByPolicyNumber(string policyNumber);
        Policy? GetPolicyById(int policyId);
        IEnumerable<Policy> GetUserPolicies(string userName);
        Policy? CreatePolicy(Policy policy);
        void ModifyPolicy(Policy policy);
        void DeletePolicy(Policy policy);
        string GeneratePolicyNumber();
    }
}