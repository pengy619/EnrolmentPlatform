using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Finance;

namespace EnrolmentPlatform.Project.IBLL.Finance
{
    public interface IT_AccountDetailInfoService : IBaseService<T_AccountDetailInfo>
    {

        /// <summary>
        /// 获取  账户资金流水  分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GridDataResponse GetPageList(AccountDetailInfoListDto request);
    }
}
