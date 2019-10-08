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
    /// 订单表
    /// </summary>
    [Serializable]
    [Table("T_Order")]
    [DataContract]
    public class T_Order : Entity
    {
        /// <summary>
        /// 报名批次
        /// </summary>
        [DataMember]
        public Guid BatchId { set; get; }

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
        /// 籍贯
        /// </summary>
        [DataMember]
        public string Native { set; get; }

        /// <summary>
        /// 报考学校
        /// </summary>
        [DataMember]
        public Guid SchoolId { set; get; }

        /// <summary>
        /// 报读层次
        /// </summary>
        [DataMember]
        public Guid LevelId { set; get; }

        /// <summary>
        /// 报读专业
        /// </summary>
        [DataMember]
        public Guid MajorId { set; get; }

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
        /// 联系地址
        /// </summary>
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { set; get; }

        /// <summary>
        /// 状态（0：待提交, 1：提交 2：已退学，3：审核不通过，4：已报名，5：录取提交，6：录取拒绝，7：已录取）
        /// </summary>
        [DataMember]
        public int Status { set; get; }

        /// <summary>
        /// 协助状态（1：待处理，2：已处理）
        /// </summary>
        public int? AssistStatus { set; get; }

        /// <summary>
        /// 报名审核时间
        /// </summary>
        [DataMember]
        public DateTime? EnrollTime { set; get; }

        /// <summary>
        /// 报送学院中心时间
        /// </summary>
        [DataMember]
        public DateTime? ToLearningCenterTime { set; get; }

        /// <summary>
        /// 退学时间
        /// </summary>
        [DataMember]
        public DateTime? LeaveTime { set; get; }

        /// <summary>
        /// 录取时间
        /// </summary>
        [DataMember]
        public DateTime? JoinTime { set; get; }

        /// <summary>
        /// 来源机构（没有的话就是渠道添加的）
        /// </summary>
        [DataMember]
        public Guid? FromChannelId { set; get; }

        /// <summary>
        /// 添加来源类型（机构，个人）
        /// </summary>
        [DataMember]
        public string FromTypeName { set; get; }

        /// <summary>
        /// 报送学院中心
        /// </summary>
        [DataMember]
        public Guid? ToLearningCenterId { set; get; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        [DataMember]
        public string GraduateSchool { set; get; }

        /// <summary>
        /// 最高学历
        /// </summary>
        [DataMember]
        public string HighesDegree { set; get; }

        /// <summary>
        /// 工作单位
        /// </summary>
        [DataMember]
        public string WorkUnit { set; get; }

        /// <summary>
        /// 报名地点
        /// </summary>
        [DataMember]
        public string EnrollAddress { set; get; }

        /// <summary>
        /// 考试科目
        /// </summary>
        [DataMember]
        public string ExamSubject { set; get; }

        /// <summary>
        /// 考试日期
        /// </summary>
        [DataMember]
        public DateTime? ExamDate { set; get; }

        /// <summary>
        /// 所有图片是否都上传完成
        /// </summary>
        [DataMember]
        public bool AllOrderImageUpload { set; get; }

        /// <summary>
        /// 招生中心所有费用是否缴完
        /// </summary>
        [DataMember]
        public bool AllZSZhongXinAmountPayed { set; get; }

        /// <summary>
        /// 渠道中心所有费用是否缴完
        /// </summary>
        [DataMember]
        public bool AllQuDaoAmountPayed { set; get; }

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
        /// 密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }

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
        /// 毕业证编号
        /// </summary>
        [DataMember]
        public string BiYeZhengBianHao { set; get; }

        /// <summary>
        /// 工作单位
        /// </summary>
        [DataMember]
        public string GongZuoDanWei { set; get; }
    }
}
