using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 考试Dto
    /// </summary>
    public class ExamDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 考试名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 学院中心
        /// </summary>
        public string LearningCenter { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
    }

    /// <summary>
    /// 考试查询Dto
    /// </summary>
    public class ExamSearchDto : GridDataRequest
    {
        /// <summary>
        /// 考试名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 学院中心
        /// </summary>
        public Guid? LearningCenterId { set; get; }
    }

    /// <summary>
    /// 新增考试Dto
    /// </summary>
    public class AddExamDto
    {
        /// <summary>
        /// 考试名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 学院中心
        /// </summary>
        public Guid LearningCenterId { set; get; }

        /// <summary>
        /// 考试名单
        /// </summary>
        public List<ExamInfoDto> ExamList { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreatorUserId { set; get; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreatorAccount { set; get; }
    }

    /// <summary>
    /// 考试名单Dto
    /// </summary>
    public class ExamInfoDto
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCardNo { set; get; }

        /// <summary>
        /// 学号
        /// </summary>		
        public string StudentNo { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// 层次
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string MajorName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 考试地点
        /// </summary>		
        public string ExamPlace { get; set; }

        /// <summary>
        /// 考试科目
        /// </summary>
        public string ExamSubject { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 邮寄地址
        /// </summary>		
        public string MailAddress { get; set; } = string.Empty;

        /// <summary>
        /// 回寄地址
        /// </summary>		
        public string ReturnAddress { get; set; } = string.Empty;
    }

    /// <summary>
    /// 考试名单查询Dto
    /// </summary>
    public class ExamListSearchDto : GridDataRequest
    {
        /// <summary>
        /// 考试Id
        /// </summary>
        public Guid ExamId { get; set; }

        /// <summary>
        /// 机构Id
        /// </summary>		
        public Guid? ChannelId { get; set; }
    }

    /// <summary>
    /// 回填考试名单Dto
    /// </summary>
    public class FillExamInfoDto
    {
        /// <summary>
        /// 考试Id
        /// </summary>		
        public Guid ExamId { get; set; }

        /// <summary>
        /// 机构Id
        /// </summary>		
        public Guid ChannelId { get; set; }

        /// <summary>
        /// 考试名单
        /// </summary>
        public List<ExamInfoDto> ExamList { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}
