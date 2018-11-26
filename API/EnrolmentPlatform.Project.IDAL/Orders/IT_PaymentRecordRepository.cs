using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Orders;

namespace EnrolmentPlatform.Project.IDAL.Orders
{
    /// <summary>
    /// 付款单记录
    /// </summary>
    public interface IT_PaymentRecordRepository : IBaseRepository<T_PaymentRecord>
    {
        /// <summary>
        /// 新增付款单
        /// </summary>
        /// <returns></returns>
        string AddPaymentRecord(PaymentRecordDto dto);

        /// <summary>
        /// 获得缴费登记明细
        /// </summary>
        /// <param name="paymentId">付款单ID</param>
        /// <returns></returns>
        PaymentRecordDto GetInfo(Guid paymentId);

        /// <summary>
        /// 缴费登记审核
        /// </summary>
        /// <param name="paymentId">paymentId</param>
        /// <param name="approved">（拒绝和通过）</param>
        /// <param name="userId">userId</param>
        /// <param name="userName">userName</param>
        /// <param name="comment">审核备注</param>
        /// <returns></returns>
        bool Approval(Guid paymentId, bool approved, Guid userId, string userName, string comment);

        /// <summary>
        /// 查看个人缴费记录
        /// </summary>
        /// <param name="orderId">报名单ID</param>
        /// <param name="paymentSource">支付发起方（1：机构，2：渠道）</param>
        /// <returns></returns>
        PaymentUserDetailDto GetUserDetail(Guid orderId, int paymentSource);
    }
}
