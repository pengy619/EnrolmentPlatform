
using System;
namespace EnrolmentPlatform.Project.Infrastructure
{ 
    public class ResultMsg
    {
        public ResultMsg()
        {
            IsSuccess = true;
            StatusCode = 200;
            Info = "请求(或处理)成功";
        }
        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

    }
}
