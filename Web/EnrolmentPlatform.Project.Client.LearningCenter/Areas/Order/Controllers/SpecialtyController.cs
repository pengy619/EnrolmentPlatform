using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Order.Controllers
{
    public class SpecialtyController : BaseController
    {
        #region 农场品订单
        // GET: Order/Specialty
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 订单列表异步搜索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> Search(SearchParamForSpecialtyOrderDTO param)
        {
            param.SupplierId = base.SupplierId;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/GetSpecialtySupplierOrderList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return msg.Data.ToString();
        }
        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Detail(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("orderId", id.ToString());
            parames.Add("supplierId", base.SupplierId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/GetSpecialtyOrderByOrderId", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                return View(msg.Data.ToString().ToObject<OrderDetailForSpecialtyDTO>());
            }
            else
            {
                throw new Exception(msg.Info);
            }
        }
        /// <summary>
        /// 供应商修改价格
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierUpdatePrice(UpdateSpecialtyOrderPriceDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/SupplierUpdatePrice", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        /// <summary>
        /// 供应商发货
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierSendGood(SpcialtyOrderSendGoodDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/SupplierSendGood", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        /// <summary>
        /// 供应商修改收货地址
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierUpdateConsigneeAddress(SpcialtyOrderConsigneeAddressDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/SupplierUpdateConsigneeAddress", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        /// <summary>
        /// 供应商申请预售商品的退款
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierRefund(SupplierSpcialtyOrderRefundDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/SupplierRefund", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        /// <summary>
        /// 供应商处理C端用户的取消
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierHandleRefund(SupplierSpcialtyOrderHandleRefundDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/SupplierHandleRefund", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        #endregion 

        #region 退换货
        /// <summary>
        /// 退换货列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RefundChange(string refundableNo = null)
        {
            ViewBag.RefundableNo = refundableNo;
            return View();
        }
        public async Task<string> RefundChangeSearch(RefundableRecordParam param)
        {
            param.SupplierId = base.SupplierId;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/RefundableRecord/GetSupplierRefundableRecordList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return msg.Data.ToString();
        }
        /// <summary>
        /// 退换货详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> RefundChangeDetail(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", id.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/RefundableRecord/GetRefundableRecordDetailById", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                return View(msg.Data.ToString().ToObject<RefundableRecordDetail>());
            }
            else
            {
                throw new Exception(msg.Info);
            }
        }

        public async Task<ActionResult> SupplierHandleRefundGood(Guid id)
        {
            HandleRefundGoodDTO dto = new HandleRefundGoodDTO();
            dto.Id = id;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/RefundableRecord/SupplierHandleRefundGood", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        #endregion
    }
}