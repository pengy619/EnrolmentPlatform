using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_OrderService: IT_OrderService
    {
        private IT_OrderRepository orderRepository;

        public T_OrderService(IT_OrderRepository _orderRepository)
        {
            this.orderRepository = DIContainer.Resolve<IT_OrderRepository>();
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败</returns>
        public int AddOrder(OrderDto dto)
        {
            return this.orderRepository.AddOrder(dto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetOrder()
        {
            return this.orderRepository.GetOrder();
        }
    }
}
