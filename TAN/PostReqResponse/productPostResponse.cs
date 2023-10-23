using DataBaseManger.Model;

namespace TAN.PostReqResponse
{
    public class productPostResponse
    {
        public int status;
        public string msg;
        public string apiVersion;
        public productVersionModel data;
    }
}
