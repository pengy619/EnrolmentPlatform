using System;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using System.Linq;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.Infrastructure.Castle;
using EnrolmentPlatform.Project.DTO.Enums.Systems;

namespace EnrolmentPlatform.Project.BLL.Accounts
{
    /// <summary>
    /// 企业服务类
    /// </summary>
    public class T_EnterpriseService : BaseService<T_Enterprise>, IT_EnterpriseService, IInterceptorLogic
    {
        private IT_EnterpriseRepository repository = null;
        private IT_AccountBasicRepository accountRepository = null;
        protected IDbContextFactory _dbContextFactory;

        public T_EnterpriseService()
        {
            this.repository = DIContainer.Resolve<IT_EnterpriseRepository>();
            this.accountRepository = DIContainer.Resolve<IT_AccountBasicRepository>();
        }

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = repository;
            base.AddDisposableObject(base.CurrentRepository);

            _dbContextFactory = DIContainer.Resolve<IDbContextFactory>();
            base.AddDisposableObject(_dbContextFactory);
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
            model.Contact = dto.Contact;
            model.EnterpriseName = dto.EnterpriseName;
            model.Phone = dto.Phone;
            model.Remark = dto.Remark;
            model.LastModifyUserId = dto.CurUserId;

            #region 更新数据库
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    this.repository.UpdateEntity(model, Domain.EFContext.E_DbClassify.Write, "修改企业资料", true, model.Id.ToString());                    
                    tran.Commit();
                    result.IsSuccess = true;
                    result.Info = string.Empty;
                }
                catch (Exception ex)
                {
                    result.Info = ex.Message;
                    tran.Rollback();
                }
            }

            #endregion


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
            var account = accountRepository.LoadEntities(t => t.Classify == (int)SystemTypeEnum.LearningCenter && t.EnterpriseId == supplierId && t.IsMaster == true).FirstOrDefault();
            if (account == null)
            {
                _resultMsg.Info = "参数有误！";
                return _resultMsg;
            }
            account.PassWord = Md5.Md5Hash("abc123456" + account.Id.ToString());
            _resultMsg.IsSuccess = accountRepository.UpdateEntity(account, Domain.EFContext.E_DbClassify.Write, "重置密码", true, account.Id.ToString()) > 0;
            return _resultMsg;
        }
    }
}
