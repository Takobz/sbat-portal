namespace SBAT.Web.Models.Response
{
    #pragma warning disable CS8618
    public class Response<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        //TODO: Have predefined codes that indicate success, error, and custom message types you want to give a client
        public int Code { get; set; } 
    }
}