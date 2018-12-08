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

        /// <summary>
        /// 报名提交（直接为已报名）
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        bool SubmitOrder(List<Guid> orderIdList, Guid userId);

        /// <summary>
        /// 报送中心
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="toLearningCenterId">报送的学习中心</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        bool ToLearningCenter(List<Guid> orderIdList, Guid toLearningCenterId, Guid userId);

        /// <summary>
        /// 录取
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        bool Luqu(List<Guid> orderIdList, Guid userId);

        /// <summary>
        /// 退学
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        bool Leave(List<Guid> orderIdList, Guid userId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="orderIds">orderIds</param>
        /// <returns></returns>
        bool Delete(Guid [] orderIds);

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
        /// <param name="paymentSource">支付发起方（1：招生机构，2：学习中心）</param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        List<OrderPaymentListDto> GetStudentPaymentList(OrderListReqDto req,int paymentSource, ref int reCount);
    }
}
