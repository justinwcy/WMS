namespace WMS.Services
{
    public class AuthMessageSenderOptions
    {
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}
