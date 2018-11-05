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
    public class WithdrawRangeDTO:BasePostOperation
    {
        /// <summary>
        /// 开始范围
        /// </summary>
        [DataMember]
        public decimal StartRange { get; set; }
        /// <summary>
        /// 结束范围
        /// </summary>
        [DataMember]
        public decimal EndRange { get; set; }
    }
}
