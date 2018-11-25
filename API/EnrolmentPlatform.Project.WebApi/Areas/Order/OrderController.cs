using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.WebApi.Areas.Order
{
    public class OrderController : ApiController
    {
        protected IT_OrderService OrderService;

        public OrderController()
        {
            this.OrderService = DIContainer.Resolve<IT_OrderService>();
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> AddOrder(OrderDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this.OrderService.AddOrder(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrder()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this.OrderService.GetOrder();
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
