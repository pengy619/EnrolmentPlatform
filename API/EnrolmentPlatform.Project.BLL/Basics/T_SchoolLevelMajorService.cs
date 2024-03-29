﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EnrolmentPlatform.Project.Domain.EFContext;
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
                        where a.ParentId == id && b.IsEnable == true
                        select new SchoolItemDto
                        {
                            Id = a.Id,
                            ItemId = b.Id,
                            ItemName = b.Name
                        };
            return query.OrderBy(t => t.ItemName).ToList();
        }

        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <returns></returns>
        public IList<SchoolItemDto> GetAllList()
        {
            var query = from a in schoolLevelMajorRepository.LoadEntities(t => !t.IsDelete)
                        join b in metadataRepository.LoadEntities(t => !t.IsDelete) on a.ItemId equals b.Id
                        select new SchoolItemDto
                        {
                            Id = a.Id,
                            ItemId = b.Id,
                            ItemName = b.Name,
                            ParentId = a.ParentId
                        };
            return query.OrderBy(t => t.ItemName).ToList();
        }

        /// <summary>
        /// 获取学校配置
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<KeyValuePair<Guid, Guid>> GetSchoolConfigList(Guid schoolId)
        {
            var query = from a in schoolLevelMajorRepository.LoadEntities(t => !t.IsDelete)
                        join b in schoolLevelMajorRepository.LoadEntities(t => !t.IsDelete) on a.Id equals b.ParentId
                        where a.ParentId == schoolId
                        select new
                        {
                            Key = a.ItemId,
                            Value = b.ItemId
                        };
            return query.ToList().Select(t => new KeyValuePair<Guid, Guid>(t.Key, t.Value)).ToList();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        public ResultMsg SaveConfig(SchoolConfigDto dto)
        {
            ResultMsg resultMsg = new ResultMsg();
            //先删除原来的配置
            string strSql = "EXEC SP_DeleteSchoolLevelMajors @SchoolId";
            SqlParameter[] Paras = new SqlParameter[]
            {
                new SqlParameter("@SchoolId",dto.SchoolId)
            };
            int n = schoolLevelMajorRepository.ExecSql(strSql, E_DbClassify.Write, Paras);
            //再添加新的配置
            var configs = new List<T_SchoolLevelMajor>();
            if (dto.LevelMajorList != null && dto.LevelMajorList.Any())
            {
                var levelList = dto.LevelMajorList.Select(t => t.LevelId).Distinct().ToList();
                foreach (var levelId in levelList)
                {
                    var level = new T_SchoolLevelMajor
                    {
                        Id = Guid.NewGuid(),
                        ItemId = levelId,
                        Type = (int)MetadataTypeEnum.Level,
                        ParentId = dto.SchoolId,
                        IsEnabled = true
                    };
                    configs.Add(level);
                    var majorList = dto.LevelMajorList.Where(t => t.LevelId == levelId).Select(t => t.MajorId).ToList();
                    if (majorList != null && majorList.Any())
                    {
                        foreach (var majorId in majorList)
                        {
                            var major = new T_SchoolLevelMajor
                            {
                                Id = Guid.NewGuid(),
                                ItemId = majorId,
                                Type = (int)MetadataTypeEnum.Major,
                                ParentId = level.Id,
                                IsEnabled = true
                            };
                            configs.Add(major);
                        }
                    }
                }
            }
            resultMsg.IsSuccess = schoolLevelMajorRepository.AddEntities(configs) > 0;
            return resultMsg;
        }

        /// <summary>
        /// 根据层次、专业查询学校列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<SchoolItemListDto> GetSchoolItemList(SchoolItemListReqDto req, out int reCount)
        {
            return schoolLevelMajorRepository.GetSchoolItemList(req, out reCount);
        }
    }
}
