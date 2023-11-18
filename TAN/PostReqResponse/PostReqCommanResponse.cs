using System.Collections.Generic;

namespace TAN.PostReqResponse
{
    public class PostReqCommanResponse<T>
    {
        public int status;
        public string msg;
        public List<appConfigResponseModel> apiVersion;
        public T data;
    }
    public class appConfigResponseModel
    {
        public string appconfigName;
        public string appconfigVersion;
    }
}
