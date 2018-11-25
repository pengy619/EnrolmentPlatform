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
        /// 
        /// </summary>
        /// <returns></returns>
        int GetOrder();
    }
}
