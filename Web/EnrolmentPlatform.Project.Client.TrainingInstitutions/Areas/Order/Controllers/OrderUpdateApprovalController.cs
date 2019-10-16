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
        /// <param name="orderId">orderId</param>
        /// <param name="approvalId">approvalId</param>
        /// <param name="action">action</param>
        /// <returns></returns>
        public ActionResult OrderInfo(Guid? orderId,Guid? approvalId,string action)
        {
            if (orderId.HasValue == false && approvalId.HasValue == false)
            {
                return RedirectToAction("Index", "OrderUpdateApproval");
            }

            OrderDto orderInfo = null;
            if (orderId.HasValue == true)
            {
                var orderApproval = OrderApprovalService.GetOrderApplyApprovalInfoByOrderId(orderId.Value);
                if (orderApproval != null)
                {
                    return OrderInfo(null, orderApproval.ApprovalId, "update");
                }
                orderInfo = OrderService.GetOrder(orderId.Value);
            }
            else
            {
                var orderApproval = OrderApprovalService.GetOrderApplyApprovalInfo(approvalId.Value);
                if (orderApproval == null)
                {
                    return RedirectToAction("Index", "OrderUpdateApproval");
                }
                orderInfo = OrderService.GetOrder(orderApproval.OrderId);
                orderInfo.Address = orderApproval.Address;
                orderInfo.BiYeZhengBianHao = orderApproval.BiYeZhengBianHao;
                orderInfo.CreateUserName = orderApproval.ZhaoShengLaoShi;
                orderInfo.Email = orderApproval.Email;
                orderInfo.GongZuoDanWei = orderApproval.GongZuoDanWei;
                orderInfo.GraduateSchool = orderApproval.GraduateSchool;
                orderInfo.HighesDegree = orderApproval.HighesDegree;
                orderInfo.IDCardNo = orderApproval.IDCardNo;
                orderInfo.JiGuan = orderApproval.JiGuan;
                orderInfo.MinZu = orderApproval.MinZu;
                orderInfo.Phone = orderApproval.Phone;
                orderInfo.Remark = orderApproval.Remark;
                orderInfo.Sex = orderApproval.Sex;
                orderInfo.StudentName = orderApproval.StudentName;
                orderInfo.TencentNo = orderApproval.TencentNo;
                ViewBag.OrderApprovalId = orderApproval.ApprovalId.Value;
            }

            //订单信息
            ViewBag.OrderInfo = orderInfo;
            //批次
            ViewBag.BatchList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Batch);
            //学校
            ViewBag.SchoolList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.School);
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
        /// <returns></returns>
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
        /// <returns></returns>
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
                return Json(new { ret = 0, msg = ret.Info });
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(OrderApprovalDto dto)
        {
            dto.UserId = this.UserId;
            var ret = OrderApprovalService.Save(dto);
            if (ret.IsSuccess == true)
            {
                return Json(new { ret = true, data = ret.Data.ToString() });
            }
            else
            {
                return Json(new { ret = false, msg = ret.Info });
            }
        }

        /// <summary>
        /// 获得层级数据
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLevelData(Guid parentId)
        {
            var list = LevelService.FindSubItemById(parentId);
            return Json(list);
        }
    }
}