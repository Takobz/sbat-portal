using System.ComponentModel;

namespace SBAT.Web.Services.Common
{
    public class ServiceResponse<T> 
    {
        public Code Code { get; set; }
        public T? Response { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public enum Code
    {
        Success,
        Conflict,
        BadRequest,
        NotFound,
        ServerError,
        UpdateFail,
        Unauthorized
    }
}