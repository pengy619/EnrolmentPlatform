using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Basics;
using EnrolmentPlatform.Project.IBLL.Basics;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.IDAL.Basics;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.BLL.Basics
{
    public class T_MetadataService : IT_MetadataService, IInterceptorLogic
    {
        private IT_MetadataRepository metadataRepository;
        private IT_OrderRepository orderRepository;
        private IT_SchoolSettingRepository schoolSettingRepository;
        private IT_SchoolImageConfigRepository schoolImageConfigRepository;

        public T_MetadataService()
        {
            this.metadataRepository = DIContainer.Resolve<IT_MetadataRepository>();
            this.orderRepository = DIContainer.Resolve<IT_OrderRepository>();
            this.schoolSettingRepository = DIContainer.Resolve<IT_SchoolSettingRepository>();
            this.schoolImageConfigRepository = DIContainer.Resolve<IT_SchoolImageConfigRepository>();
        }

        /// <summary>
        /// 添加元数据
        /// </summary>
        /// <returns></returns>
        public ResultMsg Add(MetadataDto dto)
        {
            ResultMsg _resultMsg = new ResultMsg();
            var exisit = this.metadataRepository.Count(a => a.Type == (int)dto.Type && a.Name == dto.Name) > 0;
            if (exisit == true)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = string.Format("该{0}已存在，请勿重复添加", EnumDescriptionHelper.GetDescription(dto.Type));
                return _resultMsg;
            }

            var entity = new T_Metadata
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Type = (int)dto.Type,
                Tags = dto.Tags,
                IsEnable = true,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatorUserId = dto.CreatorUserId,
                CreatorAccount = dto.CreatorAccount
            };
            _resultMsg.IsSuccess = this.metadataRepository.AddEntity(entity) > 0;
            return _resultMsg;
        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <returns></returns>
        public ResultMsg Update(MetadataDto dto)
        {
            ResultMsg _resultMsg = new ResultMsg();
            var entity = this.metadataRepository.FindEntityById(dto.Id);
            if (entity == null)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = "参数有误";
                return _resultMsg;
            }
            var exisit = this.metadataRepository.Count(a => a.Type == entity.Type && a.Id != dto.Id && a.Name == dto.Name) > 0;
            if (exisit == true)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = string.Format("该{0}已存在，请勿重复添加", EnumDescriptionHelper.GetDescription((MetadataTypeEnum)entity.Type));
                return _resultMsg;
            }
            entity.Name = dto.Name;
            entity.Tags = dto.Tags;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            _resultMsg.IsSuccess = this.metadataRepository.UpdateEntity(entity) > 0;
            return _resultMsg;
        }

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMsg Delete(List<Guid> idList)
        {
            ResultMsg _resultMsg = new ResultMsg { IsSuccess = false };
            if (orderRepository.Count(t => idList.Contains(t.SchoolId)) > 0)
            {
                _resultMsg.Info = "该学校已被使用，不能删除";
            }
            else if (orderRepository.Count(t => idList.Contains(t.LevelId)) > 0)
            {
                _resultMsg.Info = "该层次已被使用，不能删除";
            }
            else if (orderRepository.Count(t => idList.Contains(t.MajorId)) > 0)
            {
                _resultMsg.Info = "该专业已被使用，不能删除";
            }
            else if (orderRepository.Count(t => idList.Contains(t.BatchId)) > 0)
            {
                _resultMsg.Info = "该批次已被使用，不能删除";
            }
            else
            {
                _resultMsg.IsSuccess = this.metadataRepository.PhysicsDeleteBy(t => idList.Contains(t.Id)) > 0;
                _resultMsg.Info = "删除成功";
            }
            return _resultMsg;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public List<MetadataDto> GetList(MetadataTypeEnum type)
        {
            return this.metadataRepository.LoadEntities(a => a.IsDelete == false && a.Type == (int)type)
                .Select(a => new MetadataDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Type = (MetadataTypeEnum)a.Type,
                    CreatorUserId = a.CreatorUserId,
                    CreatorAccount = a.CreatorAccount
                }).OrderBy(t => t.Name).ToList();
        }

        /// <summary>
        /// 获取可用的列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<MetadataDto> GetEnableList(MetadataTypeEnum type)
        {
            return this.metadataRepository.LoadEntities(a => a.IsDelete == false && a.IsEnable == true && a.Type == (int)type)
                .Where(t => type == MetadataTypeEnum.Batch ? t.EndDate >= DateTime.Today : true) //过滤历史批次
                .Select(a => new MetadataDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Type = (MetadataTypeEnum)a.Type,
                    CreatorUserId = a.CreatorUserId,
                    CreatorAccount = a.CreatorAccount
                }).OrderBy(t => t.Name).ToList();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public GridDataResponse GetPagedList(MetadataSearchDto req)
        {
            GridDataResponse res = new GridDataResponse();
            var noName = string.IsNullOrWhiteSpace(req.Name);
            var noTags = string.IsNullOrWhiteSpace(req.Tags);
            res.Data = this.metadataRepository.LoadPageEntitiesOrderByField(t => !t.IsDelete && t.Type == (int)req.Type && 
                        (noName || t.Name.Contains(req.Name)) && (noTags || t.Tags.Contains(req.Tags)),
               req.Field,
               req.Limit,
               req.Page,
               out int records,
               (req.Sort ?? "desc").ToLower().Equals("asc")
                ).Select(t => new MetadataDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Type = (MetadataTypeEnum)t.Type,
                    Tags = t.Tags,
                    IsEnable = t.IsEnable,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate
                }).ToList();
            res.Count = records;
            return res;
        }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public ResultMsg EnableOrDisable(Guid id, bool isEnable)
        {
            ResultMsg result = new ResultMsg() { IsSuccess = false };
            var entity = this.metadataRepository.FindEntityById(id);
            if (entity == null)
            {
                result.Info = "非法操作！";
                return result;
            }
            entity.IsEnable = isEnable;
            result.IsSuccess = this.metadataRepository.UpdateEntity(entity) > 0;
            return result;
        }

        /// <summary>
        /// 根据学习形式获取学校列表
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public List<MetadataDto> GetSchoolListByTags(string tags, Guid? enterpriseId)
        {
            var schoolIds = new List<Guid>();
            if (enterpriseId.HasValue)
            {
                schoolIds = schoolSettingRepository.LoadEntities(t => t.IsDelete == false && t.EnterpriseId == enterpriseId).Select(t => t.SchoolId).ToList();
            }
            var noTags = string.IsNullOrWhiteSpace(tags);
            return this.metadataRepository.LoadEntities(a => a.IsDelete == false && a.IsEnable == true && a.Type == (int)MetadataTypeEnum.School)
                .Where(t => noTags ? true : t.Tags.Contains(tags))
                .Where(t => schoolIds.Any() ? !schoolIds.Contains(t.Id) : true)
                .Select(a => new MetadataDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Type = (MetadataTypeEnum)a.Type,
                    CreatorUserId = a.CreatorUserId,
                    CreatorAccount = a.CreatorAccount
                }).OrderBy(t => t.Name).ToList();
        }

        /// <summary>
        /// 获取学校必须上传的证件
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<int> GetSchoolImageTypes(Guid schoolId)
        {
            return schoolImageConfigRepository.LoadEntities(t => t.IsDelete == false && t.SchoolId == schoolId)
                .Select(t => t.ImageType).ToList();
        }

        /// <summary>
        /// 获取学校必须上传的证件
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="isZsb"></param>
        /// <returns></returns>
        public List<int> GetSchoolImageTypes(Guid schoolId, bool isZsb = false)
        {
            var imageTypes = GetSchoolImageTypes(schoolId);
            if (isZsb && !imageTypes.Contains(7)) //报考专升本则教育部学历证书电子备案表为必上传
            {
                imageTypes.Add(7);
            }
            if (imageTypes.Count == 0) //当学校没有设置上传证件时返回所有的
            {
                imageTypes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            }
            return imageTypes;
        }

        /// <summary>
        /// 保存学校证件配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg SaveSchoolImageConfig(SchoolImageConfigDto dto)
        {
            ResultMsg resultMsg = new ResultMsg();
            //先删除原来的配置
            schoolImageConfigRepository.PhysicsDeleteBy(t => t.SchoolId == dto.SchoolId);
            //再添加新的配置
            if (dto.ImageTypeList != null && dto.ImageTypeList.Any())
            {
                var configs = new List<T_SchoolImageConfig>();
                foreach (var item in dto.ImageTypeList)
                {
                    configs.Add(new T_SchoolImageConfig
                    {
                        Id = Guid.NewGuid(),
                        SchoolId = dto.SchoolId,
                        ImageType = Convert.ToInt32(item.value),
                        CreatorUserId = dto.CreatorUserId
                    });
                }
                resultMsg.IsSuccess = schoolImageConfigRepository.AddEntities(configs) > 0;
            }
            return resultMsg;
        }
    }
}
