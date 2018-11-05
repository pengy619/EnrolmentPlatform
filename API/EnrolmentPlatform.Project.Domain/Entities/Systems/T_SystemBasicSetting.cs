using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    /// <summary>
    /// 系统基本设置表
    /// </summary>
    [Serializable]
    [Table("T_SystemBasicSetting")]
    [DataContract]
    public class T_SystemBasicSetting : Entity
    {
        /// <summary>
        /// 键【1：网站logo】【2：网站标题】【3：网站关键字】【4：网站描叙】【5：b2c农产品取消时间（小时）】【6：b2c票务取消时间（小时）】【7：游客中心票务取消时间（小时）】【8; 农产品自动收货时间（小时）】【9：农产品自动收货时间（天）】【10：提现范围（0-5000格式分隔）】
        /// </summary>
        [DataMember]
        public int Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        public string Value { get; set; }
        /// <summary>
        /// 描叙
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

    }
}
