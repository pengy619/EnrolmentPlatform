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
        /// 获取提现范围
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetWithdrawRange()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this._systemBasicSettingService.GetWithdrawRange();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 设置提现范围
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SetWithdrawRange(WithdrawRangeDTO dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this._systemBasicSettingService.SetWithdrawRange(dto);
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

        /// <summary>
        /// 获取H5标题设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetH5TitleSet()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this._systemBasicSettingService.GetH5TitleSet();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// H5标题设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> H5TitleSet(H5TitleSetDTO dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this._systemBasicSettingService.H5TitleSet(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获取用户注册协议
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetUserProtocolSet()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this._systemBasicSettingService.GetUserProtocolSet();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 用户注册协议设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UserProtocolSet(UserProtocolSetDTO dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this._systemBasicSettingService.UserProtocolSet(dto);
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
