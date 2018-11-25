using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Orders
{
    public class T_OrderRepository : BaseRepository<T_Order>, IT_OrderRepository
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败</returns>
        public int AddOrder(OrderDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();

            //价格策略检查
            var nowDate = DateTime.Now.Date;
            var chargeStrategy = dbContext.T_ChargeStrategy.FirstOrDefault(a => a.SchoolMajorId == dto.MajorId
            && nowDate >= a.StartDate && nowDate <= a.EndDate);
            if (chargeStrategy == null)
            {
                //找不到当前时间段的价格策略
                return 2;
            }

            #region 新增处理

            T_Order order = new T_Order()
            {
                Id = Guid.NewGuid(),
                BatchId = dto.BatchId,
                AllOrderImageUpload = false,
                Email = dto.Email,
                StudentName = dto.StudentName,
                IDCardNo = dto.IDCardNo,
                Phone = dto.Phone,
                TencentNo = dto.TencentNo,
                SchoolId = dto.SchoolId,
                LevelId = dto.LevelId,
                MajorId = dto.MajorId,
                Remark = dto.Remark,
                Status = (int)OrderStatusEnum.Init,
                FromTypeName = dto.FromTypeName,
                FromChannelId = dto.FromChannelId,
                CreatorAccount = dto.UserName,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.UserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            };

            //添加订单
            dbContext.T_Order.Add(order);

            //添加订单图片数据
            dbContext.T_OrderImage.Add(new T_OrderImage()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                CreatorAccount = dto.UserName,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.UserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            });

            //添加订单（招生机构）金额数据
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                TotalAmount = chargeStrategy.InstitutionCharge,
                ApprovalAmount = 0,
                PayedAmount = 0,
                UnPayedAmount = 0,
                PaymentSource = 1,
                CreatorAccount = dto.UserName,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.UserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            });

            //添加订单（学习中心）金额数据
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                TotalAmount = chargeStrategy.CenterCharge,
                ApprovalAmount = 0,
                PayedAmount = 0,
                UnPayedAmount = 0,
                PaymentSource = 2,
                CreatorAccount = dto.UserName,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.UserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            });

            dbContext.ModuleKey = order.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "新增报名";
            return dbContext.SaveChanges() > 0 ? 1 : 3;

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetOrder()
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            return dbContext.T_Enterprise.Count();
        }
    }
}
