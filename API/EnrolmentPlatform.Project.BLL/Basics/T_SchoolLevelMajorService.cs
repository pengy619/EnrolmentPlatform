using System;
using System.Collections.Generic;
using System.Linq;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Basics;
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
                            Id = a.Id,
                            ItemId = b.Id,
                            ItemName = b.Name
                        };
            return query.ToList();
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <returns></returns>
        public ResultMsg AddConfig(SchoolConfigDto dto)
        {
            ResultMsg resultMsg = new ResultMsg();
            var configs = new List<T_SchoolLevelMajor>();
            if (dto.LevelList != null && dto.LevelList.Any())
            {
                foreach (var level in dto.LevelList)
                {
                    var parentId = Guid.NewGuid();
                    var config = new T_SchoolLevelMajor
                    {
                        Id = parentId,
                        ItemId = level.LevelId,
                        Type = (int)MetadataTypeEnum.Level,
                        ParentId = dto.SchoolId,
                        IsEnabled = true
                    };
                    configs.Add(config);
                    if (level.MajorList != null && level.MajorList.Any())
                    {
                        foreach (var major in level.MajorList)
                        {
                            config = new T_SchoolLevelMajor
                            {
                                Id = Guid.NewGuid(),
                                ItemId = major.MajorId,
                                Type = (int)MetadataTypeEnum.Major,
                                ParentId = parentId,
                                IsEnabled = true
                            };
                            configs.Add(config);
                        }
                    }
                }
            }
            resultMsg.IsSuccess = schoolLevelMajorRepository.AddEntities(configs) > 0;
            return resultMsg;
        }
    }
}
