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
    /// 部门API
    /// </summary>
    public class DepartmentController : ApiBaseController
    {
        protected IT_DepartmentService DepartmentService;
        public DepartmentController()
        {
            this.DepartmentService = DIContainer.Resolve<IT_DepartmentService>();
        }

        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SaveDepartment(DepartmentDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int ret = 0;
                if (dto.DepartmentId.HasValue)
                {
                    ret = this.DepartmentService.Update(dto);
                }
                else
                {
                    ret = this.DepartmentService.Add(dto);
                }

                if (ret == 1)
                {
                    _resultMsg.IsSuccess = true;
                }
                else if (ret == 2)
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "存在重复部门名称。";
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
        /// 删除部门
        /// </summary>
        /// <param name="DepartmentDeleteDto">dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteDepartment(DepartmentDeleteDto dto)
        {
            return await Task.Run(() =>
            {
                List<string> msgList= null;
                ResultMsg _resultMsg = new ResultMsg();
                bool ret = this.DepartmentService.Delete(dto, out msgList);
                _resultMsg.IsSuccess = ret;
                //已使用部门
                if (msgList != null && msgList.Count > 0)
                {
                    _resultMsg.Info = "[" + string.Join(",", msgList.ToArray()) + "]已经被使用,不能删除。"; ;
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetDepartmentList(DepartmentSearchDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int reCount = 0;
                var lst = this.DepartmentService.GetDepartmentList(param, out reCount);
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
        /// 获得当前系统可使用部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetDepartmentList()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var lst = this.DepartmentService.GetDepartmentList();
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = lst.ToJson();
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
