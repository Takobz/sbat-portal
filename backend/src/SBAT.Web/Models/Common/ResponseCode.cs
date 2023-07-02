namespace SBAT.Web.Models.Common
{
    /// <summary>
    /// Used as Code for ServiceResponse or SBAT.Web.Response DTO.
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// The call was success and relevant data is returned.
        /// </summary>
        Success,

        /// <summary>
        /// The call caused a conflict with the current state of the requested resource.
        /// </summary>
        Conflict,

        /// <summary>
        /// The caller of the service or api didn't give correct data
        /// </summary>
        BadRequest,

        /// <summary>
        /// The Service experienced an unexpected Error, check Errors collection
        /// </summary>
        ServerError,

        /// <summary>
        /// Updating a resource failed for some reason, see Errors List.
        /// </summary>
        UpdateFail,

        /// <summary>
        /// Caller tried to access a resource with insuffient permissions.
        /// </summary>
        Unauthorized,

        /// <summary>
        /// Resource caller is looking for doesn't exist
        /// </summary>
        NotFound
    }
}
