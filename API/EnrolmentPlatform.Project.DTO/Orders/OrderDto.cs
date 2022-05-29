using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

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
        /// 批次名称
        /// </summary>
        public string BatchName { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public int IDCardType { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCardNo { set; get; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { set; get; }

        /// <summary>
        /// 学历类型
        /// </summary>
        public string DegreeType { get; set; }

        /// <summary>
        /// 报考学校
        /// </summary>
        public Guid SchoolId { set; get; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { set; get; }

        /// <summary>
        /// 报读层次
        /// </summary>
        public Guid LevelId { set; get; }

        /// <summary>
        /// 层次名称
        /// </summary>
        public string LevelName { set; get; }

        /// <summary>
        /// 报读专业
        /// </summary>
        public Guid MajorId { set; get; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { set; get; }

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
        /// 状态（0：待提交, 1：提交 2：已退学，3：审核不通过，4：已报名，5：录取提交，6：录取拒绝，7：已录取）
        /// </summary>
        public int Status { set; get; }

        /// <summary>
        /// 协助状态（1：待处理，2：已处理）
        /// </summary>
        public int? AssistStatus { set; get; }

        /// <summary>
        /// 来源机构（没有的话就是渠道添加的）
        /// </summary>
        public Guid? FromChannelId { set; get; }

        /// <summary>
        /// 添加来源类型（机构，个人）
        /// </summary>
        public string FromTypeName { set; get; }

        /// <summary>
        /// 报送学院中心
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
        /// 毕业证编号
        /// </summary>
        public string BiYeZhengBianHao { set; get; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GongZuoDanWei { set; get; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUserName { set; get; }

        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 报名时间
        /// </summary>
        public string CreateTimeStr
        {
            get
            {
                return this.CreateTime.ToString("yyyy-MM-dd");
            }
        }

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
        /// 学习账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 自定义字段
        /// </summary>
        public string CustomerField { get; set; }
    }

    /// <summary>
    /// 订单列表基础DTO
    /// </summary>
    public class OrderListBasicInfoDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

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
        /// 状态
        /// </summary>
        public int Status { set; get; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderStatusEnum)this.Status);
            }
        }

        /// <summary>
        /// 协助状态（1：待处理，2：已处理）
        /// </summary>
        public int? AssistStatus { set; get; }

        /// <summary>
        /// 协助处理状态名称
        /// </summary>
        public string AssistStatusName
        {
            get
            {
                if (this.AssistStatus.HasValue == true)
                {
                    return EnumDescriptionHelper.GetDescription((AssistStatusEnum)this.AssistStatus);
                }
                return "不需要";
            }
        }

        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 报名时间
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

        /// <summary>
        /// 学号
        /// </summary>
        public string XueHao { set; get; }
    }

    /// <summary>
    /// 订单列表DTO
    /// </summary>
    public class OrderListDto : OrderListBasicInfoDto
    {
        /// <summary>
        /// 所有图片是否都上传完成
        /// </summary>
        public bool AllOrderImageUpload { set; get; }

        /// <summary>
        /// 报名审核时间
        /// </summary>
        public DateTime? EnrollTime { set; get; }

        /// <summary>
        /// 报送学院中心时间
        /// </summary>
        public DateTime? ToLearningCenterTime { set; get; }

        /// <summary>
        /// 退学时间
        /// </summary>
        public DateTime? LeaveTime { set; get; }

        /// <summary>
        /// 录取时间
        /// </summary>
        public DateTime? JoinTime { set; get; }

        /// <summary>
        /// 录取时间
        /// </summary>
        public string JoinTimeStr
        {
            get
            {
                return JoinTime.HasValue ? JoinTime.Value.ToString("yyyy-MM-dd") : "";
            }
        }

        /// <summary>
        /// 是否报名提交
        /// </summary>
        public bool IsSubmit
        {
            get
            {
                return this.EnrollTime.HasValue == true;
            }
        }

        /// <summary>
        /// 是否报送学院中心
        /// </summary>
        public bool IsToLearningCenter
        {
            get
            {
                return this.ToLearningCenterTime.HasValue == true;
            }
        }

        /// <summary>
        /// 是否录取
        /// </summary>
        public bool IsJoin
        {
            get
            {
                return this.JoinTime.HasValue == true;
            }
        }

        /// <summary>
        /// 是否退学
        /// </summary>
        public bool IsLeave
        {
            get
            {
                return this.LeaveTime.HasValue == true;
            }
        }

        /// <summary>
        /// 来源机构
        /// </summary>
        public Guid? FromChannelId { set; get; }

        /// <summary>
        /// 来源机构名称
        /// </summary>
        public string FromChannelName { set; get; }

        /// <summary>
        /// 学习中心名称
        /// </summary>
        public string ToLearningCenterName { set; get; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { set; get; }

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
        /// 毕业证编号
        /// </summary>
        public string BiYeZhengBianHao { set; get; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GongZuoDanWei { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { set; get; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string GraduateSchool { set; get; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public string HighesDegree { set; get; }

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
        /// 用户名
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 自定义字段
        /// </summary>
        public string CustomerField { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduationTime { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public string GraduationTimeStr
        {
            get
            {
                return GraduationTime.HasValue ? GraduationTime.Value.ToString("yyyy-MM-dd") : "";
            }
        }

        /// <summary>
        /// 学生头像
        /// </summary>
        public string StudentImg { get; set; }

        /// <summary>
        /// 所有毕业照片是否都上传完成
        /// </summary>
        public bool AllBiYeImageUpload { set; get; }
    }

    /// <summary>
    /// 订单图片列表DTO
    /// </summary>
    public class OrderImageListDto : OrderListBasicInfoDto
    {
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

        /// <summary>
        /// 毕业学信网学籍截图
        /// </summary>
        public string BiYeXueJiImg { set; get; }

        /// <summary>
        /// 毕业照
        /// </summary>
        public string BiYePhoto { set; get; }
    }

    /// <summary>
    /// 订单付款列表DTO
    /// </summary>
    public class OrderPaymentListDto : OrderListBasicInfoDto
    {
        /// <summary>
        /// 招生机构总金额
        /// </summary>
        public decimal TotalAmount { set; get; }

        /// <summary>
        /// 招生机构已缴金额
        /// </summary>
        public decimal PayedAmount { set; get; }

        /// <summary>
        /// 招生机构待审核金额
        /// </summary>
        public decimal ApprovalAmount { set; get; }

        /// <summary>
        /// 招生机构未缴金额
        /// </summary>
        public decimal UnPayedAmount
        {
            get
            {
                return this.TotalAmount - this.PayedAmount - this.ApprovalAmount;
            }
        }

        /// <summary>
        /// 渠道中心总金额
        /// </summary>
        public decimal QDTotalAmount { set; get; }

        /// <summary>
        /// 渠道中心已缴金额
        /// </summary>
        public decimal QDPayedAmount { set; get; }

        /// <summary>
        /// 渠道中心待审核金额
        /// </summary>
        public decimal QDApprovalAmount { set; get; }

        /// <summary>
        /// 渠道中心未缴金额
        /// </summary>
        public decimal QDUnPayedAmount
        {
            get
            {
                return this.QDTotalAmount - this.QDPayedAmount - this.QDApprovalAmount;
            }
        }

        /// <summary>
        /// 学院中心ID
        /// </summary>
        public Guid ToLearningCenterId { set; get; }

        /// <summary>
        /// 学院中心名称
        /// </summary>
        public string ToLearningCenterName { set; get; }
    }

    /// <summary>
    /// 订单列表请求DTO
    /// </summary>
    public class OrderListReqDto : GridDataRequest
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 学生电话
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { set; get; }

        /// <summary>
        /// 创建老师
        /// </summary>
        public string CreateUserName { set; get; }

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
        /// 报名批次
        /// </summary>
        public string BatchName { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateFrom { set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateTo { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public OrderStatusEnum? Status { set; get; }

        /// <summary>
        /// 协助处理状态
        /// </summary>
        public AssistStatusEnum? AssistStatus { set; get; }

        /// <summary>
        /// 资料状态
        /// </summary>
        public OrderImageStatusEnum? OrderImageStatus { set; get; }

        /// <summary>
        /// 电子照是否上传完成
        /// </summary>
        public bool? AllOrderImageUpload { set; get; }

        /// <summary>
        /// 毕业照是否上传完成
        /// </summary>
        public bool? AllBiYeImageUpload { set; get; }

        /// <summary>
        /// 招生中心学费是否缴完
        /// </summary>
        public bool? ZhaoShengXueFei { set; get; }

        /// <summary>
        /// 渠道中心学费情况
        /// </summary>
        public bool? QuDaoXueFei { set; get; }

        /// <summary>
        /// 来源机构（没有的话就是渠道添加的）
        /// </summary>
        public Guid? FromChannelId { set; get; }

        /// <summary>
        /// 报送学院中心
        /// </summary>
        public Guid? ToLearningCenterId { set; get; }

        /// <summary>
        /// 是否是渠道查询
        /// </summary>
        public bool? IsChannel { set; get; }

        /// <summary>
        /// 是否是渠道添加
        /// </summary>
        public bool? IsChannelAdd { set; get; }

        /// <summary>
        /// 查询的ID
        /// </summary>
        public List<Guid> OrderIds { set; get; }

        /// <summary>
        /// 学院中心
        /// </summary>
        public string ToLearningCenterName { set; get; }

        /// <summary>
        /// 用户Id(用于子账号数据隔离)
        /// </summary>
        public Guid? UserId { set; get; }
    }

    /// <summary>
    /// 订单导入DTO
    /// </summary>
    public class OrderUploadDto
    {
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
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { set; get; }

        /// <summary>
        /// 录取时间
        /// </summary>
        public DateTime? LuquDate { set; get; }

        /// <summary>
        /// 招生老师
        /// </summary>
        public string CreateUserName { set; get; }

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
        /// 所读专业
        /// </summary>
        public string SuoDuZhuanYe { get; set; }

        /// <summary>
        /// 是否电大毕业
        /// </summary>
        public string IsTvUniversity { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduationTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        /// 来源机构
        /// </summary>
        public string FromChannelName { set; get; }

        /// <summary>
        /// 学院中心
        /// </summary>
        public string ToLearningCenterName { set; get; }

        /// <summary>
        /// 学号
        /// </summary>		
        public string StudentNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 机构应缴
        /// </summary>
        public decimal JiGouAmount { set; get; }

        /// <summary>
        /// 机构已缴
        /// </summary>
        public decimal JiGouPayedAmount { set; get; }

        /// <summary>
        /// 中心应缴
        /// </summary>
        public decimal ZhongXinAmount { set; get; }

        /// <summary>
        /// 中心已缴
        /// </summary>
        public decimal ZhongXinPayedAmount { set; get; }
    }

    /// <summary>
    /// 录取导入DTO
    /// </summary>
    public class OrderLuQuUploadDto
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 身份证号码
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
        /// 录取时间
        /// </summary>
        public DateTime? LuquDate { set; get; }

        /// <summary>
        /// 学号
        /// </summary>		
        public string StudentNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 提交订单DTO
    /// </summary>
    public class SubmitOrderDto
    {
        /// <summary>
        /// 提交的订单ID集合
        /// </summary>
        public Guid[] IDs { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }
    }

    /// <summary>
    /// 初审导入DTO
    /// </summary>
    public class OrderAuditUploadDto
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 身份证号码
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
    }

    /// <summary>
    /// 机构订单导入DTO
    /// </summary>
    public class JiGouOrderUploadDto
    {
        public List<OrderUploadDto> OrderUploadList { get; set; }

        /// <summary>
        /// 机构Id
        /// </summary>
        public Guid FromChannelId { set; get; }

        /// <summary>
        /// 创建用户Id
        /// </summary>
        public Guid CreatorUserId { set; get; }
    }
}
