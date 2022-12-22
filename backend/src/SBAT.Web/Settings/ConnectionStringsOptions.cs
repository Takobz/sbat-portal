namespace SBAT.Web.Settings
{
    public class ConnectionStringsOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";

        public string SbatDatabase { get; set; } = string.Empty;
    }
}