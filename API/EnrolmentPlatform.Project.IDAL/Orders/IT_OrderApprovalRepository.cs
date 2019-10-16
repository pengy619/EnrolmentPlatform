using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IDAL.Orders
{
    public interface IT_OrderApprovalRepository : IBaseRepository<T_OrderApproval>
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        ResultMsg Save(OrderApprovalDto dto);

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        ResultMsg SaveImage(OrderApprovalImgDto dto);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="approvalId">approvalId</param>
        /// <param name="approved">approved</param>
        /// <param name="comment">comment</param>
        /// <returns></returns>
        ResultMsg Approval(Guid approvalId, bool approved, string comment);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="approvalIdList">approvalIdList</param>
        /// <returns></returns>
        ResultMsg Delete(List<Guid> approvalIdList);

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="approvalIdList">approvalIdList</param>
        /// <returns></returns>
        ResultMsg Submit(List<Guid> approvalIdList);

        /// <summary>
        /// 根据订单ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="req">req</param>
        /// <param name="reCount">reCount</param>
        /// <returns></returns>
        List<OrderUpdateApprovalListDto> GetOrderUpdateApprovalList(OrderUpdateApprovalReq req, out int reCount);
    }
}
