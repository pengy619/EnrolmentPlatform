using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Order.Controllers
{
    public class SpecialtyController : BaseController
    {
        /// <summary>
        /// 农产品订单
        /// </summary>
        /// <returns></returns>
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
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/GetSpecialtyScenicOrderList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return msg.Data.ToString();
        }
        /// <summary>
        /// 退换货
        /// </summary>
        /// <returns></returns>
        public ActionResult RefundChange(string refundableNo=null)
        {
            ViewBag.RefundableNo = refundableNo;
            return View();
        }
        public async Task<string> RefundChangeSearch(RefundableRecordParam param)
        {
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/RefundableRecord/GetScenicRefundableRecordList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
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
        /// <summary>
        /// 订单详情
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Detail(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("orderId", id.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/GetScenicSpecialtyOrderByOrderId", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
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
        /// 景区处理供应商申请的退款
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ScenicHandleRefund(ScenicSpcialtyOrderHandleRefundDTO dto)
        {
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderItemForSpecialty/ScenicHandleRefund", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
    }
}