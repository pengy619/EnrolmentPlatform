using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Orders
{
    [Serializable]
    [Table("T_Order")]
    [DataContract]
    public class T_Order : Entity
    {
        /// <summary>
        /// 报名批次
        /// </summary>
        public Guid BatchId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCardNo { set; get; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { set; get; }

        /// <summary>
        /// 报考学校
        /// </summary>
        public Guid SchoolId { set; get; }

        /// <summary>
        /// 报读层次
        /// </summary>
        public Guid LevelId { set; get; }

        /// <summary>
        /// 报读专业
        /// </summary>
        public Guid MajorId { set; get; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 微信/QQ
        /// </summary>
        public string TencentNo { set; get; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        /// 状态（1:暂存(草稿) 2:保存(待审核) 3:通过 4:确认）
        /// </summary>
        public int Status { set; get; }

        /// <summary>
        /// 来源渠道
        /// </summary>
        public Guid FromChannelId { set; get; }

        /// <summary>
        /// 来源机构
        /// </summary>
        public Guid FromInstitutionId { set; get; }

        /// <summary>
        /// 报送学习中心
        /// </summary>
        public Guid ToLearningCenterId { set; get; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string GraduateSchool { set; get; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public string HighesDegree { set; get; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnit { set; get; }

        /// <summary>
        /// 报名地点
        /// </summary>
        public string EnrollAddress { set; get; }

        /// <summary>
        /// 考试科目
        /// </summary>
        public string ExamSubject { set; get; }

        /// <summary>
        /// 考试日期
        /// </summary>
        public DateTime ExamDate { set; get; }

        /// <summary>
        /// 学费标准
        /// </summary>
        public decimal TuitionStandard { set; get; }

        /// <summary>
        /// 是否录取
        /// </summary>
        public bool IsEnroll { set; get; }
    }
}
