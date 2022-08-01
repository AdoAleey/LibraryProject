using Newtonsoft.Json;

namespace Library.WebAPI.Common
{
    public class ErrorDetail
    {
        public string Message { get; set; }
        public override string ToString()
        {
            //kovertuj JSON u string
            return JsonConvert.SerializeObject(this);
        }
    }
}
