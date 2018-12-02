using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Accounts
{
    /// <summary>
    /// 企业信息添加DTO
    /// </summary>
    public class EnterpriseAddDto
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary> 
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 类型
        /// </summary> 
        public SystemTypeEnum Classify { get; set; }

        /// <summary>
        /// 联系人
        /// </summary> 
        public string Contact { get; set; }

        /// <summary>
        /// 手机号
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary> 
        public string UserAccount { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary> 
        public string UserPwd { get; set; }

        /// <summary>
        /// 备注
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid CurUserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string CurUserAccount { set; get; }
    }

    /// <summary>
    /// 供应商端-企业信息获取DTO
    /// </summary>
    public class SupplierEnterpriseGetDto
    {
        /// <summary>
        /// 企业名称
        /// </summary> 
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary> 
        public string Contact { get; set; }

        /// <summary>
        /// 手机号
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary> 
        public string UserAccount { get; set; }
    }

    /// <summary>
    /// 供应商列表Dto
    /// </summary>
    public class SupplierListDto
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginAccount { get; set; }

        /// <summary>
        /// 联系人
        /// </summary> 
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 状态【1：未启用】【2：启用】
        /// </summary>
        public int Status { get; set; }

        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusBaseEnum)Status);
            }
        }
    }

    /// <summary>
    /// 供应商查询Dto
    /// </summary>
    public class SupplierSearchDto : GridDataRequest
    {
        /// <summary>
        /// 类型
        /// </summary> 
        public SystemTypeEnum Classify { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginAccount { get; set; }
    }

    /// <summary>
    /// 更新企业状态 
    /// </summary>
    public class UpdateEnterpriseStatusDto
    {
        /// <summary>
        ///  企业ID集合 
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator { get; set; }

    }


    public class DeleteEnterpriseDto
    {
        /// <summary>
        ///  企业ID集合 
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator { get; set; }

    }

}
