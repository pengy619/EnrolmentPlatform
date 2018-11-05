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
    /// <summary>
    /// 岗位API
    /// </summary>
    public class PositionController : ApiBaseController
    {
        protected IT_PositionService PositionService;
        public PositionController()
        {
            this.PositionService = DIContainer.Resolve<IT_PositionService>();
        }

        /// <summary>
        /// 保存岗位信息
        /// </summary>
        /// <param name="JobDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SavePosition(JobDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int ret = 0;
                if (dto.JobId.HasValue)
                {
                    ret = this.PositionService.Update(dto);
                }
                else
                {
                    ret = this.PositionService.Add(dto);
                }

                if (ret == 1)
                {
                    _resultMsg.IsSuccess = true;
                }
                else if (ret == 2)
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "存在重复岗位名称。";
                }
                else
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "保存失败。";
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> DeletePosition(JobDeleteDto dto)
        {
            return await Task.Run(() =>
            {
                List<string> msgList = null;
                ResultMsg _resultMsg = new ResultMsg();
                bool ret = this.PositionService.Delete(dto, out msgList);
                _resultMsg.IsSuccess = ret;
                //已使用岗位
                if (msgList != null && msgList.Count > 0)
                {
                    _resultMsg.Info = "[" + string.Join(",", msgList.ToArray()) + "]已经被使用,不能删除。"; ;
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得岗位列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetJobList(JobSearchDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int reCount = 0;
                var lst = this.PositionService.GetJobList(param, out reCount);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = reCount,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得当前系统可使用岗位列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetJobList()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var lst = this.PositionService.GetJobList();
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = lst.ToJson();
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
