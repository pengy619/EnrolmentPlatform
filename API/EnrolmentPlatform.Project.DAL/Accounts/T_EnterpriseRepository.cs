using System;
using System.Linq;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Accounts
{
    /// <summary>
    /// 企业接口
    /// </summary>
    public class T_EnterpriseRepository : BaseRepository<T_Enterprise>, IT_EnterpriseRepository
    {
        private IT_SystemBasicSettingRepository IT_SystemBasicSettingRepository;
        public T_EnterpriseRepository()
        {
            this.IT_SystemBasicSettingRepository = DIContainer.Resolve<IT_SystemBasicSettingRepository>();
        }

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public GridDataResponse GetSupplierPageList(SupplierSearchDto param)
        {
            var res = new GridDataResponse();
            int _classfiy = (int)SystemTypeEnum.LearningCenter; //供应商类型企业
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = from a in _dbcontext.T_Enterprise.Where(t => t.IsDelete == false && t.Classify == _classfiy)
                               join b in _dbcontext.T_AccountBasic.Where(t => t.IsMaster == true)
                               on a.Id equals b.EnterpriseId
                               where (param.Status.HasValue ? a.Status == param.Status.Value : true)
                               && (!(param.SupplierName == null || param.SupplierName.Trim() == string.Empty) ? a.EnterpriseName.Contains(param.SupplierName) : true)
                               && (!(param.LoginAccount == null || param.LoginAccount.Trim() == string.Empty) ? b.AccountNo.Contains(param.LoginAccount) : true)
                               select new SupplierListDto
                               {
                                   SupplierId = a.Id,
                                   SupplierName = a.EnterpriseName,
                                   LoginAccount = b.AccountNo,
                                   Contact = a.Contact,
                                   Status = a.Status
                               };

            res.Count = _tIQueryable.Count();
            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, "SupplierId", false);
            _tIQueryable = _tIQueryable.Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            res.Data = _tIQueryable.ToList();
            return res;
        }

        /// <summary>
        /// 新增企业信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：账号重复，3：失败</returns>
        public int AddEnterprice(EnterpriseAddDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            #region  基础信息
            T_Enterprise enterprise = new T_Enterprise
            {
                Classify = (int)dto.Classify,
                Contact = dto.Contact,
                CreatorAccount = dto.CurUserAccount,
                CreatorUserId = dto.CurUserId,
                CreatorTime = DateTime.Now,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                EnterpriseName = dto.EnterpriseName,
                Id = Guid.NewGuid(),
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.CurUserId,
                Phone = dto.Phone,
                Remark = dto.Remark,
                RowVersion = null,
                Status = (int)StatusBaseEnum.Enabled,
                Unix = DateTime.Now.ConvertDateTimeInt()
            };

            dbContext.T_Enterprise.Add(enterprise);

            #endregion

            #region 主账号信息

            T_AccountBasic account = new T_AccountBasic
            {
                Classify = enterprise.Classify,
                Id = Guid.NewGuid(),
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.CurUserId,
                CreatorAccount = dto.CurUserAccount,
                Phone = dto.Phone
            };
            account.PassWord = Md5.Md5Hash(dto.UserPwd + account.Id.ToString());
            account.CreatorUserId = dto.CurUserId;
            account.CreatorTime = DateTime.Now;
            account.DeleteTime = DateTime.MaxValue;
            account.DeleteUserId = Guid.Empty;
            account.Unix = DateTime.Now.ConvertDateTimeInt();
            account.DepartmentId = Guid.Empty;
            account.RoleId = Guid.Empty;
            account.EnterpriseId = enterprise.Id;
            account.IsMaster = true;
            account.JobID = Guid.Empty;
            account.Nickname = "管理员";
            account.AccountNo = dto.UserAccount;
            account.RealName = enterprise.Contact;
            account.Remark = "主账号";
            account.Status = (int)StatusBaseEnum.Enabled;

            //检查账号是否重复（todo：鉴于现有系统登陆方式，整个系统不允许出现重复账号，而不是一个供应商下）
            if (dbContext.T_AccountBasic.Count(a => a.AccountNo == dto.UserAccount
                        && a.Classify == account.Classify) > 0) //&& a.EnterpriseId == dto.EnterpriseId
            {
                //账号重复
                return 2;
            }
            dbContext.T_AccountBasic.Add(account);
            #endregion

            dbContext.ModuleKey = enterprise.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "企业添加";
            return dbContext.SaveChanges() > 0 ? 1 : 3;
        }

        /// <summary>
        /// 获得供应商信息
        /// </summary>
        /// <returns></returns>
        public SupplierEnterpriseGetDto GetSupplierInfo(Guid supplierId)
        {
            T_Enterprise enterprise = this.FindEntityById(supplierId);
            SupplierEnterpriseGetDto dto = new SupplierEnterpriseGetDto();
            
            dto.Contact = enterprise.Contact;
            dto.EnterpriseName = enterprise.EnterpriseName;

            dto.UserAccount = "";
            dto.Phone = enterprise.Phone;

            return dto;
        }

        /// <summary>
        /// 获得企业信息
        /// </summary>
        /// <returns></returns>
        public EnterpriseAddDto GetEnterpriseById(Guid enterpriseId)
        {
            T_Enterprise enterprise = this.FindEntityById(enterpriseId);
            EnterpriseAddDto dto = new EnterpriseAddDto
            {
                EnterpriseId = enterprise.Id,
                EnterpriseName = enterprise.EnterpriseName,
                Contact = enterprise.Contact,
                Phone = enterprise.Phone,
                Remark = enterprise.Remark
            };

            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            //企业账号信息
            T_AccountBasic account = dbContext.T_AccountBasic.FirstOrDefault(t => t.Classify == enterprise.Classify && t.EnterpriseId == enterpriseId && t.IsMaster == true);
            if (account != null)
            {
                dto.UserAccount = account.AccountNo;
            }

            return dto;
        }
    }
}
