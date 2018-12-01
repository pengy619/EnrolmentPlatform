using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.IBLL.Basics;
using EnrolmentPlatform.Project.IDAL.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Basics
{
    public class T_SchoolLevelMajorService : IT_SchoolLevelMajorService, IInterceptorLogic
    {
        private IT_SchoolLevelMajorRepository schoolLevelMajorRepository;
        private IT_MetadataRepository metadataRepository;

        public T_SchoolLevelMajorService()
        {
            this.schoolLevelMajorRepository = DIContainer.Resolve<IT_SchoolLevelMajorRepository>();
            this.metadataRepository = DIContainer.Resolve<IT_MetadataRepository>();
        }

        /// <summary>
        /// 查找子项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SchoolItemDto> FindSubItemById(Guid id)
        {
            var query = from a in schoolLevelMajorRepository.LoadEntities(t => !t.IsDelete)
                        join b in metadataRepository.LoadEntities(t => !t.IsDelete) on a.ItemId equals b.Id
                        where a.ParentId == id
                        select new SchoolItemDto
                        {
                            ItemId = b.Id,
                            ItemName = b.Name
                        };
            return query.ToList();
        }
    }
}
