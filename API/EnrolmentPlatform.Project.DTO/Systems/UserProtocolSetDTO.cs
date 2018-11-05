using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 用户注册协议
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserProtocolSetDTO : BasePostOperation
    {
        /// <summary>
        /// 协议内容
        /// </summary>
        [DataMember]
        public string Content { get; set; }
    }
}
