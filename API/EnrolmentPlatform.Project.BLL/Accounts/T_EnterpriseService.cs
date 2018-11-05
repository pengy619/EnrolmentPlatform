using System;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.DTO.Enums.File;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Accounts
{
    /// <summary>
    /// 企业服务类
    /// </summary>
    public class T_EnterpriseService : BaseService<T_Enterprise>, IT_EnterpriseService, IInterceptorLogic
    {
        private IT_EnterpriseRepository repository = null;
        private IT_AccountBasicRepository accountRepository = null;
        private IT_FileRepository _fileRepository;
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
            _fileRepository = DIContainer.Resolve<IT_FileRepository>();
            base.AddDisposableObject(_fileRepository);

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
        ///  获取某个企业账户余额
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public SupplierBalanceDto GetSupplierBalance(Guid supplierId)
        {
            SupplierBalanceDto result = new SupplierBalanceDto();
            var model = repository.FindEntityById(supplierId);
            result.Balance = model.Balance;
            result.Id = model.Id;
            result.Name = model.EnterpriseName;
            result.IsSetPassword = !string.IsNullOrEmpty(model.CashPassWord);
            return result;
        }


        /// <summary>
        /// 设置 提现密码
        /// </summary>
        /// <param name="setCashPasswordDto"></param>
        /// <returns></returns>
        public ResultMsg SetCashPassword(SetCashPasswordDto setCashPasswordDto)
        {
            try
            {
                ResultMsg result = new ResultMsg() { IsSuccess = false };
                var model = repository.FindEntityById(setCashPasswordDto.EnterpriseId);
                if (model == null)
                {
                    result.Info = "非法操作！";
                    return result;
                }
                model.CashPassWord = Md5.Md5Hash(setCashPasswordDto.Password + model.Id.ToString());
                model.LastModifyTime = DateTime.Now;
                model.LastModifyUserId = setCashPasswordDto.OperatorId;
                // repository.UpdateEntity(model);
                repository.UpdateEntity(model, Domain.EFContext.E_DbClassify.Write, "设置提现密码");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
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
            model.Address = dto.Address;
            model.AddressId = dto.AddressId;
            model.BusinessRang = string.Join(",", dto.BusinessRang);
            model.BusinessType = dto.BusinessType.HasValue ? (int)dto.BusinessType : 0;
            model.Contact = dto.Contact;
            model.EnterpriseName = dto.EnterpriseName;
            model.Phone = dto.Phone;
            model.Rate = dto.Rate;
            model.DepositAmount = dto.DepositAmount;
            model.Remark = dto.Remark;
            model.SettlementCycle = (int)dto.SettlementCycle;
            model.LicenseNo = dto.LicenseNo;
            model.BusinessEndDate = dto.BusinessEndDate;
            model.SupplierType = (int)dto.SupplierType;
            model.LastModifyUserId = dto.CurUserId;
            #region   图片处理 
            var list = _fileRepository.LoadEntities(o => o.ForeignKeyId == model.Id && o.FileClassify == (int)FileClassifyEnum.Picture && o.ForeignKeyClassify == (int)ForeignKeyClassifyEnum.Enterprise).ToList();

            #region  身份证反面照片
            var iDCardReverse = list.FirstOrDefault(t => t.FileBusinessType == (int)FileBusinessTypeEnum.IDCardNegative);
            if (iDCardReverse != null && !string.IsNullOrEmpty(dto.IDCardReverseUrl))
            {
                iDCardReverse.FilePath = dto.IDCardReverseUrl;
                iDCardReverse.LastModifyTime = DateTime.Now;
                iDCardReverse.LastModifyUserId = dto.CurUserId;
            }
            #endregion

            #region  身份证正面照片

            var IDCardUpwardsUrl = list.FirstOrDefault(t => t.FileBusinessType == (int)FileBusinessTypeEnum.IDCardPositive);
            if (IDCardUpwardsUrl != null && !string.IsNullOrEmpty(dto.IDCardUpwardsUrl))
            {
                IDCardUpwardsUrl.FilePath = dto.IDCardUpwardsUrl;
                IDCardUpwardsUrl.LastModifyTime = DateTime.Now;
                IDCardUpwardsUrl.LastModifyUserId = dto.CurUserId;
            }
            #endregion

            #region  营业执照照片
            var BusinessLicenseUrl = list.FirstOrDefault(t => t.FileBusinessType == (int)FileBusinessTypeEnum.License);
            T_File file3 = null;
            if (BusinessLicenseUrl == null && !string.IsNullOrEmpty(dto.BusinessLicenseUrl))
            {
                file3 = new T_File
                {
                    Id = Guid.NewGuid(),
                    IsDelete = false,
                    LastModifyTime = DateTime.Now,
                    LastModifyUserId = dto.CurUserId,
                    CreatorAccount = dto.CurUserAccount,
                    CreatorUserId = dto.CurUserId,
                    CreatorTime = DateTime.Now,
                    DeleteUserId = Guid.Empty,
                    DeleteTime = DateTime.MaxValue,
                    Unix = DateTime.Now.ConvertDateTimeInt(),
                    FileBusinessType = (int)EnterpriseFileTypeEnum.BusinessLicense,
                    FileClassify = 1,
                    FileName = "营业执照",
                    FilePath = dto.BusinessLicenseUrl,
                    ForeignKeyClassify = 3,
                    ForeignKeyId = model.Id,
                    Iscover = false,
                    IsFocus = false
                };

            }
            else
            {
                if (BusinessLicenseUrl != null)
                {
                    if (!string.IsNullOrEmpty(dto.BusinessLicenseUrl))
                    {
                        BusinessLicenseUrl.FilePath = dto.BusinessLicenseUrl;
                        BusinessLicenseUrl.LastModifyTime = DateTime.Now;
                        BusinessLicenseUrl.LastModifyUserId = dto.CurUserId;
                    }
                    else
                    {
                        BusinessLicenseUrl.IsDelete = true;
                        BusinessLicenseUrl.DeleteTime = DateTime.Now;
                        BusinessLicenseUrl.DeleteUserId = dto.CurUserId;
                    }
                }
            }
            #endregion

            #endregion

            #region 更新数据库
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    this.repository.UpdateEntity(model, Domain.EFContext.E_DbClassify.Write, "修改企业资料", true, model.Id.ToString());
                    this._fileRepository.UpdateEntities(list);
                    if (file3 != null)
                    {
                        this._fileRepository.AddEntity(file3);
                    }
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
            var account = accountRepository.LoadEntities(t => t.Classify == (int)EnterpriceTypeEnum.Supplier && t.EnterpriseId == supplierId && t.IsMaster == true).FirstOrDefault();
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
