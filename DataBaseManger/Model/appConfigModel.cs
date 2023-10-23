namespace DataBaseManger.Model
{
    public class appConfigModel
    {

        public int appID { get; set; }
        public string adminEmail { get; set; }
        public string adminPassword { get; set; }
        public string adminToken { get; set; }
        public string apiVersion { get; set; }


        public appConfigModel(int AppID, string UserName, string Password,
            string Token, string ApiVersion)
        {
            appID = AppID;
            adminEmail = UserName;
            adminPassword = Password;
            adminToken = Token;
            apiVersion = ApiVersion;
        }
    }
}
