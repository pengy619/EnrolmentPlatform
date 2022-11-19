using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.IDAL.Basics;

namespace EnrolmentPlatform.Project.DAL.Basics
{
    public class T_SchoolLevelMajorRepository : BaseRepository<T_SchoolLevelMajor>, IT_SchoolLevelMajorRepository
    {
        /// <summary>
        /// 根据层次、专业查询学校列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<SchoolItemListDto> GetSchoolItemList(SchoolItemListReqDto req, out int reCount)
        {
            var dbContext = this.GetDbContext();
            var query = from a in dbContext.T_Metadata
                        join b in dbContext.T_SchoolLevelMajor on a.Id equals b.ParentId
                        join l in dbContext.T_Metadata on b.ItemId equals l.Id
                        join c in dbContext.T_SchoolLevelMajor on b.Id equals c.ParentId
                        join m in dbContext.T_Metadata on c.ItemId equals m.Id
                        where a.Type == 1 && a.IsEnable == true && l.IsEnable == true && m.IsEnable == true
                        select new SchoolItemListDto
                        {
                            Tags = a.Tags,
                            SchoolId = a.Id,
                            SchoolName = a.Name,
                            LevelId = l.Id,
                            LevelName = l.Name,
                            MajorId = m.Id,
                            MajorName = m.Name
                        };
            //排除招生机构不可报读的学校
            var schoolIds = dbContext.T_SchoolSetting.Where(t => t.EnterpriseId == req.EnterpriseId).Select(t => t.SchoolId).ToList();
            if (schoolIds.Any())
            {
                query = query.Where(a => !schoolIds.Contains(a.SchoolId));
            }

            //层级
            if (!string.IsNullOrWhiteSpace(req.LevelName))
            {
                query = query.Where(a => a.LevelName.Contains(req.LevelName));
            }

            //专业
            if (!string.IsNullOrWhiteSpace(req.MajorName))
            {
                query = query.Where(a => a.MajorName.Contains(req.MajorName));
            }

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }

            var list = query.OrderBy(o => o.SchoolName).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
            return list;
        }
    }
}
