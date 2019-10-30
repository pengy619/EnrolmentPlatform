using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Orders;

namespace EnrolmentPlatform.Project.IDAL.Orders
{
    public interface IT_OrderRepository : IBaseRepository<T_Order>
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败</returns>
        int AddOrder(OrderDto dto);

        /// <summary>
        /// 修改报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        int UpdateOrder(OrderDto dto);

        /// <summary>
        /// 获得报名列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        List<OrderListDto> GetStudentList(OrderListReqDto req, ref int reCount);

        /// <summary>
        /// 获得报名照片列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        List<OrderImageListDto> GetStudentImageList(OrderListReqDto req, ref int reCount);

        /// <summary>
        /// 获得报名缴费列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        List<OrderPaymentListDto> GetStudentPaymentList(OrderListReqDto req, ref int reCount);

        /// <summary>
        /// 修改订单金额
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="amount">金额</param>
        /// <param name="amountType">金额类型</param>
        /// <returns>1：成功，2：找不到，3：金额不能小于已经申请的金额，4失败</returns>
        int UpdateQDAmount(Guid orderId, decimal amount, int amountType);

        /// <summary>
        /// 上传报名单
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        string Upload(List<OrderUploadDto> list);

        /// <summary>
        /// 录取上传
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        string LuQuUpload(List<OrderLuQuUploadDto> list);

        /// <summary>
        /// 获得订单统计
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        OrderStatisticsDto GetOrderStatistics(OrderListReqDto req);

        /// <summary>
        /// 初审上传
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        string AuditUpload(List<OrderAuditUploadDto> list);

        /// <summary>
        /// 机构上传报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        string JiGouUpload(JiGouOrderUploadDto dto);
    }
}
