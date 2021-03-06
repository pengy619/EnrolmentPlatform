﻿using System;
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
            var sourceId = dto.PaymentSourceId;

            //订单列表
            var orderList = dbContext.T_Order.Where(a => orderIdList.Contains(a.Id)).ToList();
            if (orderList.Count != orderIdList.Count)
            {
                return "请求失败，请重新发起！";
            }

            //如果是渠道发起
            if (dto.PaymentSource == 2)
            {
                //存在没有录取的报名单
                if (orderList.Exists(a => a.JoinTime.HasValue == false))
                {
                    return "包含不允许缴费的报名单！";
                }

                //一次提交多个学院中心报名单
                if (orderList.Select(a => a.ToLearningCenterId).Distinct().Count() > 1)
                {
                    return "一次只能提交相同的学院中心缴费登记！";
                }
                sourceId = orderList.First().ToLearningCenterId.Value;
            }

            var orderAmountList = dbContext.T_OrderAmount.Where(a => orderIdList.Contains(a.OrderId) && a.PaymentSource == dto.PaymentSource)
                .ToList();

            //总金额
            decimal totalAmount = dto.TotalAmount;
            if (dto.Type == PaymentTypeEnum.Normal)
            {
                //如果是普通缴费，那么总金额等于单价 * 订单数量
                totalAmount = (dto.UnitAmount * dto.OrderList.Count);
            }

            //新增付款单
            T_PaymentRecord payment = new T_PaymentRecord()
            {
                Id = Guid.NewGuid(),
                FilePath = dto.FilePath,
                Name = dto.Name,
                PaymentSource = dto.PaymentSource,
                PaymentSourceId = sourceId,
                Status = (int)PaymentStatusEnum.Submit,
                TotalAmount = totalAmount,
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
                var curUnitPrice = dto.UnitAmount;
                if (dto.Type == PaymentTypeEnum.EndPayment)
                {
                    var curAmount = orderAmountList.Find(a => a.OrderId == item.OrderId);
                    curUnitPrice = curAmount.TotalAmount - curAmount.PayedAmount - curAmount.ApprovalAmount;
                }

                dbContext.T_PaymentInfo.Add(new T_PaymentInfo()
                {
                    Id = Guid.NewGuid(),
                    Amount = curUnitPrice,
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

            //订单缴费金额检查
            foreach (var item in orderAmountList)
            {
                if (item.TotalAmount == item.PayedAmount)
                {
                    return "本次缴费包含已完成的报名单，登记失败。";
                }
                if (dto.Type == PaymentTypeEnum.Normal)
                {
                    var unPayedAmount = item.TotalAmount - item.PayedAmount - item.ApprovalAmount;
                    if (dto.UnitAmount > unPayedAmount)
                    {
                        return "本次缴费金额大于报名单未缴金额。";
                    }

                    //如果是普通缴费，待审核金额 = 待审核金额 + 本次登记金额
                    item.ApprovalAmount = item.ApprovalAmount + dto.UnitAmount;
                }
                else if (dto.Type == PaymentTypeEnum.EndPayment)
                {
                    //如果是尾款，待审核金额 = 总金额 - 已付金额
                    item.ApprovalAmount = item.TotalAmount - item.PayedAmount;
                }
            }
            //修改订单缴费金额信息
            foreach (var item in orderAmountList)
            {
                dbContext.Entry(item).State = EntityState.Modified;
            }

            dbContext.ModuleKey = "缴费";
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = (dto.PaymentSource == 1 ? "机构" : "渠道" + "缴费");
            dbContext.SaveChanges();
            return "";
        }

        /// <summary>
        /// 缴费登记审核
        /// </summary>
        /// <param name="paymentId">paymentId</param>
        /// <param name="approved">（拒绝和通过）</param>
        /// <param name="userId">userId</param>
        /// <param name="userName">userName</param>
        /// <param name="comment">审核备注</param>
        /// <returns></returns>
        public bool Approval(Guid paymentId, bool approved, Guid userId, string userName, string comment)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var payment = dbContext.T_PaymentRecord.Find(paymentId);
            payment.Auditor = userName;
            payment.AuditorId = userId;
            payment.AuditTime = DateTime.Now;
            payment.LastModifyTime = DateTime.Now;
            payment.LastModifyUserId = userId;
            //审核通过
            if (approved == true)
            {
                //修改付款登记状态为审核通过
                payment.Status = (int)PaymentStatusEnum.Approved;
                dbContext.Entry(payment).State = EntityState.Modified;
            }
            else
            {
                //修改付款登记状态为审核拒绝
                payment.Status = (int)PaymentStatusEnum.Reject;
                dbContext.Entry(payment).State = EntityState.Modified;
            }

            //修改订单金额数据
            var paymentInfoList = dbContext.T_PaymentInfo.Where(a => a.PaymentRecordId == paymentId).ToList();
            var orderIdList = paymentInfoList.Select(a => a.OrderId).ToList();
            var orderAmountList = dbContext.T_OrderAmount.Where(a => orderIdList.Contains(a.OrderId) && a.PaymentSource == payment.PaymentSource)
            .ToList();
            foreach (var item in orderAmountList)
            {
                var curPayment = paymentInfoList.FirstOrDefault(a => a.OrderId == item.OrderId);
                //审核通过
                if (approved == true)
                {
                    //修改待审核的金额 - 本次审核的金额
                    item.ApprovalAmount = item.ApprovalAmount - curPayment.Amount;
                    //已支付的金额 + 本次审核的金额
                    item.PayedAmount = item.PayedAmount + curPayment.Amount;
                }
                else
                {
                    //修改待审核的金额 - 本次审核的金额
                    item.ApprovalAmount = item.ApprovalAmount - curPayment.Amount;
                }

                dbContext.Entry(item).State = EntityState.Modified;
            }

            dbContext.ModuleKey = "缴费审核";
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = (payment.PaymentSource == 1 ? "机构" : "渠道" + "缴费审核，审核结果" + (approved ? "通过" : "不通过"));
            dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获得缴费登记明细
        /// </summary>
        /// <param name="paymentId">付款单ID</param>
        /// <returns></returns>
        public PaymentRecordDto GetInfo(Guid paymentId)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var entity = dbContext.T_PaymentRecord.Find(paymentId);
            if (entity == null)
            {
                return null;
            }

            //付款单信息
            PaymentRecordDto payment = new PaymentRecordDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = (PaymentTypeEnum)entity.Type,
                Status = (PaymentStatusEnum)entity.Status,
                Auditor = entity.Auditor,
                AuditorId = entity.AuditorId,
                AuditTime = entity.AuditTime,
                CreateTime = entity.CreatorTime,
                UserName = entity.CreatorAccount,
                UserId = entity.Id,
                FilePath = entity.FilePath,
            };

            //付款单订单
            var orderListQuery = from a in dbContext.T_PaymentInfo
                                 join b in dbContext.T_Order on a.OrderId equals b.Id
                                 join c in dbContext.T_Metadata on b.SchoolId equals c.Id into ctemp
                                 from cctemp in ctemp.DefaultIfEmpty()
                                 join d in dbContext.T_Metadata on b.LevelId equals d.Id into dtemp
                                 from ddtemp in dtemp.DefaultIfEmpty()
                                 join e in dbContext.T_Metadata on b.MajorId equals e.Id into etemp
                                 from eetemp in etemp.DefaultIfEmpty()
                                 join f in dbContext.T_Metadata on b.BatchId equals f.Id into ftemp
                                 from fftemp in ftemp.DefaultIfEmpty()
                                 where a.PaymentRecordId == payment.Id
                                 select new PaymentOrderInfo()
                                 {
                                     StudentName = b.StudentName,
                                     BatchName = fftemp.Name,
                                     SchoolName = cctemp.Name,
                                     LevelName = ddtemp.Name,
                                     MajorName = eetemp.Name,
                                     Amount = a.Amount,
                                     OrderId = a.OrderId
                                 };
            payment.OrderList = orderListQuery.ToList();
            return payment;
        }

        /// <summary>
        /// 查看个人缴费记录
        /// </summary>
        /// <param name="orderId">报名单ID</param>
        /// <param name="paymentSource">支付发起方（1：机构，2：渠道）</param>
        /// <returns></returns>
        public PaymentUserDetailDto GetUserDetail(Guid orderId, int paymentSource)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var query = from a in dbContext.T_Order
                        join b in dbContext.T_Metadata on a.BatchId equals b.Id into btemp
                        from bbtemp in btemp.DefaultIfEmpty()
                        join c in dbContext.T_Metadata on a.SchoolId equals c.Id into ctemp
                        from cctemp in ctemp.DefaultIfEmpty()
                        join d in dbContext.T_Metadata on a.LevelId equals d.Id into dtemp
                        from ddtemp in dtemp.DefaultIfEmpty()
                        join e in dbContext.T_Metadata on a.MajorId equals e.Id into etemp
                        from eetemp in etemp.DefaultIfEmpty()
                        join f in dbContext.T_OrderAmount on new { OrderId = a.Id, PaymentSource = 1 } equals new { OrderId = f.OrderId, PaymentSource = f.PaymentSource } into ftemp
                        from fftemp in ftemp.DefaultIfEmpty()
                        join g in dbContext.T_OrderAmount on new { OrderId = a.Id, PaymentSource = 2 } equals new { OrderId = g.OrderId, PaymentSource = g.PaymentSource } into gtemp
                        from ggtemp in gtemp.DefaultIfEmpty()
                        where a.Id == orderId && fftemp.PaymentSource == paymentSource
                        select new PaymentUserDetailDto()
                        {
                            BatchName = bbtemp.Name,
                            LevelName = ddtemp.Name,
                            MajorName = eetemp.Name,
                            OrderId = a.Id,
                            SchoolName = cctemp.Name,
                            StudentName = a.StudentName,
                            ApprovalAmount = fftemp.ApprovalAmount,
                            PayedAmount = fftemp.PayedAmount,
                            TotalAmount = fftemp.TotalAmount,
                            QDApprovalAmount = ggtemp.ApprovalAmount,
                            QDPayedAmount = ggtemp.PayedAmount,
                            QDTotalAmount = ggtemp.TotalAmount
                        };
            var detail = query.FirstOrDefault();
            //如果有数据
            if (detail != null)
            {
                var listQuery = from a in dbContext.T_PaymentInfo
                                join b in dbContext.T_PaymentRecord on a.PaymentRecordId equals b.Id into btemp
                                from bbtemp in btemp.DefaultIfEmpty()
                                where a.OrderId == orderId
                                orderby a.CreatorTime
                                select new PaymentRecordListDto()
                                {
                                    Auditor = bbtemp.Auditor,
                                    AuditorId = bbtemp.AuditorId,
                                    AuditTime = bbtemp.AuditTime,
                                    Id = bbtemp.Id,
                                    Name = bbtemp.Name,
                                    Status = bbtemp.Status,
                                    TotalAmount = a.Amount,
                                    Type = bbtemp.Type,
                                    UserId = bbtemp.CreatorUserId,
                                    UserName = bbtemp.CreatorAccount,
                                    CreatorTime = bbtemp.CreatorTime,
                                    PaymentSource = bbtemp.PaymentSource,
                                    PaymentSourceId = bbtemp.PaymentSourceId
                                };
                detail.PaymentRecordList = listQuery.ToList();
            }
            return detail;
        }
    }
}
