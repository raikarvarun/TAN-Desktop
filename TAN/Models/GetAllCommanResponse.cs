
using DataBaseManger.Model;
using System.Collections.Generic;

namespace TAN.Models
{
    public class GetAllCommanResponse<T>
    {
        public int status;
        public string msg;
        public List<appConfigResponseModel> apiVersion;
        public List<T> data;
    }
    public class appConfigResponseModel
    {
        public string appconfigName;
        public string appconfigVersion;
    }
}
