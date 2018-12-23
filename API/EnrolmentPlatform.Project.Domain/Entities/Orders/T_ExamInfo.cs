using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_ExamInfo")]
    [DataContract]
    public class T_ExamInfo : Entity
    {
        /// <summary>
        /// 学生Id
        /// </summary>		
        [DataMember]
        public Guid StudentId { get; set; }

        /// <summary>
        /// 考试Id
        /// </summary>		
        [DataMember]
        public Guid ExamId { get; set; }

        /// <summary>
        /// 学号
        /// </summary>		
        [DataMember]
        public string StudentNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>		
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 考试地点
        /// </summary>		
        [DataMember]
        public string ExamPlace { get; set; }

        /// <summary>
        /// 邮寄地址
        /// </summary>		
        [DataMember]
        public string MailAddress { get; set; }

        /// <summary>
        /// 回寄地址
        /// </summary>		
        [DataMember]
        public string ReturnAddress { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        [DataMember]
        public string StudentName { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [DataMember]
        public string BatchName { get; set; }

        /// <summary>
        /// 层次
        /// </summary>
        [DataMember]
        public string LevelName { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        [DataMember]
        public string MajorName { get; set; }

        /// <summary>
        /// 机构Id
        /// </summary>
        [DataMember]
        public Guid ChannelId { get; set; }
    }
}