namespace SBAT.Web.Models.Response
{
    #pragma warning disable CS8618
    public class Response<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}