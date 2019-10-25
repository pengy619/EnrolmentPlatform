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
    /// <summary>
    /// 库存设置服务
    /// </summary>
    public class T_StockSettingService : IT_StockSettingService, IInterceptorLogic
    {
        private IT_StockSettingRepository stockSettingRepository;

        public T_StockSettingService()
        {
            this.stockSettingRepository = DIContainer.Resolve<IT_StockSettingRepository>();
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public GridDataResponse GetList(StockSettingSearchDto dto)
        {
            GridDataResponse grd = new GridDataResponse();
            var query = this.stockSettingRepository.LoadEntities(a => a.SchoolId == dto.SchoolId
              && a.LevelId == dto.LevelId
              && a.MajorId == dto.MajorId);
            grd.Count = query.Count();
            if (grd.Count > 0)
            {
                grd.Data = query.Select(a => new StockSettingDto()
                {
                    StockSettingId = a.Id,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    LevelId = a.LevelId,
                    MajorId = a.MajorId,
                    SchoolId = a.SchoolId,
                    Name = a.Name,
                    Inventory = a.Inventory,
                    UsedInventory = a.UsedInventory
                }).OrderBy(a => a.StartDate).Skip((dto.Page - 1) * dto.Limit).Take(dto.Limit).ToList();
            }
            return grd;
        }

        /// <summary>
        /// 新增库存设置
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public ResultMsg Add(StockSettingDto dto)
        {
            ResultMsg result = new ResultMsg();
            result.IsSuccess = this.stockSettingRepository.AddEntity(new Domain.Entities.T_StockSetting()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Inventory = dto.Inventory,
                UsedInventory = 0,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                SchoolId = dto.SchoolId,
                LevelId = dto.LevelId,
                MajorId = dto.MajorId,
                CreatorAccount = dto.UserName,
                CreatorUserId = dto.UserId,
                CreatorTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                LastModifyTime = DateTime.Now
            }) > 0;
            return result;
        }

        /// <summary>
        /// 修改库存设置
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public ResultMsg Update(StockSettingDto dto)
        {
            ResultMsg result = new ResultMsg();
            var stockSetting = this.stockSettingRepository.FindEntityById(dto.StockSettingId.Value);
            if (stockSetting == null)
            {
                return new ResultMsg() { IsSuccess = false, Info = "找不到库存设置信息。" };
            }
            stockSetting.LastModifyUserId = dto.UserId;
            stockSetting.LastModifyTime = DateTime.Now;
            stockSetting.Name = dto.Name;
            stockSetting.StartDate = dto.StartDate;
            stockSetting.EndDate = dto.EndDate;
            stockSetting.Inventory = dto.Inventory;
            result.IsSuccess = this.stockSettingRepository.UpdateEntity(stockSetting) > 0;
            return result;
        }

        /// <summary>
        /// 查找库存设置
        /// </summary>
        /// <param name="stockId">stockId</param>
        /// <returns></returns>
        public StockSettingDto Find(Guid stockId)
        {
            var stockSetting = this.stockSettingRepository.FindEntityById(stockId);
            if (stockSetting == null)
            {
                return null;
            }
            return new StockSettingDto()
            {
                StockSettingId = stockSetting.Id,
                StartDate = stockSetting.StartDate,
                EndDate = stockSetting.EndDate,
                LevelId = stockSetting.LevelId,
                MajorId = stockSetting.MajorId,
                SchoolId = stockSetting.SchoolId,
                Name = stockSetting.Name,
                Inventory = stockSetting.Inventory,
                UsedInventory = stockSetting.UsedInventory,
                UserId = stockSetting.CreatorUserId,
                UserName = stockSetting.Name
            };
        }

        /// <summary>
        /// 删除库存设置
        /// </summary>
        /// <param name="stockId">stockId</param>
        /// <returns></returns>
        public ResultMsg Delete(Guid stockId)
        {
            ResultMsg result = new ResultMsg();
            var stockSetting = this.stockSettingRepository.FindEntityById(dto.StockSettingId);
            if (stockSetting == null)
            {
                return new ResultMsg() { IsSuccess = false, Info = "找不到库存设置信息。" };
            }
            result.IsSuccess = this.stockSettingRepository.PhysicsDeleteEntity(stockSetting) > 0;
            return result;
        }
    }
}
