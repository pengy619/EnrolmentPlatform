using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Systems
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class T_SystemLoginLogRepository : BaseRepository<T_SystemLoginLog>, IT_SystemLoginLogRepository
    {
        public IList<T_SystemLoginLog> GetEnterpriseLoginLog(LoginLogDto param, out int records)
        {
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = from it in _dbcontext.T_SystemLoginLog
                               join account in _dbcontext.T_AccountBasic
                               on it.AccountId equals account.Id
                               where account.EnterpriseId == param.EnterpriseId
                                &&((param.KeyWrod == null || param.KeyWrod.Trim() == string.Empty) ? true : it.Account.Contains(param.KeyWrod))
                                && param.StartDate <= it.CreatorTime && param.EndDate >= it.CreatorTime
                                select it;
            records = _tIQueryable.Count();
            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, "CreatorTime", false);
            _tIQueryable = _tIQueryable.Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            return _tIQueryable.ToList();
        }
        
    }
}
