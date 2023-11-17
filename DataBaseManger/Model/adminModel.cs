using System.ComponentModel.DataAnnotations;

namespace DataBaseManger.Model
{
    public class adminModel
    {
        public int adminID;
        public string adminEmail;
        public string adminPassword;
        public string adminToken;
        public int isAdmin;

        public adminModel(int adminID, string adminEmail, string adminPassword,
            string adminToken, int isAdmin)
        {
            this.adminID = adminID;
            this.adminEmail = adminEmail;
            this.adminPassword = adminPassword;
            this.adminToken = adminToken;
            this.isAdmin = isAdmin;

        }
    }
}
