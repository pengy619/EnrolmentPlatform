using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Orders
{
    /// <summary>
    /// 订单审批表
    /// </summary>
    [Serializable]
    [Table("T_OrderApproval")]
    [DataContract]
    public class T_OrderApproval : Entity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        [DataMember]
        public string StudentName { set; get; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [DataMember]
        public string IDCardNo { set; get; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        public string Phone { set; get; }

        /// <summary>
        /// 微信/QQ
        /// </summary>
        [DataMember]
        public string TencentNo { set; get; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string Sex { set; get; }

        /// <summary>
        /// 民族
        /// </summary>
        [DataMember]
        public string MinZu { set; get; }

        /// <summary>
        /// 籍贯
        /// </summary>
        [DataMember]
        public string JiGuan { set; get; }

        /// <summary>
        /// 最高学历
        /// </summary>
        [DataMember]
        public string HighesDegree { set; get; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        [DataMember]
        public string GraduateSchool { set; get; }

        /// <summary>
        /// 毕业证编号
        /// </summary>
        [DataMember]
        public string BiYeZhengBianHao { set; get; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 工作单位
        /// </summary>
        [DataMember]
        public string GongZuoDanWei { set; get; }

        /// <summary>
        /// 招生老师
        /// </summary>
        [DataMember]
        public string ZhaoShengLaoShi { set; get; }

        /// <summary>
        /// 所读专业
        /// </summary>
        [DataMember]
        public string SuoDuZhuanYe { get; set; }

        /// <summary>
        /// 报考院校Id
        /// </summary>
        [DataMember]
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次Id
        /// </summary>
        [DataMember]
        public Guid LevelId { get; set; }

        /// <summary>
        /// 专业Id
        /// </summary>
        [DataMember]
        public Guid MajorId { get; set; }

        /// <summary>
        /// 批次Id
        /// </summary>
        [DataMember]
        public Guid BatchId { get; set; }

        /// <summary>
        /// 是否电大毕业
        /// </summary>
        [DataMember]
        public bool IsTvUniversity { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        [DataMember]
        public DateTime? GraduationTime { get; set; }

        /// <summary>
        /// 自定义字段
        /// </summary>
        [DataMember]
        public string CustomerField { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { set; get; }

        /// <summary>
        /// 所有图片是否都上传完成
        /// </summary>
        [DataMember]
        public bool AllOrderImageUpload { set; get; }

        /// <summary>
        /// 审批状态（0：草稿，1：待审核，2：审核通过，3：审核失败）
        /// </summary>
        [DataMember]
        public int ApprovalStatus { set; get; }

        /// <summary>
        /// 审批备注
        /// </summary>
        [DataMember]
        public string ApprovalComment { set; get; }
    }
}
