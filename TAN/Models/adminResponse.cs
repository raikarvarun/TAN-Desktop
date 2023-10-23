namespace TAN.Models
{
    public class adminResponse
    {
        public adminModel data1 { get; set; }

    }
    public class adminModel
    {
        public int adminID { get; set; }
        public string adminEmail { get; set; }
        public string adminPassword { get; set; }
        public string adminToken { get; set; }
    }
}
