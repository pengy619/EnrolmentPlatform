using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.IBLL.Accounts
{
    /// <summary>
    /// 企业服务类接口
    /// </summary>
    public interface IT_EnterpriseService : IBaseService<T_Enterprise>
    {
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        GridDataResponse GetSupplierPageList(SupplierSearchDto param);

        /// <summary>
        /// 新增企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：账号重复，3：失败</returns>
        int AddEnterprice(EnterpriseAddDto dto);

        /// <summary>
        /// 获得供应商信息
        /// </summary>
        /// <returns></returns>
        SupplierEnterpriseGetDto GetSupplierInfo(Guid supplierId);

        /// <summary>
        /// 获得企业信息
        /// </summary>
        /// <returns></returns>
        EnterpriseAddDto GetEnterpriseById(Guid enterpriseId);

        /// <summary>
        /// 修改供应商密码
        /// </summary>
        /// <param name="accountId">账号ID</param>
        /// <param name="oldPwd">原始密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        bool ChangeSupplierPwd(Guid accountId, string oldPwd, string newPwd);

        /// <summary>
        /// 更新企业状态
        /// </summary>
        /// <param name="updateEnterpriseStatusDto"></param>
        /// <returns></returns>
        ResultMsg UpdateEnterpriseStatus(UpdateEnterpriseStatusDto updateEnterpriseStatusDto);

        /// <summary>
        /// 删除企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg DeleteEnterprise(DeleteEnterpriseDto dto);


        /// <summary>
        /// 更新企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg UpdateEnterpriseInfo(EnterpriseAddDto dto);

        /// <summary>
        /// 重置供应商账号密码
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        ResultMsg ResetSupplierAccountPassword(Guid supplierId);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        List<SupplierListDto> GetUserList(SystemTypeEnum classify);

        /// <summary>
        /// 获取招生机构不可报读的学校
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        List<Guid> GetNotSchoolIds(Guid enterpriseId);

        /// <summary>
        /// 保存学校配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg SaveConfig(SchoolSettingDto dto);
    }
}
