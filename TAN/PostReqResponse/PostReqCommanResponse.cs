

namespace TAN.PostReqResponse
{
    public class PostReqCommanResponse<T>
    {
        public int status;
        public string msg;
        public string apiVersion;
        public T data;
    }
}

