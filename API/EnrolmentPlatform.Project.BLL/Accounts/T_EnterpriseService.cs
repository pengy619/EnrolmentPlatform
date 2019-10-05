using System;
using System.Collections.Generic;
using System.Linq;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.BLL.Accounts
{
    /// <summary>
    /// 企业服务类
    /// </summary>
    public class T_EnterpriseService : BaseService<T_Enterprise>, IT_EnterpriseService, IInterceptorLogic
    {
        private IT_EnterpriseRepository repository = null;
        private IT_AccountBasicRepository accountRepository = null;

        public T_EnterpriseService()
        {
            this.repository = DIContainer.Resolve<IT_EnterpriseRepository>();
            this.accountRepository = DIContainer.Resolve<IT_AccountBasicRepository>();
        }

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = repository;
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        public GridDataResponse GetSupplierPageList(SupplierSearchDto param)
        {
            return this.repository.GetSupplierPageList(param);
        }

        /// <summary>
        /// 新增企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：账号重复，3：失败</returns>
        public int AddEnterprice(EnterpriseAddDto dto)
        {
            return this.repository.AddEnterprice(dto);
        }

        /// <summary>
        /// 获得供应商信息
        /// </summary>
        /// <param name="supplierId">供应商ID</param>
        /// <returns></returns>
        public SupplierEnterpriseGetDto GetSupplierInfo(Guid supplierId)
        {
            return this.repository.GetSupplierInfo(supplierId);
        }

        /// <summary>
        /// 获得企业信息
        /// </summary>
        /// <returns></returns>
        public EnterpriseAddDto GetEnterpriseById(Guid enterpriseId)
        {
            return this.repository.GetEnterpriseById(enterpriseId);
        }

        /// <summary>
        /// 修改供应商密码
        /// </summary>
        /// <param name="accountId">账号ID</param>
        /// <param name="oldPwd">原始密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public bool ChangeSupplierPwd(Guid accountId, string oldPwd, string newPwd)
        {
            string realOldPwd = Md5.Md5Hash(oldPwd + accountId.ToString());
            string realNewPwd = Md5.Md5Hash(newPwd + accountId.ToString());
            return this.accountRepository.ChangePwd(accountId, realOldPwd, realNewPwd);
        }

        /// <summary>
        /// 更新企业状态 
        /// </summary>
        /// <param name="updateEnterpriseStatusDto"></param>
        /// <returns></returns>
        public ResultMsg UpdateEnterpriseStatus(UpdateEnterpriseStatusDto updateEnterpriseStatusDto)
        {
            ResultMsg result = new ResultMsg() { IsSuccess = false };
            if (updateEnterpriseStatusDto.Ids.Count <= 0)
            {
                result.Info = "参数有误！";
                return result;
            }
            var list = repository.LoadEntities(o => updateEnterpriseStatusDto.Ids.Contains(o.Id)).ToList();
            if (list.Count() <= 0)
            {
                result.Info = "非法操作！";
                return result;
            }
            foreach (var m in list)
            {
                m.Status = updateEnterpriseStatusDto.Status;
                m.LastModifyTime = DateTime.Now;
                m.LastModifyUserId = updateEnterpriseStatusDto.OperatorId;
            }
            result.IsSuccess = repository.UpdateEntities(list) > 0;
            return result;
        }

        /// <summary>
        /// 删除企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg DeleteEnterprise(DeleteEnterpriseDto dto)
        {
            ResultMsg result = new ResultMsg() { IsSuccess = false };
            if (dto.Ids.Count <= 0)
            {
                result.Info = "参数有误！";
                return result;
            }
            var list = repository.LoadEntities(o => dto.Ids.Contains(o.Id)).ToList();
            if (list.Count() <= 0)
            {
                result.Info = "非法操作！";
                return result;
            }
            foreach (var m in list)
            {
                m.IsDelete = true;
                m.DeleteTime = DateTime.Now;
                m.DeleteUserId = dto.OperatorId;
            }
            result.IsSuccess = repository.UpdateEntities(list) > 0;
            return result;
        }


        /// <summary>
        /// 更新企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public ResultMsg UpdateEnterpriseInfo(EnterpriseAddDto dto)
        {
            ResultMsg result = new ResultMsg() { IsSuccess = false };
            var model = repository.FindEntityById(dto.EnterpriseId);
            if (model == null)
            {
                result.Info = "参数有误！";
                return result;
            }
            model.EnterpriseName = dto.EnterpriseName;
            model.Contact = dto.Contact;
            model.Phone = dto.Phone;
            model.Remark = dto.Remark;
            model.LastModifyUserId = dto.CurUserId;
            string businessName = string.Format("修改{0}资料", EnumDescriptionHelper.GetDescription((SystemTypeEnum)model.Classify));
            result.IsSuccess = this.repository.UpdateEntity(model, Domain.EFContext.E_DbClassify.Write, businessName, true, model.Id.ToString()) > 0;
            return result;
        }

        /// <summary>
        /// 重置供应商账号密码
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public ResultMsg ResetSupplierAccountPassword(Guid supplierId)
        {
            ResultMsg _resultMsg = new ResultMsg() { IsSuccess = false };
            var account = accountRepository.LoadEntities(t => t.EnterpriseId == supplierId && t.IsMaster == true).FirstOrDefault();
            if (account == null)
            {
                _resultMsg.Info = "参数有误！";
                return _resultMsg;
            }
            account.PassWord = Md5.Md5Hash("abc123456" + account.Id.ToString());
            _resultMsg.IsSuccess = accountRepository.UpdateEntity(account, Domain.EFContext.E_DbClassify.Write, "重置密码", true, account.Id.ToString()) > 0;
            return _resultMsg;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        public List<SupplierListDto> GetUserList(SystemTypeEnum classify)
        {
            return repository.LoadEntities(t => t.IsDelete == false && t.Classify == (int)classify && t.Status == 2)
                .Select(t => new SupplierListDto()
                {
                    SupplierId = t.Id,
                    SupplierName = t.EnterpriseName
                }).OrderBy(t => t.SupplierName).ToList();
        }
    }
}
