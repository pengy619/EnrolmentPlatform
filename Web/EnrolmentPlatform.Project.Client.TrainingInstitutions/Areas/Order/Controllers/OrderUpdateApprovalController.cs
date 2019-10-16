using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Order.Controllers
{
    public class OrderUpdateApprovalController : BaseController
    {
        /// <summary>
        /// 订单修改审核列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Search(OrderUpdateApprovalReq param)
        {
            int reCount = 0;
            param.FromChannelId = this.EnterpriseId;
            List<OrderUpdateApprovalListDto> list = OrderApprovalService.GetOrderUpdateApprovalList(param, out reCount);
            if (list == null)
            {
                list = new List<OrderUpdateApprovalListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderInfo()
        {
            return View();
        }

        /// <summary>
        /// 订单图片信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderImageInfo()
        {
            return View();
        }

        /// <summary>
        /// 删除报名单
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Delete(Guid[] ids)
        {
            var ret = OrderApprovalService.Delete(ids.ToList());
            if (ret.IsSuccess == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "删除失败。" });
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Submit(Guid[] ids)
        {
            var ret = OrderApprovalService.Submit(ids.ToList());
            if (ret.IsSuccess == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "提交失败。" });
            }
        }
    }
}