using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IBLL.Basics
{
    public interface IT_ChargeStrategyService
    {
        /// <summary>
        /// 添加收费策略
        /// </summary>
        /// <returns></returns>
        ResultMsg Add(ChargeStrategyDto dto);

        /// <summary>
        /// 删除收费策略
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(Guid id);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        GridDataResponse GetPagedList(ChargeStrategySearchDto req);
    }
}
