using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.IBLL.Basics;
using EnrolmentPlatform.Project.IDAL.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.BLL.Basics
{
    public class T_CustomerFieldService : IT_CustomerFieldService, IInterceptorLogic
    {
        private IT_CustomerFieldRepository customerFieldRepository;

        public T_CustomerFieldService()
        {
            this.customerFieldRepository = DIContainer.Resolve<IT_CustomerFieldRepository>();
        }

        /// <summary>
        /// 获得一个学校所有自定义字段列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public List<CustomerFieldDto> GetAllList(GetAllListSearchDto dto)
        {
            return this.customerFieldRepository.LoadEntities(a => a.SchoolId == dto.SchoolId)
                .OrderBy(a => a.CreatorTime)
                .Select(a => new CustomerFieldDto()
                {
                    CustomerFieldType = a.CustomerFieldType,
                    Id = a.Id,
                    Name = a.Name,
                    SchoolId = a.SchoolId,
                    SelectItems = a.SelectItems,
                    UserId = a.CreatorUserId,
                    UserName = a.CreatorAccount
                }).ToList();
        }

        /// <summary>
        /// 获得一个学校所有自定义字段列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public GridDataResponse GetList(GetAllListSearchDto dto)
        {
            var list = this.GetAllList(dto);
            return new GridDataResponse()
            {
                Count = list.Count,
                Data = list
            };
        }

        /// <summary>
        /// 新增自定义字段
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public ResultMsg Add(CustomerFieldDto dto)
        {
            //检查字段名是否有重复
            var exisitCount = this.customerFieldRepository.LoadEntities(a => a.Name == dto.Name && a.SchoolId == dto.SchoolId).Count();
            if (exisitCount > 0)
            {
                return new ResultMsg() { IsSuccess = false, Info = "该字段重复！" };
            }

            //新增
            ResultMsg result = new ResultMsg();
            result.IsSuccess = this.customerFieldRepository.AddEntity(new Domain.Entities.Basics.T_CustomerField()
            {
                Id = Guid.NewGuid(),
                CustomerFieldType = dto.CustomerFieldType,
                Name = dto.Name,
                SelectItems = dto.SelectItems,
                SchoolId = dto.SchoolId,

                CreatorAccount = dto.UserName,
                CreatorUserId = dto.UserId,
                CreatorTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                LastModifyTime = DateTime.Now
            }) > 0;
            return result;
        }

        /// <summary>
        /// 修改自定义字段
        /// </summary>
        /// <param name="">dto</param>
        /// <returns></returns>
        public ResultMsg Update(CustomerFieldDto dto)
        {
            //检查时间段是否有重复
            var exisitCount = this.customerFieldRepository.LoadEntities(a => a.Id != dto.Id.Value && a.Name == dto.Name && a.SchoolId == dto.SchoolId).Count();
            if (exisitCount > 0)
            {
                return new ResultMsg() { IsSuccess = false, Info = "该字段重复！" };
            }

            //查找需要修改的库存设置信息
            var customerField = this.customerFieldRepository.FindEntityById(dto.Id.Value);
            if (customerField == null)
            {
                return new ResultMsg() { IsSuccess = false, Info = "找不到自定义字段信息。" };
            }

            //修改
            ResultMsg result = new ResultMsg();
            customerField.LastModifyUserId = dto.UserId;
            customerField.LastModifyTime = DateTime.Now;
            customerField.Name = dto.Name;
            customerField.SelectItems = dto.SelectItems
;
            customerField.CustomerFieldType = dto.CustomerFieldType;
            result.IsSuccess = this.customerFieldRepository.UpdateEntity(customerField) > 0;
            return result;
        }

        /// <summary>
        /// 查找自定义字段
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public CustomerFieldDto Find(Guid id)
        {
            var customerField = this.customerFieldRepository.FindEntityById(id);
            if (customerField == null)
            {
                return null;
            }
            return new CustomerFieldDto()
            {
                CustomerFieldType = customerField.CustomerFieldType,
                Id = customerField.Id,
                SelectItems = customerField.SelectItems,
                SchoolId = customerField.SchoolId,
                Name = customerField.Name,
                UserId = customerField.CreatorUserId,
                UserName = customerField.Name
            };
        }

        /// <summary>
        /// 删除自定义字段
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ResultMsg Delete(Guid id)
        {
            ResultMsg result = new ResultMsg();
            var customerField = this.customerFieldRepository.FindEntityById(id);
            if (customerField == null)
            {
                return new ResultMsg() { IsSuccess = false, Info = "找不到自定义字段信息。" };
            }
            result.IsSuccess = this.customerFieldRepository.PhysicsDeleteEntity(customerField) > 0;
            return result;
        }
    }
}
