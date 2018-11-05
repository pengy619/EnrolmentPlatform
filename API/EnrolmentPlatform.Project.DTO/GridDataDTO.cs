using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace EnrolmentPlatform.Project.DTO
{
    public class GridDataResponse
    {
        public GridDataResponse()
        {
            this.Code = 0;
            this.Msg = "处理成功";
        }
        [JsonProperty("data")]
        public object Data { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }
    }

    public class GridDataRequest
    {
        public GridDataRequest()
        {
            this.Limit = 10;
            this.Page = 1;
        }
        /// <summary>
        /// 每页大小数
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }
        /// <summary>
        /// 排序 desc asc
        /// </summary>
        [JsonProperty("sort")]
        public string Sort { get; set; }
    }
}
