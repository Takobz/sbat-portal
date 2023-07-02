using SBAT.Web.Models.Common;

namespace SBAT.Web.Models.Response
{
    #pragma warning disable CS8618
    public class Response<T>
    {
        //TODO: MAKE SET PRIVATE AND LET CALLERS USER CREATERESPONSE METHOD

        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public ResponseCode Code { get; set; }

        public static Response<T> CreateResponse(T data, List<string> errors, ResponseCode code) 
        {
            return new Response<T> { Data = data, Errors = errors, Code = code };
        }
    }
}