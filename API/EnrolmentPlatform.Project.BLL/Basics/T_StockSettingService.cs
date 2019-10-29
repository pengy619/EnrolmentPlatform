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
        private IT_MetadataRepository metadataRepository;

        public T_StockSettingService()
        {
            this.stockSettingRepository = DIContainer.Resolve<IT_StockSettingRepository>();
            this.metadataRepository = DIContainer.Resolve<IT_MetadataRepository>();
        }

        /// <summary>
        /// 获得库存列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public GridDataResponse GetStockList(StockListSearchDto dto)
        {
            GridDataResponse grd = new GridDataResponse();
            var query = from a in this.stockSettingRepository.LoadEntities(a => true)
                        join b in this.metadataRepository.LoadEntities(a => true) on a.SchoolId equals b.Id
                        join c in this.metadataRepository.LoadEntities(a => true) on a.LevelId equals c.Id
                        join d in this.metadataRepository.LoadEntities(a => true) on a.MajorId equals d.Id
                        join e in this.metadataRepository.LoadEntities(a => true) on a.BatchId equals e.Id
                        select new StockListDto()
                        {
                            SchoolId = a.SchoolId,
                            LevelId = a.LevelId,
                            MajorId = a.MajorId,
                            SchoolName = b.Name,
                            LevelName = c.Name,
                            MajorName = d.Name,
                            BatchName = e.Name,
                            BatchId =a.BatchId,
                            Name = a.Name,
                            StockSettingId = a.Id,
                            UsedInventory = a.UsedInventory,
                            Inventory = a.Inventory,
                        };

            if (!string.IsNullOrWhiteSpace(dto.SchoolName))
            {
                query = query.Where(a => a.SchoolName.Contains(dto.SchoolName));
            }
            if (!string.IsNullOrWhiteSpace(dto.LevelName))
            {
                query = query.Where(a => a.LevelName.Contains(dto.LevelName));
            }
            if (!string.IsNullOrWhiteSpace(dto.MajorName))
            {
                query = query.Where(a => a.MajorName.Contains(dto.MajorName));
            }
            if (!string.IsNullOrWhiteSpace(dto.BatchName))
            {
                query = query.Where(a => a.BatchName.Contains(dto.BatchName));
            }

            grd.Count = query.Count();
            if (grd.Count > 0)
            {
                grd.Data = query.OrderBy(a => a.BatchName).Skip((dto.Page - 1) * dto.Limit).Take(dto.Limit).ToList();
            }
            return grd;
        }

        /// <summary>
        /// 获得库存设置列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public GridDataResponse GetList(StockSettingSearchDto dto)
        {
            GridDataResponse grd = new GridDataResponse();
            var query = from a in this.stockSettingRepository.LoadEntities(a => a.SchoolId == dto.SchoolId && a.LevelId == dto.LevelId && a.MajorId == dto.MajorId)
                        join b in this.metadataRepository.LoadEntities(a => true) on a.BatchId equals b.Id
                        select new StockSettingDto()
                        {
                            StockSettingId = a.Id,
                            BatchId = a.BatchId,
                            BatchName = b.Name,
                            LevelId = a.LevelId,
                            MajorId = a.MajorId,
                            SchoolId = a.SchoolId,
                            UserId = a.CreatorUserId,
                            UserName = a.CreatorAccount,
                            Name = a.Name,
                            Inventory = a.Inventory,
                            UsedInventory = a.UsedInventory
                        };

            grd.Count = query.Count();
            if (grd.Count > 0)
            {
                grd.Data = query.OrderBy(a => a.BatchName).Skip((dto.Page - 1) * dto.Limit).Take(dto.Limit).ToList();
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
            //检查时间段是否有重复
            var exisitCount = this.stockSettingRepository.LoadEntities(a => a.SchoolId == dto.SchoolId && a.LevelId == dto.LevelId && a.MajorId == dto.MajorId
                && a.BatchId == dto.BatchId).Count();
            if (exisitCount > 0)
            {
                return new ResultMsg() { IsSuccess = false, Info = "该时间段有重复！" };
            }

            //新增
            ResultMsg result = new ResultMsg();
            result.IsSuccess = this.stockSettingRepository.AddEntity(new Domain.Entities.T_StockSetting()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Inventory = dto.Inventory,
                UsedInventory = 0,
                BatchId = dto.BatchId,
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
            //检查时间段是否有重复
            var exisitCount = this.stockSettingRepository.LoadEntities(a => a.Id != dto.StockSettingId && a.SchoolId == dto.SchoolId
                && a.LevelId == dto.LevelId && a.MajorId == dto.MajorId && a.BatchId == dto.BatchId).Count();
            if (exisitCount > 0)
            {
                return new ResultMsg() { IsSuccess = false, Info = "该时间段有重复！" };
            }

            //查找需要修改的库存设置信息
            var stockSetting = this.stockSettingRepository.FindEntityById(dto.StockSettingId.Value);
            if (stockSetting == null)
            {
                return new ResultMsg() { IsSuccess = false, Info = "找不到库存设置信息。" };
            }

            //修改
            ResultMsg result = new ResultMsg();
            stockSetting.LastModifyUserId = dto.UserId;
            stockSetting.LastModifyTime = DateTime.Now;
            stockSetting.Name = dto.Name;
            stockSetting.BatchId = dto.BatchId;
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
                BatchId=stockSetting.BatchId,
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
            var stockSetting = this.stockSettingRepository.FindEntityById(stockId);
            if (stockSetting == null)
            {
                return new ResultMsg() { IsSuccess = false, Info = "找不到库存设置信息。" };
            }
            result.IsSuccess = this.stockSettingRepository.PhysicsDeleteEntity(stockSetting) > 0;
            return result;
        }
    }
}
