using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.IBLL.Systems
{
    public interface IT_SystemBasicSettingService : IBaseService<T_SystemBasicSetting>
    {
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <returns></returns>
        SystemParameterDTO GetSystemParameter();

        /// <summary>
        /// 设置系统参数
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg SystemParameterSet(SystemParameterDTO dto);

        /// <summary>
        /// 获取提现范围
        /// </summary>
        /// <returns></returns>
        List<string> GetWithdrawRange();

        /// <summary>
        /// 设置提现范围
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg SetWithdrawRange(WithdrawRangeDTO dto);

        /// <summary>
        /// 获取全站设置
        /// </summary>
        /// <returns></returns>
        TotalStationSetDTO GetTotalStationSet();

        /// <summary>
        /// 全站设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg TotalStationSet(TotalStationSetDTO dto);

        /// <summary>
        /// 获取H5标题设置
        /// </summary>
        /// <returns></returns>
        ResultMsg GetH5TitleSet();

        /// <summary>
        /// H5标题设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg H5TitleSet(H5TitleSetDTO dto);

        /// <summary>
        /// 获取用户注册协议
        /// </summary>
        /// <returns></returns>
        ResultMsg GetUserProtocolSet();

        /// <summary>
        /// 用户注册协议设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg UserProtocolSet(UserProtocolSetDTO dto);
    }
}
