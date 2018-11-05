using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    /// <summary>
    /// 系统消息表
    /// </summary>
    [Table("T_SystemMessage")]
    [Serializable]
    [DataContract]
    public class T_SystemMessage : Entity
    {
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
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        [DataMember]
        public Guid EnterpriseId { get; set; }
    }
}
