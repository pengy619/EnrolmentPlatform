using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.IBLL.Systems
{ 
    public interface IT_SystemMessageService : IBaseService<T_SystemMessage>
    {
        /// <summary>
        /// 供应商根据条件分页查询系统消息列表
        /// </summary>
        /// <param name="paramForSystemMessageDto"></param>
        /// <returns></returns>
        GridDataResponse GetSystemMessageForSupplierForList(ParamForSystemMessageDto paramForSystemMessageDto);
        /// <summary>
        /// 系统消息列表标记已读
        /// </summary>
        /// <param name="messageIds"></param>
        /// <returns></returns>
        bool MessageOnReadForSupplier(List<Guid> messageIds);

        /// <summary>
        /// 供应商查询首页信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        HomeInfoForSupplierDto GetHomeInfoForSupplierId(Guid supplierId);


        /// <summary>
        /// admin查询首页信息
        /// </summary> 
        /// <returns></returns>
        HomeInfoForSupplierDto GetHomeInfoForAdmin();

        /// <summary>
        /// ADMIN查询首页信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        HomeInfoForAdminDto GetHomeInfoForAdminDtoByTime(string startTime, string endTime);

        ResultMsg SendMessage(Guid[] enterpriseIds, SystemMessageEnum systemMessageEnum, string[] parmas);
        /// <summary>
        /// 供应商查询首页信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        HomeInfoForAdminDto GetHomeInfoForSupplierByTime(string startTime, string endTime, Guid supplierId);
    }
}
