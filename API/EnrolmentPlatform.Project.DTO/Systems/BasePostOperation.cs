using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    [Serializable]
    [DataContract]
    public class BasePostOperation
    {
        /// <summary>
        /// 操作人姓名
        /// </summary>
        [DataMember]
        public string UpdateUserName { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        [DataMember]
        public Guid UpdateUserId { get; set; }
    }
}
