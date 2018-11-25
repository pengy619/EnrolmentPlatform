using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// <summary>
    /// 付款单
    /// </summary>
    public class T_PaymentRecordRepository : BaseRepository<T_PaymentRecord>, IT_PaymentRecordRepository
    {
        /// <summary>
        /// 新增付款单
        /// </summary>
        /// <returns></returns>
        public string AddPaymentRecord(PaymentRecordDto dto)
        {
            if (dto.OrderList == null)
            {
                return "没有报名单";
            }
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var orderIdList = dto.OrderList.Select(a => a.OrderId).ToList();
            var orderAmountList = dbContext.T_OrderAmount.Where(a => orderIdList.Contains(a.OrderId) && a.PaymentSource == dto.PaymentSource)
                .ToList();
            //订单缴费金额检查
            foreach (var item in orderAmountList)
            {
                var unPayedAmount = item.TotalAmount - item.ApprovalAmount - item.PayedAmount;
                if (dto.UnitAmount > unPayedAmount)
                {
                    return "本次缴费金额大于报名单缴费金额。";
                }
                item.ApprovalAmount = item.ApprovalAmount + dto.UnitAmount;
            }
            //修改订单缴费金额信息
            foreach (var item in orderAmountList)
            {
                dbContext.Entry(item).State = EntityState.Modified;
            }

            //新增付款单
            T_PaymentRecord payment = new T_PaymentRecord()
            {
                Id = Guid.NewGuid(),
                FilePath = dto.FilePath,
                Name = dto.Name,
                PaymentSource = dto.PaymentSource,
                PaymentSourceId = dto.PaymentSourceId,
                Status = (int)PaymentStatusEnum.Submit,
                TotalAmount = (dto.UnitAmount * dto.OrderList.Count),
                Type = (int)dto.Type,
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
            dbContext.T_PaymentRecord.Add(payment);

            //新增付款单记录
            foreach (var item in dto.OrderList)
            {
                dbContext.T_PaymentInfo.Add(new T_PaymentInfo()
                {
                    Id = Guid.NewGuid(),
                    Amount = dto.UnitAmount,
                    OrderId = item.OrderId,
                    PaymentRecordId = payment.Id,
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
            }

            dbContext.ModuleKey = "缴费";
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = (dto.PaymentSource == 1 ? "机构" : "渠道" + "缴费");
            dbContext.SaveChanges();
            return "";
        }
    }
}
