using DataBaseManger.Model;
using System.Collections.Generic;

namespace TAN.Models
{
    public class productVersionResponse
    {
        public int status;
        public string msg;
        public string apiVersion;
        public List<productVersionModel> data;
    }
}
