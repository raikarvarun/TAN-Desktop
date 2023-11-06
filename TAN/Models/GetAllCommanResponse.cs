using DataBaseManger.Model;
using System.Collections.Generic;

namespace TAN.Models
{
    public class GetAllCommanResponse<T>
    {
        public int status;
        public string msg;
        public string apiVersion;
        public List<T> data;
    }
}
