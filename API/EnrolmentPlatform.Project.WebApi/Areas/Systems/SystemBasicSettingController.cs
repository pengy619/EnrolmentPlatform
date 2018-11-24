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
    public class SystemBasicSettingController : ApiBaseController
    {
        protected IT_SystemBasicSettingService _systemBasicSettingService;
        public SystemBasicSettingController()
        {
            this._systemBasicSettingService = DIContainer.Resolve<IT_SystemBasicSettingService>();
        }
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetSystemParameter()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this._systemBasicSettingService.GetSystemParameter();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 设置系统参数
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SystemParameterSet(SystemParameterDTO dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this._systemBasicSettingService.SystemParameterSet(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获取全站设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetTotalStationSet()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this._systemBasicSettingService.GetTotalStationSet();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 全站设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> TotalStationSet(TotalStationSetDTO dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this._systemBasicSettingService.TotalStationSet(dto);
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
