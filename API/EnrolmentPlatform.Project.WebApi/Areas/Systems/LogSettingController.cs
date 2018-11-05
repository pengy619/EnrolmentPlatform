using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Systems
{
    public class LogSettingController : ApiBaseController
    {
        protected IT_LogSettingService LogSettingService;
        public LogSettingController()
        {
            this.LogSettingService = DIContainer.Resolve<IT_LogSettingService>();
        }
        /// <summary>
        /// 查询操作日志
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> FindLogSettingAll()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var _lst = LogSettingService.LoadEntities(it => it.IsDelete == false).ToList();
                _resultMsg.Data = _lst;
                return _resultMsg.ResponseMessage();
            });
        }
        /// <summary>
        /// 查询操作日志GridData
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> FindLogSettingByKeyForGridData(LogSettingDTO param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int records = 0;
                var lst = LogSettingService.FindLogSettingByKey(param, out records);
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
        /// 查询企业操作日志GridData
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> FindLogSettingBEnterpriseIdForGridData(LogSettingDTO param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int records = 0;
                var lst = LogSettingService.GetLogSettingByEnterpriseId(param, out records);
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
        /// 景区查看日志
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<HttpResponseMessage> GetLogSetting_ScenicForGridData(LogSettingDTO param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int records = 0;
                var lst = LogSettingService.GetLogSetting_Scenic(param, out records);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = records,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }

    }
}
