namespace SBAT.Web.Models.Common
{
    //TODO: MAKE SET PRIVATE AND LET CALLERS USER CREATERESPONSE METHOD

    /// <summary>
    /// Basic structure for a service response in the namespace: SBAT.Web.Services.
    /// </summary>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Response Code that specifies how the service processed a call.
        /// </summary>
        public ResponseCode Code { get; set; }

        /// <summary>
        /// Response data default(T) if there's no response.
        /// </summary>
        public T? Response { get; set; }

        /// <summary>
        /// Response data default(T) if there's no response.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Creates a ServiceResponse that can be used by caller of the service.
        /// </summary>
        /// <param name="response">Response Code that specifies how the service processed a call.</param>
        /// <param name="code">Response data default(T) if there's no response.</param>
        /// <param name="errors">Response data default(T) if there's no response.</param>
        /// <returns>ServiceResponse for type T</returns>
        public static ServiceResponse<T> CreateServiceResponse(T? response, ResponseCode code, List<string> errors)
        {
            return new ServiceResponse<T> { Response = response, Code = code, Errors = errors };
        }
    }

    /// <summary>
    /// Empty response for service's empty service responses.
    /// </summary>
    public class EmptyServiceResponse { }
}