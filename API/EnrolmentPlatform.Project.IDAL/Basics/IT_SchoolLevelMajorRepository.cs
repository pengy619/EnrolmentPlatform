using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Basics;

namespace EnrolmentPlatform.Project.IDAL.Basics
{
    public interface IT_SchoolLevelMajorRepository : IBaseRepository<T_SchoolLevelMajor>
    {
        /// <summary>
        /// 根据层次、专业查询学校列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        List<SchoolItemListDto> GetSchoolItemList(SchoolItemListReqDto req, out int reCount);
    }
}
