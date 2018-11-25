using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 订单DTO
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid? OrderId { set; get; }

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
        /// 状态（0：草稿, 1：提交 2：已退学，3：已拒绝，4：已报名，5：录取提交，6：录取拒绝，7：已录取）
        /// </summary>
        public int Status { set; get; }

        /// <summary>
        /// 来源机构（没有的话就是渠道添加的）
        /// </summary>
        public Guid? FromChannelId { set; get; }

        /// <summary>
        /// 添加来源类型（机构，个人）
        /// </summary>
        public string FromTypeName { set; get; }

        /// <summary>
        /// 报送学习中心
        /// </summary>
        public Guid? ToLearningCenterId { set; get; }

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
        public DateTime? ExamDate { set; get; }

        /// <summary>
        /// 所有图片是否都上传完成
        /// </summary>
        public bool AllOrderImageUpload { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 创建用户Id
        /// </summary>
        public Guid UserId { set; get; }
    }
}
