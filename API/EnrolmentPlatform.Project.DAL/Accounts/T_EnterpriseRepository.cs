using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.File;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

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
                                   BusinessType = a.BusinessType,
                                   LoginAccount = b.AccountNo,
                                   Contact = a.Contact,
                                   SettlementCycle = a.SettlementCycle,
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
                Address = dto.Address,
                AddressId = dto.AddressId,
                Balance = 0,
                BusinessRang = string.Join(",", dto.BusinessRang),
                BusinessType = dto.BusinessType.HasValue ? (int)dto.BusinessType : 0,
                CashPassWord = "",
                Classify = (int)dto.Classify,
                Contact = dto.Contact,
                CreatorAccount = dto.CurUserAccount,
                CreatorUserId = dto.CurUserId,
                CreatorTime = DateTime.Now,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                EnterpriseName = dto.EnterpriseName,
                Fax = "无",
                Id = Guid.NewGuid(),
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.CurUserId,
                LegalPerson = "无",
                Phone = dto.Phone,
                Rate = dto.Rate,
                DepositAmount = dto.DepositAmount,
                Remark = dto.Remark,
                RowVersion = null,
                SettlementCycle = (int)dto.SettlementCycle,
                Status = (int)StatusBaseEnum.Enabled,
                TaxNo = "无",
                Unix = DateTime.Now.ConvertDateTimeInt(),
                LicenseNo = dto.LicenseNo,
                BusinessEndDate = dto.BusinessEndDate,
                SupplierType = (int)dto.SupplierType
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

            #region 相关图片
            if (!string.IsNullOrEmpty(dto.BusinessLicenseUrl))
            {
                //营业执照文件
                T_File file1 = new T_File
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
                    ForeignKeyId = enterprise.Id,
                    Iscover = false,
                    IsFocus = false
                };
                dbContext.T_File.Add(file1);
            }
            //身份证正面
            if (!string.IsNullOrEmpty(dto.IDCardReverseUrl))
            {
                T_File file2 = new T_File
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
                    FileBusinessType = (int)EnterpriseFileTypeEnum.IDCardUpwards,
                    FileClassify = 1,
                    FileName = "身份证正面",
                    FilePath = dto.IDCardReverseUrl,
                    ForeignKeyClassify = 3,
                    ForeignKeyId = enterprise.Id,
                    Iscover = false,
                    IsFocus = false
                };
                dbContext.T_File.Add(file2);
            }
            //身份证反面
            if (!string.IsNullOrEmpty(dto.IDCardUpwardsUrl))
            {
                T_File file3 = new T_File
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
                    FileBusinessType = (int)EnterpriseFileTypeEnum.IDCardReverse,
                    FileClassify = 1,
                    FileName = "身份证反面",
                    FilePath = dto.IDCardUpwardsUrl,
                    ForeignKeyClassify = 3,
                    ForeignKeyId = enterprise.Id,
                    Iscover = false,
                    IsFocus = false
                };
                dbContext.T_File.Add(file3);
            }
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

            //企业基础信息
            string[] rangIds = enterprise.BusinessRang.Split(',');
            dto.BusinessRang = "";
            if (rangIds != null && rangIds.Length > 0)
            {
                foreach (var item in rangIds)
                {
                    BusinessRangEnum curEnum = (BusinessRangEnum)Convert.ToInt32(item);
                    dto.BusinessRang += EnumDescriptionHelper.GetDescription(curEnum) + ",";
                }
                dto.BusinessRang = dto.BusinessRang.TrimEnd(',');
            }
            dto.BusinessType = EnumDescriptionHelper.GetDescription((BusinessRangEnum)enterprise.BusinessType);
            dto.Contact = enterprise.Contact;
            dto.DepositAmount = enterprise.DepositAmount;
            dto.EnterpriseName = enterprise.EnterpriseName;
            dto.SupplierType = EnumDescriptionHelper.GetDescription((SupplierTypeEnum)enterprise.SupplierType);
            //企业图片信息
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            List<T_File> fileList = dbContext.T_File.Where(a => a.ForeignKeyClassify == 3 &&
                    a.ForeignKeyId == supplierId && a.IsDelete == false).ToList();
            if (fileList != null && fileList.Count > 0)
            {
                foreach (var item in fileList)
                {
                    if (item.FileBusinessType == (int)EnterpriseFileTypeEnum.BusinessLicense)
                    {
                        dto.BusinessLicenseUrl = item.FilePath;
                    }
                    else if (item.FileBusinessType == (int)EnterpriseFileTypeEnum.IDCardUpwards)
                    {
                        dto.IDCardUpwardsUrl = item.FilePath;
                    }
                    else if (item.FileBusinessType == (int)EnterpriseFileTypeEnum.IDCardReverse)
                    {
                        dto.IDCardReverseUrl = item.FilePath;
                    }
                }
            }

            dto.UserAccount = "";
            dto.Phone = enterprise.Phone;
            dto.Rate = enterprise.Rate;
            dto.SettlementCycle = EnumDescriptionHelper.GetDescription((SettlementCycleEnum)enterprise.SettlementCycle);

            //地址
            T_Address address = dbContext.T_Address.FirstOrDefault(a => a.Id == enterprise.AddressId);
            dto.FullAddress = "";
            if (address != null)
            {
                dto.FullAddress = address.ChinaRoute + enterprise.Address;
            }
            else
            {
                dto.FullAddress = enterprise.Address;
            }

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
                SupplierType = (SupplierTypeEnum)enterprise.SupplierType,
                BusinessRang = enterprise.BusinessRang.Split(',').ToList(),
                Contact = enterprise.Contact,
                Phone = enterprise.Phone,
                AddressId = enterprise.AddressId,
                Address = enterprise.Address,
                SettlementCycle = (SettlementCycleEnum)enterprise.SettlementCycle,
                Rate = enterprise.Rate,
                DepositAmount = enterprise.DepositAmount,
                LicenseNo = enterprise.LicenseNo,
                BusinessEndDate = enterprise.BusinessEndDate,
                Remark = enterprise.Remark
            };
            if (enterprise.BusinessType != 0)
            {
                dto.BusinessType = (EnterpriseBusinessTypeEnum)enterprise.BusinessType;
            }

            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            //企业账号信息
            T_AccountBasic account = dbContext.T_AccountBasic.FirstOrDefault(t => t.Classify == enterprise.Classify && t.EnterpriseId == enterpriseId && t.IsMaster == true);
            if (account != null)
            {
                dto.UserAccount = account.AccountNo;
            }

            //企业图片信息
            List<T_File> fileList = dbContext.T_File.Where(a => a.ForeignKeyClassify == (int)ForeignKeyClassifyEnum.Enterprise &&
                    a.ForeignKeyId == enterpriseId && a.IsDelete == false).ToList();
            if (fileList != null && fileList.Count > 0)
            {
                foreach (var item in fileList)
                {
                    if (item.FileBusinessType == (int)EnterpriseFileTypeEnum.BusinessLicense)
                    {
                        dto.BusinessLicenseUrl = item.FilePath;
                    }
                    else if (item.FileBusinessType == (int)EnterpriseFileTypeEnum.IDCardUpwards)
                    {
                        dto.IDCardUpwardsUrl = item.FilePath;
                    }
                    else if (item.FileBusinessType == (int)EnterpriseFileTypeEnum.IDCardReverse)
                    {
                        dto.IDCardReverseUrl = item.FilePath;
                    }
                }
            }

            return dto;
        }

        /// <summary>
        /// 根据供应商id得到是否自营，获取发布产品是否需要审核
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns>true:不需要审核  false 需要审核</returns>
        public bool GetSupplierIsNeedAdult(Guid SupplierId)
        {
            bool result = false;
            if (!SupplierId.Equals(Guid.Empty))
            {
                T_Enterprise t_Enterprise = this.FindEntityById(SupplierId);
                if (t_Enterprise != null && t_Enterprise.SupplierType.Equals((int)SupplierTypeEnum.Self))
                {
                    int isNeedAdultBySelfSupplier = (int)SystemBasicSettingEnum.IsNeedAdultBySelfSupplier;
                    T_SystemBasicSetting t_SystemBasicSetting = IT_SystemBasicSettingRepository.LoadEntities(a => a.Key == isNeedAdultBySelfSupplier).FirstOrDefault();
                    result = (t_SystemBasicSetting != null && t_SystemBasicSetting.Value.Equals("0"));
                }
            }
            return result;
        }

    }
}
