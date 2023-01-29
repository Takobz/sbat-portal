namespace SBAT.Web.Models.Response
{
    public class GetPolicyResponse 
    {
        public string policyNumber { get; set; } = string.Empty;
        public List<MemberResponse> Members { get; set; } = new List<MemberResponse>();   
    }
}