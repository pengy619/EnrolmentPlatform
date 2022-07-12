using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 账号列表DTO
    /// </summary>
    public class AccountListDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { set; get; }

        /// <summary>
        /// 学号
        /// </summary>		
        public string StudentNo { get; set; }

        /// <summary>
        /// 账号
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 账号列表请求DTO
    /// </summary>
    public class AccountListReqDto : GridDataRequest
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { set; get; }

        /// <summary>
        /// 来源机构
        /// </summary>
        public Guid? FromChannelId { set; get; }

        /// <summary>
        /// 用户Id(用于子账号数据隔离)
        /// </summary>
        public Guid? UserId { set; get; }
    }

    /// <summary>
    /// 修改学员账号Dto
    /// </summary>
    public class UpdateAccountDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 学号
        /// </summary>		
        public string StudentNo { get; set; }

        /// <summary>
        /// 账号
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { set; get; }
    }
}
