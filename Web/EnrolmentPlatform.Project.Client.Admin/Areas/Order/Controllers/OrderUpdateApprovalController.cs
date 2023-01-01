using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Order.Controllers
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
        /// <param name="approvalId">approvalId</param>
        /// <returns></returns>
        public ActionResult OrderInfo(Guid approvalId)
        {
            var orderApproval = OrderApprovalService.GetOrderApplyApprovalInfo(approvalId);
            if (orderApproval == null)
            {
                return RedirectToAction("Index", "OrderUpdateApproval");
            }

            var orderInfo = OrderService.GetOrder(orderApproval.OrderId);
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
            orderInfo.SuoDuZhuanYe = orderApproval.SuoDuZhuanYe;
            orderInfo.IsTvUniversity = orderApproval.IsTvUniversity;
            orderInfo.GraduationTime = orderApproval.GraduationTime;
            orderInfo.CustomerField = orderApproval.CustomerField;
            orderInfo.SchoolId = orderApproval.SchoolId;
            orderInfo.LevelId = orderApproval.LevelId;
            orderInfo.MajorId = orderApproval.MajorId;
            orderInfo.BatchId = orderApproval.BatchId;
            ViewBag.OrderApprovalId = orderApproval.ApprovalId.Value;

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
        /// <param name="approvalId">approvalId</param>
        /// <returns></returns>
        public ActionResult OrderImageInfo(Guid approvalId)
        {
            var approval = OrderApprovalService.GetOrderApplyApprovalInfo(approvalId);
            if (approval == null)
            {
                return RedirectToAction("Index", "OrderUpdateApproval");
            }

            //报名单信息
            var orderInfo = OrderService.GetOrder(approval.OrderId);
            orderInfo.Address = approval.Address;
            orderInfo.BiYeZhengBianHao = approval.BiYeZhengBianHao;
            orderInfo.CreateUserName = approval.ZhaoShengLaoShi;
            orderInfo.Email = approval.Email;
            orderInfo.GongZuoDanWei = approval.GongZuoDanWei;
            orderInfo.GraduateSchool = approval.GraduateSchool;
            orderInfo.HighesDegree = approval.HighesDegree;
            orderInfo.IDCardNo = approval.IDCardNo;
            orderInfo.JiGuan = approval.JiGuan;
            orderInfo.MinZu = approval.MinZu;
            orderInfo.Phone = approval.Phone;
            orderInfo.Remark = approval.Remark;
            orderInfo.Sex = approval.Sex;
            orderInfo.StudentName = approval.StudentName;
            orderInfo.TencentNo = approval.TencentNo;
            orderInfo.SuoDuZhuanYe = approval.SuoDuZhuanYe;
            orderInfo.IsTvUniversity = approval.IsTvUniversity;
            orderInfo.GraduationTime = approval.GraduationTime;
            orderInfo.CustomerField = approval.CustomerField;
            ViewBag.OrderApprovalId = approval.ApprovalId.Value;
            ViewBag.OrderInfo = orderInfo;
            ViewBag.ApprovalInfo = approval;

            //批次
            var batchList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Batch);
            ViewBag.BatchName = batchList.Find(a => a.Id == orderInfo.BatchId).Name;

            //学校
            var schoolList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.School);
            //层级
            var levelList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Level);
            //专业
            var majorList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Major);
            var levelName = levelList.Find(a => a.Id == orderInfo.LevelId).Name;
            var biyeInfo = schoolList.Find(a => a.Id == orderInfo.SchoolId).Name + " " + levelName
                + " " + majorList.Find(a => a.Id == orderInfo.MajorId).Name;
            ViewBag.BiYeInfo = biyeInfo;

            //照片信息
            ViewBag.ImageDto = OrderApprovalService.GetOrderImageApplyApprovalInfo(approval.ApprovalId.Value);
            //学校必须上传的证件
            ViewBag.ImageTypes = MetadataService.GetSchoolImageTypes(orderInfo.SchoolId, levelName == "专升本");
            ViewBag.ApprovalId = approvalId;
            return View();
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="approved">approved</param>
        /// <param name="comment">comment</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Approval(Guid[] ids, bool approved,string comment)
        {
            var ret = OrderApprovalService.Approval(ids.ToList(), approved, comment);
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
        /// 获得层级数据
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLevelData(Guid parentId)
        {
            var list = SchoolConfigService.FindSubItemById(parentId);
            return Json(list);
        }

        /// <summary>
        /// 文件列表
        /// </summary>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        public string FileList(Guid approvalId)
        {
            List<FileDto> list = FileService.GetFileList(approvalId);
            GridDataResponse grid = new GridDataResponse
            {
                Count = list.Count,
                Data = list
            };
            return grid.ToJson();
        }

        #region 自定义字段

        /// <summary>
        /// 获得自定义字段列表
        /// </summary>
        /// <param name="schooldId">schooldId</param>
        /// <returns></returns>
        public JsonResult GetCustomerFieldList(Guid schooldId)
        {
            var list = CustomerFieldService.GetAllList(new DTO.Basics.GetAllListSearchDto() { SchoolId = schooldId });
            return Json(list);
        }

        #endregion
    }
}