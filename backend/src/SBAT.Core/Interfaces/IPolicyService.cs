using SBAT.Core.Entities;

namespace SBAT.Core.Interfaces
{
    public interface IPolicyService
    {
        IEnumerable<Policy> GetUserPolicies(string userName);
        Policy? CreatePolicy(Policy policy);
    }
}