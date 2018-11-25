using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Accounts
{
    /// <summary>
    /// 账号数据处理
    /// </summary>
    public class T_AccountBasicRepository : BaseRepository<T_AccountBasic>, IT_AccountBasicRepository
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="accountId">账号ID</param>
        /// <param name="realOldPwd">加密后原始密码</param>
        /// <param name="realNewPwd">加密后新密码</param>
        /// <returns></returns>
        public bool ChangePwd(Guid accountId, string realOldPwd, string realNewPwd)
        {
            T_AccountBasic accountEntity = this.FindEntityById(accountId);
            if (accountEntity == null || accountEntity.PassWord != realOldPwd)
            {
                return false;
            }

            //如果要修改的密码和原始密码一样
            if (accountEntity.PassWord == realNewPwd)
            {
                //直接返回成功
                return true;
            }

            accountEntity.PassWord = realNewPwd;
            int ret = this.UpdateEntity(accountEntity, Domain.EFContext.E_DbClassify.Write, "修改密码", true, accountEntity.Id.ToString());
            return ret > 0;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int AddUser(UserDto dto)
        {
            int classify = (int)dto.SystemType;
            //检查是否重复名称（todo：鉴于现有系统登陆方式，整个系统不允许出现重复账号，而不是一个供应商下）
            if (this.Count(a => a.AccountNo == dto.UserAccount
                         && a.Classify == classify) > 0) //&& a.EnterpriseId == dto.EnterpriseId
            {
                //账号重复
                return 2;
            }
            T_AccountBasic accountEntity = new T_AccountBasic()
            {
                Classify = classify,
                DepartmentId = dto.DepartmentId,
                EnterpriseId = dto.EnterpriseId,
                Id = dto.UserId.Value,
                IsMaster = dto.IsMaster,
                JobID = dto.JobID,
                PassWord = dto.Password,
                Sex = dto.Sex,
                Remark = dto.Comment,
                RoleId = dto.RoleId,
                Status = (int)dto.Status,
                Phone = dto.Phone,
                AccountNo = dto.UserAccount,
                RealName = dto.UserName,
                Nickname = dto.UserName,
                CreatorAccount = dto.CreateAccount,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.CreateUserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.CreateUserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            };
            //添加至EF
            return this.AddEntity(accountEntity, Domain.EFContext.E_DbClassify.Write, "新增用户【" + accountEntity.AccountNo + "】", true, accountEntity.Id.ToString()) > 0 ? 1 : 3;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int UpdateUser(UserDto dto)
        {
            //不存在
            T_AccountBasic user = this.FindEntityById(dto.UserId.Value);
            if (user == null || user.EnterpriseId != dto.EnterpriseId)
            {
                return 3;
            }

            //检查是否重复名称
            if (this.Count(a => a.AccountNo == dto.UserAccount
                        && a.Id != dto.UserId
                        && a.Classify == user.Classify
                        && a.EnterpriseId == user.EnterpriseId) > 0)
            {
                //账号重复
                return 2;
            }

            //修改实体属性
            user.RealName = dto.UserName;
            user.Nickname = dto.UserName;
            user.Sex = dto.Sex;
            user.DepartmentId = dto.DepartmentId;
            user.JobID = dto.JobID;
            user.Phone = dto.Phone;
            user.RoleId = dto.RoleId;
            user.Remark = dto.Comment;
            user.Status = (int)dto.Status;
            user.LastModifyTime = DateTime.Now;
            user.LastModifyUserId = dto.CreateUserId;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PassWord = dto.Password;
            }
            user.Phone = dto.Phone;
            int ret = this.UpdateEntity(user, Domain.EFContext.E_DbClassify.Write, "修改用户信息", true, user.Id.ToString());
            return ret > 0 ? 1 : 3;

        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public UserDto GetUser(Guid userId)
        {
            T_AccountBasic user = this.FindEntityById(userId);
            UserDto dto = new UserDto()
            {
                Comment = user.Remark,
                IntStatus = user.Status,
                IsMaster = user.IsMaster,
                Password = user.PassWord,
                Picture = user.Picture,
                Phone = user.Phone,
                RoleId = user.RoleId,
                SystemType = (SystemTypeEnum)user.Classify,
                UserAccount = user.AccountNo,
                UserName = user.RealName,
                UserId = user.Id,
                Sex = user.Sex,
                CreateTime = user.CreatorTime,
                EnterpriseId = user.EnterpriseId,
                DepartmentId = user.DepartmentId,
                JobID = user.JobID,
                Nickname = user.Nickname
            };
            return dto;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="roleIds">用户ID集合</param>
        /// <returns></returns>
        public bool DeleteUser(DeleteUserDto userDto)
        {
            //做个检查，非主账号和非本账户才可以删除
            return this.LogicDeleteBy((a =>
            userDto.UserIds.Contains(a.Id)
            && !a.IsMaster
            && a.Id != userDto.CurrentUserIds)) > 0;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userDto">dto</param>
        /// <returns></returns>
        public bool ChangeUserStatus(ChangeUserStatusDto userDto)
        {
            bool result = false;
            if (userDto != null && userDto.UserIds != null && userDto.UserIds.Any())
            {
                var userLst = this.LoadEntities(it => userDto.UserIds.Contains(it.Id)).ToList();
                userLst.ForEach(it => it.Status = (int)userDto.UserStatus);
                result = this.UpdateEntities(userLst) > 0;
            }
            return result;
        }

        /// <summary>
        /// 获得用户列表（供应商）
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<UserDto> GetUserList(UserSearchDto param, out int reCount)
        {
            EnrolmentPlatformDbContext dbContext = GetDbContext();
            var queryable = from a in dbContext.T_AccountBasic
                            join b in dbContext.T_Role on a.RoleId equals b.Id
                            into h
                            from g in h.DefaultIfEmpty()
                            where a.Classify == (int)param.SystemType
                                && a.IsDelete == false
                                && a.EnterpriseId == param.EnterpriseId
                                //&& (param.IsMaster.HasValue == false || a.IsMaster == param.IsMaster)
                                && (param.Status.HasValue == false || a.Status == (int)param.Status.Value)
                                && (param.RoleId.HasValue == false || a.RoleId == param.RoleId.Value)
                            select new UserDto()
                            {
                                UserId = a.Id,
                                UserName = a.RealName,
                                RoleId = a.RoleId,
                                UserAccount = a.AccountNo,
                                RoleName = g.RoleName,
                                IntStatus = a.Status,
                                CreateTime = a.CreatorTime,
                                IsMaster = a.IsMaster
                            };
            if (!string.IsNullOrWhiteSpace(param.UserName))
            {
                queryable = queryable.Where(a => a.UserName.Contains(param.UserName));
            }
            if (!string.IsNullOrWhiteSpace(param.UserAccount))
            {
                queryable = queryable.Where(a => a.UserAccount.Contains(param.UserAccount));
            }
            reCount = queryable.Count();
            queryable = queryable.OrderByDescending(a => a.CreateTime).Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            return queryable.ToList();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public UserLoginDto UserLogin(UserLoginRequestDto dto, out string msg)
        {
            //查找用户
            msg = "";
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var userInfo = dbContext.T_AccountBasic.FirstOrDefault(a => a.Classify == (int)dto.SystemType
                && a.AccountNo == dto.UserAccount);

            //用户错误
            if (userInfo == null)
            {
                return null;
            }

            //校验密码
            dto.Password = Md5.Md5Hash(dto.Password + userInfo.Id.ToString());
            if (dto.Password != userInfo.PassWord)
            {
                return null;
            }
            //判断用户状态
            if (userInfo.Status == 1)
            {
                msg = "账户已禁用，请联系客服。";
                return null;
            }

            //校验企业信息
            var enterprise = dbContext.T_Enterprise.FirstOrDefault(a => a.Id == userInfo.EnterpriseId);
            if (enterprise == null || enterprise.IsDelete == true || enterprise.Status != (int)StatusBaseEnum.Enabled)
            {
                msg = "企业信息被禁用。";
                return null;
            }

            //校验角色是否禁用（只有当不是主账号才会去检查）
            var roleInfo = dbContext.T_Role.FirstOrDefault(a => a.Id == userInfo.RoleId);
            if ((userInfo.IsMaster == false) && (roleInfo == null || roleInfo.Status != (int)StatusBaseEnum.Enabled || userInfo.Status != (int)StatusBaseEnum.Enabled))
            {
                msg = "用户或角色被禁用。";
                return null;
            }

            //返回结果
            return new UserLoginDto()
            {
                RoleId = userInfo.RoleId,
                EnterpriseId = userInfo.EnterpriseId,
                IsMaster = userInfo.IsMaster,
                Phone = userInfo.Phone,
                UserAccount = userInfo.AccountNo,
                UserId = userInfo.Id,
                UserName = userInfo.Nickname
            };
        }

        /// <summary>
        /// 获得用户列表（景区后台）
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<AdminUserListDto> GetAdminUserList(UserSearchDto param, out int reCount)
        {
            EnrolmentPlatformDbContext dbContext = GetDbContext();
            var queryable = from a in dbContext.T_AccountBasic
                            join b in dbContext.T_Role on a.RoleId equals b.Id into btemp
                            from btmp in btemp.DefaultIfEmpty()
                            join c in dbContext.T_Department on a.DepartmentId equals c.Id into ctemp
                            from ctmp in ctemp.DefaultIfEmpty()
                            join d in dbContext.T_Job on a.JobID equals d.Id into dtemp
                            from dtmp in dtemp.DefaultIfEmpty()
                            where a.Classify == (int)param.SystemType
                                && a.IsDelete == false
                            //&& (param.IsMaster.HasValue == false || a.IsMaster == param.IsMaster)
                            select new AdminUserListDto()
                            {
                                UserId = a.Id,
                                UserName = a.RealName,
                                RoleId = a.RoleId,
                                UserAccount = a.AccountNo,
                                RoleName = btmp.RoleName,
                                IntStatus = a.Status,
                                DepartmentId = a.DepartmentId,
                                DepartmentName = ctmp.DepartmentName,
                                JobID = a.JobID,
                                JobName = dtmp.JobName,
                                Phone = a.Phone,
                                Sex = a.Sex,
                                CreateDatetime = a.LastModifyTime,
                                IsMaster = a.IsMaster
                            };

            if (!string.IsNullOrWhiteSpace(param.UserName))
            {
                queryable = queryable.Where(a => a.UserName.Contains(param.UserName));
            }
            reCount = queryable.Count();
            queryable = queryable.OrderByDescending(a => a.CreateDatetime).Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            return queryable.ToList();
        }

        /// <summary>
        /// 获得C端用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<UserDto> GetMemberList(UserSearchDto param, out int reCount)
        {
            EnrolmentPlatformDbContext dbContext = GetDbContext();
            var queryable = from a in dbContext.T_AccountBasic
                            where a.Classify == (int)param.SystemType
                                && a.IsDelete == false
                                && (param.Status.HasValue == false || a.Status == (int)param.Status.Value)
                            select new UserDto()
                            {
                                UserId = a.Id,
                                UserName = a.RealName,
                                Phone = a.Phone,
                                Sex = a.Sex,
                                RoleId = a.RoleId,
                                UserAccount = a.AccountNo,
                                IntStatus = a.Status,
                                CreateTime = a.CreatorTime,
                                Nickname = a.Nickname
                            };
            if (!string.IsNullOrWhiteSpace(param.UserAccount))
            {
                queryable = queryable.Where(a => a.UserAccount.Contains(param.UserAccount));
            }
            reCount = queryable.Count();
            queryable = queryable.OrderByDescending(a => a.CreateTime).Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            return queryable.ToList();
        }

        #region 供应商用户

        /// <summary>
        /// 新增供应商用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int AddSupplierUser(SupplierUserDto dto)
        {
            int classify = (int)dto.SystemType;
            //检查是否重复名称（todo：鉴于现有系统登陆方式，整个系统不允许出现重复账号，而不是一个供应商下）
            if (this.Count(a => a.AccountNo == dto.UserAccount
                         && a.Classify == classify) > 0) //&& a.EnterpriseId == dto.EnterpriseId
            {
                //账号重复
                return 2;
            }
            T_AccountBasic accountEntity = new T_AccountBasic()
            {
                Classify = classify,
                DepartmentId = dto.DepartmentId,
                EnterpriseId = dto.EnterpriseId,
                Id = Guid.NewGuid(),
                IsMaster = dto.IsMaster,
                JobID = dto.JobID,
                PassWord = dto.Password,
                Sex = dto.Sex,
                Remark = dto.Comment,
                RoleId = dto.RoleId,
                Status = (int)dto.Status,
                Phone = dto.Phone,
                AccountNo = dto.UserAccount,
                RealName = dto.UserName,
                Nickname = dto.UserName,
                IsAllowMobileLogin = dto.IsAllowMobileLogin,
                CreatorAccount = dto.CreateAccount,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.CreateUserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.CreateUserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            };
            
            //数据库操作
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            //添加至EF
            dbContext.T_AccountBasic.Add(accountEntity);

            //保存并记录日志
            dbContext.ModuleKey = accountEntity.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "供应商子账户添加";
            return (dbContext.SaveChanges() > 0) ? 1 : 3;
        }

        /// <summary>
        /// 修改供应商用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int UpdateSupplierUser(SupplierUserDto dto)
        {
            //数据库操作
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            //不存在
            T_AccountBasic user = dbContext.T_AccountBasic.Find(dto.UserId.Value);
            if (user == null || user.EnterpriseId != dto.EnterpriseId)
            {
                return 3;
            }

            //检查是否重复名称
            if (this.Count(a => a.AccountNo == dto.UserAccount
                        && a.Id != dto.UserId
                        && a.Classify == user.Classify
                        && a.EnterpriseId == user.EnterpriseId) > 0)
            {
                //账号重复
                return 2;
            }

            //修改实体属性
            user.RealName = dto.UserName;
            user.Nickname = dto.UserName;
            user.Sex = dto.Sex;
            user.DepartmentId = dto.DepartmentId;
            user.JobID = dto.JobID;
            user.Phone = dto.Phone;
            user.IsAllowMobileLogin = dto.IsAllowMobileLogin;
            user.RoleId = dto.RoleId;
            user.Remark = dto.Comment;
            user.Status = (int)dto.Status;
            user.LastModifyTime = DateTime.Now;
            user.LastModifyUserId = dto.CreateUserId;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PassWord = dto.Password;
            }
            user.Phone = dto.Phone;
            dbContext.Entry(user).State = EntityState.Modified;

            //保存并记录日志
            dbContext.ModuleKey = user.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "供应商子账户修改";
            return (dbContext.SaveChanges() > 0) ? 1 : 3;

        }

        /// <summary>
        /// 获得供应商子账户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public SupplierUserDto GetSupplierUser(Guid userId)
        {
            T_AccountBasic user = this.FindEntityById(userId);
            SupplierUserDto dto = new SupplierUserDto()
            {
                Comment = user.Remark,
                IntStatus = user.Status,
                IsMaster = user.IsMaster,
                Password = user.PassWord,
                Picture = user.Picture,
                Phone = user.Phone,
                RoleId = user.RoleId,
                SystemType = (SystemTypeEnum)user.Classify,
                IsAllowMobileLogin = user.IsAllowMobileLogin.HasValue ? user.IsAllowMobileLogin.Value : false,
                UserAccount = user.AccountNo,
                UserName = user.RealName,
                UserId = user.Id,
                Sex = user.Sex,
                CreateTime = user.CreatorTime,
                EnterpriseId = user.EnterpriseId,
                DepartmentId = user.DepartmentId,
                JobID = user.JobID,
                Nickname = user.Nickname
            };
            return dto;
        }

        #endregion
    }
}
