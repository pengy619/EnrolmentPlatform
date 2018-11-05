using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Systems
{
    public class LoginLogController : ApiBaseController
    {
        protected IT_SystemLoginLogService SystemLoginLogService;
        public LoginLogController()
        {
            this.SystemLoginLogService = DIContainer.Resolve<IT_SystemLoginLogService>();
        }
        [HttpPost]
        public async Task<HttpResponseMessage> FindLoginLog(LoginLogDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int records = 0;
                var lst = SystemLoginLogService.GetLoginLog(param, out records);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = records,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }
        /// <summary>
        /// 供应商登登录日志
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetEnterpriseLoginLog(LoginLogDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int records = 0;
                var lst = SystemLoginLogService.GetEnterpriseLoginLog(param, out records);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = records,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }

        public async Task<HttpResponseMessage> AddLoginLog(LoginLogDto param)
        {

            return await Task.Run(() =>
            {
                var model = param.MapTo<LoginLogDto, T_SystemLoginLog>();
                model.Id = Guid.NewGuid();
                ResultMsg _resultMsg = new ResultMsg();
                var _result = SystemLoginLogService.AddEntity(model);
                if (_result <= 0)
                {
                    _resultMsg.IsSuccess = false;
                }
                _resultMsg.Data = _result;
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
