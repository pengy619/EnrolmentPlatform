using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Accounts
{
    /// <summary>
    /// 企业API
    /// </summary>
    public class EnterpriseController : ApiBaseController
    {
        protected IT_EnterpriseService _enterpriseService;
        public EnterpriseController()
        {
            this._enterpriseService = DIContainer.Resolve<IT_EnterpriseService>();
        }

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        [HttpPost]
        public async Task<HttpResponseMessage> GetSupplierPageList(SupplierSearchDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = _enterpriseService.GetSupplierPageList(param);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 新增企业信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> AddEnterprise(EnterpriseAddDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int ret = this._enterpriseService.AddEnterprice(dto);
                if (ret == 1)
                {
                    _resultMsg.IsSuccess = true;
                }
                else if (ret == 2)
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "账号已经存在。";
                }
                else
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "添加失败。";
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetSupplierInfo(Guid supplierId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this._enterpriseService.GetSupplierInfo(supplierId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获取企业信息
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetEnterpriseById(Guid enterpriseId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this._enterpriseService.GetEnterpriseById(enterpriseId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改供应商密码
        /// </summary>
        /// <param name="accountId">账号ID</param>
        /// <param name="oldPwd">原始密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> ChangeSupplierPwd(Guid accountId, string oldPwd, string newPwd)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = this._enterpriseService.ChangeSupplierPwd(accountId, oldPwd, newPwd);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 更新企业状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateEnterpriseStatus(UpdateEnterpriseStatusDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg = this._enterpriseService.UpdateEnterpriseStatus(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 删除企业 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteEnterprise(DeleteEnterpriseDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg = this._enterpriseService.DeleteEnterprise(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 更新企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateEnterpriseInfo(EnterpriseAddDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg = this._enterpriseService.UpdateEnterpriseInfo(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 重置供应商账号密码
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> ResetSupplierAccountPassword([FromBody]Guid supplierId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg = this._enterpriseService.ResetSupplierAccountPassword(supplierId);
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
