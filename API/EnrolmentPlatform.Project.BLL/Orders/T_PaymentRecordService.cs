using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_PaymentRecordService: IT_PaymentRecordService
    {
        private IT_PaymentRecordRepository paymentRecordRepository;
        private IT_EnterpriseRepository enterpriseRepository;

        public T_PaymentRecordService()
        {
            this.paymentRecordRepository = DIContainer.Resolve<IT_PaymentRecordRepository>();
            this.enterpriseRepository= DIContainer.Resolve<IT_EnterpriseRepository>();
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
            var query = from a in this.paymentRecordRepository.LoadEntities(i => i.IsDelete == false)
                        join b in this.enterpriseRepository.LoadEntities(j => j.IsDelete == false) on a.PaymentSourceId equals b.Id
                        into btemp
                        from bbtemp in btemp.DefaultIfEmpty()
                        select new PaymentRecordListDto()
                        {
                            Id = a.Id,
                            Auditor = a.Auditor,
                            AuditorId = a.AuditorId,
                            AuditTime = a.AuditTime,
                            Name = a.Name,
                            OrgName = bbtemp.EnterpriseName,
                            Status = a.Status,
                            TotalAmount = a.TotalAmount,
                            Type = a.Type,
                            UserId = a.CreatorUserId,
                            UserName = a.CreatorAccount,
                            CreatorTime = a.CreatorTime,
                            PaymentSource = a.PaymentSource,
                            PaymentSourceId = a.PaymentSourceId.Value
                        };
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

            //状态
            if (req.Status.HasValue)
            {
                query = query.Where(a => a.Status == (int)req.Status.Value);
            }

            //名称
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                query = query.Where(a => a.Name.Contains(req.Name));
            }

            //招生机构/学习中心
            if (!string.IsNullOrWhiteSpace(req.OrgName))
            {
                query = query.Where(a => a.OrgName.Contains(req.OrgName));
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
            return query.OrderByDescending(a => a.CreatorTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
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

        /// <summary>
        /// 查看个人缴费记录
        /// </summary>
        /// <param name="orderId">报名单ID</param>
        /// <param name="paymentSource">支付发起方（1：机构，2：渠道）</param>
        /// <returns></returns>
        public PaymentUserDetailDto GetUserDetail(Guid orderId, int paymentSource)
        {
            return this.paymentRecordRepository.GetUserDetail(orderId, paymentSource);
        }
    }
}
