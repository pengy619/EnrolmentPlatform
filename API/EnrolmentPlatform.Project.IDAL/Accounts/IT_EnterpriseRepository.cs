using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;

namespace EnrolmentPlatform.Project.IDAL.Accounts
{
    /// <summary>
    /// 企业操作
    /// </summary>
    public interface IT_EnterpriseRepository : IBaseRepository<T_Enterprise>
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
    }
}
