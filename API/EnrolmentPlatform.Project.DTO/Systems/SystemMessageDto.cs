using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 系统消息
    /// </summary>
    [Serializable]
    [DataContract]
    public class SystemMessageDto
    {
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public string Content { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        [DataMember]
        public string BusinessName { get; set; }
        /// <summary>
        /// 企业编号
        /// </summary>
        [DataMember]
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((MessageStatusEnum)Status);
            }
        }
    }

    /// <summary>
    /// 系统消息搜索参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class ParamForSystemMessageDto : GridDataRequest
    {
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// EnterpriseId
        /// </summary>
        [DataMember]
        public Guid EnterpriseId { get; set; }
    }
}
