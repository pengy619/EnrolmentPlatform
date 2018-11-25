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

        public T_MetadataService()
        {
            this.metadataRepository = DIContainer.Resolve<IT_MetadataRepository>();
            this.orderRepository = DIContainer.Resolve<IT_OrderRepository>();
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
            var exisit = this.metadataRepository.Count(a => a.Type == (int)dto.Type && a.Id != dto.Id && a.Name == dto.Name) > 0;
            if (exisit == true)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = string.Format("该{0}已存在，请勿重复添加", EnumDescriptionHelper.GetDescription((MetadataTypeEnum)entity.Type));
                return _resultMsg;
            }
            entity.Name = dto.Name;
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
                }).ToList();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public GridDataResponse GetPagedList(MetadataSearchDto req)
        {
            GridDataResponse res = new GridDataResponse();
            res.Data = this.metadataRepository.LoadPageEntitiesOrderByField(t => !t.IsDelete && t.Type == (int)req.Type,
               req.Field,
               req.Limit,
               req.Page,
               out int records,
               (req.Sort ?? "desc").ToLower().Equals("asc")
                ).Select(t => new MetadataDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Type = (MetadataTypeEnum)t.Type
                }).ToList();
            res.Count = records;
            return res;
        }
    }
}
