using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IBLL.Orders
{
    public interface IT_OrderApprovalService
    {
        /// <summary>
        /// 根据订单ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderApprovalDto GetOrderApplyApprovalInfo(Guid orderId);

        /// <summary>
        /// 根据订单ID获得待审核的订单图片修改申请
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderApprovalImgDto GetOrderImageApplyApprovalInfo(Guid orderId);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="approvalId">approvalId</param>
        /// <param name="approved">approved</param>
        /// <param name="comment">comment</param>
        /// <returns></returns>
        ResultMsg Approval(Guid approvalId, bool approved, string comment);

        /// <summary>
        /// 根据订单ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderApprovalDto GetOrderUpdateApprovalList(OrderUpdateApprovalReq req, out int reCount);
    }
}
