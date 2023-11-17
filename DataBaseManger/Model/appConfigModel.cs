namespace DataBaseManger.Model
{
    public class appConfigModel
    {

        public int appconfigID { get; set; }
        public string appconfigName { get; set; }
        public string appconfigVersion { get; set; }
       
        public appConfigModel(int appconfigID, string appconfigName, string appconfigVersion)
        {
            this.appconfigID = appconfigID;
            this.appconfigName = appconfigName;
            this.appconfigVersion = appconfigVersion;
        }
    }
}
