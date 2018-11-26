using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_PaymentRecordService: IT_PaymentRecordService
    {
        private IT_PaymentRecordRepository paymentRecordRepository;

        public T_PaymentRecordService()
        {
            this.paymentRecordRepository = DIContainer.Resolve<IT_PaymentRecordRepository>();
        }

        /// <summary>
        /// 新增付款单
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public string AddPaymentRecord(PaymentRecordDto dto)
        {
            return this.paymentRecordRepository.AddPaymentRecord(dto);
        }

        /// <summary>
        /// 获得缴费登记列表
        /// </summary>
        /// <param name="req">req</param>
        /// <param name="reCount">reCount</param>
        /// <returns></returns>
        public List<PaymentRecordListDto> GetPagedList(PaymentRecordListReqDto req, ref int reCount)
        {
            var query = this.paymentRecordRepository.LoadEntities(a => a.IsDelete == false);
            //发起方
            if (req.PaymentSource.HasValue)
            {
                query = query.Where(a => a.PaymentSource == req.PaymentSource.Value);
            }

            //发起方ID
            if (req.PaymentSourceId.HasValue)
            {
                query = query.Where(a => a.PaymentSourceId == req.PaymentSourceId.Value);
            }

            //名称
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                query = query.Where(a => a.Name.Contains(req.Name));
            }

            //开始时间
            if (req.DateFrom.HasValue)
            {
                query = query.Where(a => a.CreatorTime >= req.DateFrom.Value);
            }

            //结束时间
            if (req.DateTo.HasValue)
            {
                var dateTo = req.DateTo.Value.AddDays(1);
                query = query.Where(a => a.CreatorTime < dateTo);
            }

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }
            return query.OrderByDescending(a => a.CreatorTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit)
                .Select(a => new PaymentRecordListDto()
                {
                    Auditor = a.Auditor,
                    AuditorId = a.AuditorId,
                    AuditTime = a.AuditTime,
                    Id = a.Id,
                    Name = a.Name,
                    Status = a.Status,
                    TotalAmount = a.TotalAmount,
                    Type = a.Type,
                    UserId = a.CreatorUserId,
                    UserName = a.CreatorAccount
                })
                .ToList();
        }

        /// <summary>
        /// 获得缴费登记明细
        /// </summary>
        /// <param name="paymentId">付款单ID</param>
        /// <returns></returns>
        public PaymentRecordDto GetInfo(Guid paymentId)
        {
            return this.paymentRecordRepository.GetInfo(paymentId);
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
        public bool Approval(Guid paymentId, bool approved,Guid userId,string userName, string comment)
        {
            try
            {
                return this.paymentRecordRepository.Approval(paymentId, approved, userId, userName, comment);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 缴费登记删除
        /// </summary>
        /// <param name="paymentId">paymentId</param>
        /// <returns></returns>
        public bool Delete(Guid paymentId)
        {
            return this.paymentRecordRepository.LogicDeleteBy(a => a.Id == paymentId) > 0;
        }

        //查看个人缴费记录
    }
}
