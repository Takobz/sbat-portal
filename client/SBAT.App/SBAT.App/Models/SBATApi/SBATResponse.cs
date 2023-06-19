namespace SBAT.App.Models.SBATApi
{
    #pragma warning disable CS8618
    public class SBATResponse<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public int Code { get; set; }
    }
}
