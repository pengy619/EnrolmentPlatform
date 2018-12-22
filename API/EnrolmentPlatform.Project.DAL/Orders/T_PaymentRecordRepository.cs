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
                var unPayedAmount = item.TotalAmount - item.PayedAmount - item.ApprovalAmount;
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
                        join f in dbContext.T_OrderAmount on a.Id equals f.OrderId into ftemp
                        from fftemp in ftemp.DefaultIfEmpty()
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
                            TotalAmount = fftemp.TotalAmount
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
                                    CreatorTime=bbtemp.CreatorTime
                                };
                detail.PaymentRecordList = listQuery.ToList();
            }
            return detail;
        }
    }
}
