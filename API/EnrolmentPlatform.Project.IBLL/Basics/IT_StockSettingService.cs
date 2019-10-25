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
    /// <summary>
    /// 库存设置服务
    /// </summary>
    public interface IT_StockSettingService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        GridDataResponse GetList(StockSettingSearchDto dto);

        /// <summary>
        /// 新增库存设置
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        ResultMsg Add(StockSettingDto dto);

        /// <summary>
        /// 修改库存设置
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        ResultMsg Update(StockSettingDto dto);

        /// <summary>
        /// 查找库存设置
        /// </summary>
        /// <param name="stockId">stockId</param>
        /// <returns></returns>
        StockSettingDto Find(Guid stockId);

        /// <summary>
        /// 删除库存设置
        /// </summary>
        /// <param name="stockId">stockId</param>
        /// <returns></returns>
        ResultMsg Delete(Guid stockId);
    }
}
