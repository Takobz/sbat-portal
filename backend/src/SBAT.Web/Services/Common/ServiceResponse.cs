using System.ComponentModel;

namespace SBAT.Web.Services.Common
{
    public class ServiceResponse<T>
    {
        public Code Code { get; set; }
        public T? Response { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ServiceResponse<T> CreateServiceResponse(T? response, Code code, List<string> errors)
        {
            return new ServiceResponse<T> { Response = response, Code = code, Errors = errors };
        }
    }

    public class EmptyServiceResponse {} 

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