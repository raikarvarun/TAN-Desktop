using DataBaseManger.Model;
using System.Collections.Generic;

namespace TAN.Models
{
    public class paymentResponse
    {

        public int status;
        public string msg;
        public string apiVersion;
        public List<paymentModel> data;

    }
}
