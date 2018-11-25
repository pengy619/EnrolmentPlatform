using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Orders;

namespace EnrolmentPlatform.Project.IBLL.Orders
{
    public interface IT_OrderService
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        int AddOrder(OrderDto dto);

        /// <summary>
        /// 修改报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        int UpdateOrder(OrderDto dto);

        /// <summary>
        /// 获得报名单图片
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderImageDto FindOrderImage(Guid orderId);

        /// <summary>
        /// 修改报名单图片
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        bool UpdateImage(OrderImageDto dto);

        /// <summary>
        /// 获得订单
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        OrderDto GetOrder(Guid id);
    }
}
