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
    public class T_LogSettingRepository : BaseRepository<T_LogSetting>, IT_LogSettingRepository
    {
        /// <summary>
        /// 根据企业Id查找日志
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        public IList<LogSettingDTO> GetLogSettingByEnterpriseId(LogSettingDTO param, out int records)
        {
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = (from it in _dbcontext.T_LogSetting
                                join account in _dbcontext.T_AccountBasic
                                on it.CreatorUserId equals account.Id
                                where account.EnterpriseId == param.EnterpriseId
                                && ((param.KeyWrod == null || param.KeyWrod.Trim() == string.Empty) ? true : it.BusinessName.Contains(param.KeyWrod))
                                && param.StartDate <= it.CreatorTime && param.EndDate >= it.CreatorTime
                                select new LogSettingDTO
                                {
                                    BusinessName = it.BusinessName,
                                    IP = it.IP,
                                    CreatorTime = it.CreatorTime,
                                    CreatorAccount = account.AccountNo
                                });

            records = _tIQueryable.Count();
            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, "CreatorTime", false);
            _tIQueryable = _tIQueryable.Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            return _tIQueryable.ToList();
        }
        /// <summary>
        /// 根据Kye查找日志
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        public IList<LogSettingDTO> GetLogSettingByKey(LogSettingDTO param, out int records)
        {
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = (from it in _dbcontext.T_LogSetting
                                join account in _dbcontext.T_AccountBasic
                                on it.CreatorUserId equals account.Id
                                where (it.ModuleKey == param.Key || it.PrimaryKey == param.Key)
                                select new LogSettingDTO
                                {
                                    BusinessName = it.BusinessName,
                                    IP = it.IP,
                                    CreatorTime = it.CreatorTime,
                                    CreatorAccount = account.AccountNo
                                });

            records = _tIQueryable.Count();
            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, "CreatorTime", false);
            _tIQueryable = _tIQueryable.Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            return _tIQueryable.ToList();
        }
        /// <summary>
        /// 平台日志
        /// </summary>
        /// <param name="param"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public IList<LogSettingDTO> GetLogSetting_Scenic(LogSettingDTO param, out int records)
        {
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = (from it in _dbcontext.T_LogSetting
                                join account in _dbcontext.T_AccountBasic
                                on it.CreatorUserId equals account.Id
                                where ((param.KeyWrod == null || param.KeyWrod.Trim() == string.Empty) ? true : it.BusinessName.Contains(param.KeyWrod))
                                && param.StartDate <= it.CreatorTime && param.EndDate >= it.CreatorTime
                                select new LogSettingDTO
                                {
                                    BusinessName = it.BusinessName,
                                    IP = it.IP,
                                    CreatorTime = it.CreatorTime,
                                    CreatorAccount = account.AccountNo
                                });

            records = _tIQueryable.Count();
            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, "CreatorTime", false);
            _tIQueryable = _tIQueryable.Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            return _tIQueryable.ToList();
        }
    }
}
