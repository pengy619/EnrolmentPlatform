
using System;
namespace EnrolmentPlatform.Project.Infrastructure
{
    [Serializable]
    public class HttpResponseMsg
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public string Info { get; set; }
        public bool IsSuccess { get; set; }
    }
}
