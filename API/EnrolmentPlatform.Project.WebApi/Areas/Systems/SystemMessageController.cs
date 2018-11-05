using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Systems
{
    /// <summary>
    /// 系统消息
    /// 2018-03-19
    /// </summary>
    public class SystemMessageController : ApiBaseController
    {
        private IT_SystemMessageService IT_SystemMessageService;

        public SystemMessageController()
        {
            this.IT_SystemMessageService = DIContainer.Resolve<IT_SystemMessageService>();

        }


        /// <summary>
        /// 供应商根据条件分页查询系统消息列表
        /// </summary>
        /// <param name="paramForSystemMessageDto"></param>
        /// <returns></returns> 
        [HttpPost]
        public async Task<HttpResponseMessage> GetSystemMessageForSupplierForList(ParamForSystemMessageDto paramForSystemMessageDto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = IT_SystemMessageService.GetSystemMessageForSupplierForList(paramForSystemMessageDto);
                return _resultMsg.ToJson().ResponseMessage();
            });
        }

        /// <summary>
        /// 系统消息列表标记已读
        /// </summary>
        /// <param name="messageIds"></param>
        /// <returns></returns> 
        [HttpPost]
        public async Task<HttpResponseMessage> MessageOnReadForSupplier(List<Guid> messageIds)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = IT_SystemMessageService.MessageOnReadForSupplier(messageIds);
                return _resultMsg.ToJson().ResponseMessage();
            });
        }

        /// <summary>
        /// 供应商查询首页信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetHomeInfoForSupplierId(Guid supplierId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = IT_SystemMessageService.GetHomeInfoForSupplierId(supplierId);
                return _resultMsg.ToJson().ResponseMessage();
            });
        }

        /// <summary>
        /// ADMIN查询首页信息
        /// </summary> 
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetHomeInfoForAdmin()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = IT_SystemMessageService.GetHomeInfoForAdmin();
                return _resultMsg.ToJson().ResponseMessage();
            });
        }

        /// <summary>
        /// ADMIN查询首页信息
        /// </summary> 
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetHomeInfoForAdminDtoByTime(string startTime, string endTime)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = IT_SystemMessageService.GetHomeInfoForAdminDtoByTime(startTime, endTime);
                return _resultMsg.ToJson().ResponseMessage();
            });
        }

        /// <summary>
        /// 供应商查询首页信息
        /// </summary> 
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetHomeInfoForSupplierByTime(string startTime, string endTime, Guid supplierId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = IT_SystemMessageService.GetHomeInfoForSupplierByTime(startTime, endTime, supplierId);
                return _resultMsg.ToJson().ResponseMessage();
            });
        }
    }
}