using DataBaseManger.Model;
using System.Collections.Generic;

namespace TAN.Models
{
    public class customerResponse
    {
        public int status;
        public string msg;
        public string apiVersion;
        public List<customerModel> data;
    }

}
