using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    public class OrderApprovalDto
    {
        /// <summary>
        /// 审批ID
        /// </summary>
        public Guid? ApprovalId { set; get; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCardNo { set; get; }

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
        /// 性别
        /// </summary>
        public string Sex { set; get; }

        /// <summary>
        /// 民族
        /// </summary>
        public string MinZu { set; get; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string JiGuan { set; get; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public string HighesDegree { set; get; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string GraduateSchool { set; get; }

        /// <summary>
        /// 毕业证编号
        /// </summary>
        public string BiYeZhengBianHao { set; get; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { set; get; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GongZuoDanWei { set; get; }

        /// <summary>
        /// 招生老师
        /// </summary>
        public string ZhaoShengLaoShi { set; get; }

        /// <summary>
        /// 所读专业
        /// </summary>
        public string SuoDuZhuanYe { get; set; }

        /// <summary>
        /// 是否电大毕业
        /// </summary>
        public bool IsTvUniversity { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduationTime { get; set; }

        /// <summary>
        /// 报考院校Id
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次Id
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// 专业Id
        /// </summary>
        public Guid MajorId { get; set; }

        /// <summary>
        /// 批次Id
        /// </summary>
        public Guid BatchId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        /// 审批状态（0：草稿，1：待审核，2：审核通过，3：审核失败）
        /// </summary>
        public int? ApprovalStatus { set; get; }

        /// <summary>
        /// 审批备注
        /// </summary>
        public string ApprovalComment { set; get; }
    }

    public class OrderApprovalImgDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 审批ID
        /// </summary>
        public Guid ApprovalId { set; get; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        public string IDCard1 { set; get; }

        /// <summary>
        /// 身份证反面
        /// </summary>
        public string IDCard2 { set; get; }

        /// <summary>
        /// 两寸蓝底
        /// </summary>
        public string LiangCunLanDiImg { set; get; }

        /// <summary>
        /// 毕业证
        /// </summary>
        public string BiYeZhengImg { set; get; }

        /// <summary>
        /// 免考英语
        /// </summary>
        public string MianKaoYingYuImg { set; get; }

        /// <summary>
        /// 免考计算机
        /// </summary>
        public string MianKaoJiSuanJiImg { set; get; }

        /// <summary>
        /// 教育部学历证书电子备案表
        /// </summary>
        public string XueXinWangImg { set; get; }

        /// <summary>
        /// 其他
        /// </summary>
        public string QiTa { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        public string TouXiang { set; get; }
    }

    /// <summary>
    /// 订单修改审批返回
    /// </summary>
    public class OrderUpdateApprovalListDto
    {
        /// <summary>
        /// 订单审核ID
        /// </summary>
        public Guid ApprovalId { set; get; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { set; get; }

        /// <summary>
        /// 报名批次
        /// </summary>
        public string BatchName { set; get; }

        /// <summary>
        /// 报考学校
        /// </summary>
        public string SchoolName { set; get; }

        /// <summary>
        /// 报读层次
        /// </summary>
        public string LevelName { set; get; }

        /// <summary>
        /// 报读专业
        /// </summary>
        public string MajorName { set; get; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int ApprovalStatus { set; get; }

        /// <summary>
        /// 审核状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderApprovalStatusEnum)this.ApprovalStatus);
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTimeStr
        {
            get
            {
                return this.CreateTime.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 招生老师
        /// </summary>
        public string CreateUserName { set; get; }
    }

    /// <summary>
    /// 订单修改审批申请
    /// </summary>
    public class OrderUpdateApprovalReq : GridDataRequest
    {
        /// <summary>
        /// 状态
        /// </summary>
        public OrderApprovalStatusEnum? Status { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 报考学校名称
        /// </summary>
        public string SchoolName { set; get; }

        /// <summary>
        /// 学生电话
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { set; get; }

        /// <summary>
        /// 招生机构ID
        /// </summary>
        public Guid? FromChannelId { set; get; }

        /// <summary>
        /// 用户Id(用于子账号数据隔离)
        /// </summary>
        public Guid? UserId { set; get; }
    }
}