using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Payment.Controllers
{
    public class ManagerController : BaseController
    {
        /// <summary>
        /// 缴费单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountIndex()
        {
            //招生机构
            ViewBag.TrainingList = GetUserList(SystemTypeEnum.TrainingInstitutions);
            //学习中心
            ViewBag.LearningList = GetUserList(SystemTypeEnum.LearningCenter);
            return View();
        }

        /// <summary>
        /// 机构缴费列表
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgPaymentIndex()
        {
            return View();
        }

        /// <summary>
        /// 用户缴费记录
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPaymentRecord(Guid? orderId)
        {
            if (orderId.HasValue == false)
            {
                return RedirectToAction("AccountIndex");
            }
            PaymentUserDetailDto paymentUserDetailDto = PaymentRecordService.GetUserDetail(orderId.Value, 1);
            if (paymentUserDetailDto == null)
            {
                return RedirectToAction("AccountIndex");
            }
            ViewBag.Dto = paymentUserDetailDto;
            return View();
        }

        /// <summary>
        /// 缴费添加
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentAdd()
        {
            string orderIds = Request.QueryString["OrderIds"];
            if (string.IsNullOrWhiteSpace(orderIds))
            {
                return RedirectToAction("AccountIndex");
            }
            string[] orderIdArr = orderIds.Split('|');
            ViewBag.PersonCount = orderIdArr.Length;
            ViewBag.OrderList = orderIds;
            return View();
        }

        /// <summary>
        /// 学院中心缴费查看
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentDetail(Guid? paymentId)
        {
            if (paymentId.HasValue == false)
            {
                return RedirectToAction("Index");
            }

            PaymentRecordDto dto = PaymentRecordService.GetInfo(paymentId.Value);
            ViewBag.Dto = dto;

            return View();
        }

        /// <summary>
        /// 机构缴费查看
        /// </summary>
        /// <returns></returns>
        public ActionResult OrgPaymentDetail(Guid? paymentId)
        {
            if (paymentId.HasValue == false)
            {
                return RedirectToAction("OrgPaymentIndex");
            }

            PaymentRecordDto dto = PaymentRecordService.GetInfo(paymentId.Value);
            ViewBag.Dto = dto;

            return View("PaymentDetail");
        }

        /// <summary>
        /// 查询学习管理列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Search(OrderListReqDto param)
        {
            int reCount = 0;
            List<OrderPaymentListDto> list = OrderService.GetStudentPaymentList(param, ref reCount);
            if (list == null)
            {
                list = new List<OrderPaymentListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 查询缴费登记列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string PaymentSearch(PaymentRecordListReqDto param)
        {
            int reCount = 0;
            List<PaymentRecordListDto> list = PaymentRecordService.GetPagedList(param, ref reCount);
            if (list == null)
            {
                list = new List<PaymentRecordListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="param">param</param>
        /// <returns></returns>
        public ActionResult Export(OrderListReqDto param)
        {
            int reCount = 0;
            param.Page = 1;
            param.Limit = int.MaxValue;
            List<OrderPaymentListDto> list = OrderService.GetStudentPaymentList(param, ref reCount);
            if (list == null || list.Count == 0)
            {
                return Content("没有任何可以导出的数据！");
            }

            #region 导出

            HSSFWorkbook hssfworkbook = null;
            try
            {
                using (FileStream file = new FileStream(this.Server.MapPath("~/Temp/OrderAmountTemp.xls"), FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception ex)
            {
            }

            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheet("data");

            int startRow = 2;
            var tempDataRow = list.First();
            HSSFRow tempRow = (HSSFRow)sheet.GetRow(startRow);
            tempRow.Cells[0].SetCellValue(tempDataRow.StudentName);
            tempRow.Cells[1].SetCellValue(tempDataRow.ToLearningCenterName);
            tempRow.Cells[2].SetCellValue(tempDataRow.BatchName);
            tempRow.Cells[3].SetCellValue(tempDataRow.SchoolName);
            tempRow.Cells[4].SetCellValue(tempDataRow.LevelName);
            tempRow.Cells[5].SetCellValue(tempDataRow.MajorName);
            tempRow.Cells[6].SetCellValue(tempDataRow.TotalAmount.ToString("N2"));
            tempRow.Cells[7].SetCellValue(tempDataRow.PayedAmount.ToString("N2"));
            tempRow.Cells[8].SetCellValue(tempDataRow.UnPayedAmount.ToString("N2"));
            tempRow.Cells[9].SetCellValue(tempDataRow.ApprovalAmount.ToString("N2"));
            tempRow.Cells[10].SetCellValue(tempDataRow.QDTotalAmount.ToString("N2"));
            tempRow.Cells[11].SetCellValue(tempDataRow.QDPayedAmount.ToString("N2"));
            tempRow.Cells[12].SetCellValue(tempDataRow.QDUnPayedAmount.ToString("N2"));
            tempRow.Cells[13].SetCellValue(tempDataRow.QDApprovalAmount.ToString("N2"));
            tempRow.Cells[14].SetCellValue(tempDataRow.StatusName);
            tempRow.Cells[15].SetCellValue(tempDataRow.CreateTimeStr);
            tempRow.Cells[16].SetCellValue(tempDataRow.CreateUserName);

            for (int a = 1; a < list.Count; a++)
            {
                HSSFRow row = (HSSFRow)sheet.CreateRow(startRow + a);
                row.HeightInPoints = tempRow.HeightInPoints;
                row.Height = tempRow.Height;

                //创建列
                for (int c = 0; c < tempRow.Cells.Count; c++)
                {
                    ICell cell = row.CreateCell(c);
                    ICell sourceCell = tempRow.GetCell(c);
                    cell.CellStyle = sourceCell.CellStyle;
                    cell.SetCellType(sourceCell.CellType);
                }
                var dto = list[a];
                row.Cells[0].SetCellValue(dto.StudentName);
                row.Cells[1].SetCellValue(dto.ToLearningCenterName);
                row.Cells[2].SetCellValue(dto.BatchName);
                row.Cells[3].SetCellValue(dto.SchoolName);
                row.Cells[4].SetCellValue(dto.LevelName);
                row.Cells[5].SetCellValue(dto.MajorName);
                row.Cells[6].SetCellValue(dto.TotalAmount.ToString("N2"));
                row.Cells[7].SetCellValue(dto.PayedAmount.ToString("N2"));
                row.Cells[8].SetCellValue(dto.UnPayedAmount.ToString("N2"));
                row.Cells[9].SetCellValue(dto.ApprovalAmount.ToString("N2"));
                row.Cells[10].SetCellValue(dto.QDTotalAmount.ToString("N2"));
                row.Cells[11].SetCellValue(dto.QDPayedAmount.ToString("N2"));
                row.Cells[12].SetCellValue(dto.QDUnPayedAmount.ToString("N2"));
                row.Cells[13].SetCellValue(dto.QDApprovalAmount.ToString("N2"));
                row.Cells[14].SetCellValue(dto.StatusName);
                row.Cells[15].SetCellValue(dto.CreateTimeStr);
                row.Cells[16].SetCellValue(dto.CreateUserName);
            }

            //导出
            this.NPOIExport("学习管理" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", hssfworkbook, new List<HSSFSheet> { sheet });

            #endregion

            return null;
        }

        /// <summary>
        /// 提交付款单
        /// </summary>
        /// <param name="orderIdStr"></param>
        /// <param name="name"></param>
        /// <param name="unitAmount"></param>
        /// <param name="file"></param>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SavePayment(string orderIdStr, string name, decimal unitAmount, HttpPostedFileBase file, PaymentTypeEnum paymentType)
        {
            //订单数量检查
            if (string.IsNullOrWhiteSpace(orderIdStr))
            {
                return Json(new { ret = true, msg = "没有任何报名单！" });
            }
            var orderIdArr = orderIdStr.Split('|');
            List<Guid> orderIdList = new List<Guid>();
            foreach (var item in orderIdArr)
            {
                orderIdList.Add(Guid.Parse(item));
            }

            //上传的图片地址
            string fileUrl = this.ImageUpload(file);
            List<PaymentOrderInfo> paymentOrders = orderIdList.Select(a => new PaymentOrderInfo() { OrderId = a, Amount = unitAmount }).ToList();
            PaymentRecordDto dto = new PaymentRecordDto()
            {
                UserId = this.UserId,
                UserName = this.UserUser,
                CreateTime = DateTime.Now,
                PaymentSource = 2,
                Name = name,
                FilePath = fileUrl,
                Type = paymentType,
                UnitAmount = unitAmount,
                OrderList = paymentOrders
            };
            string msg = PaymentRecordService.AddPaymentRecord(dto);
            if (msg != "")
            {
                return Json(new { ret = false, msg = msg });
            }
            return Json(new { ret = true });
        }

        /// <summary>
        /// 删除付款单
        /// </summary>
        /// <param name="paymentId">付款单</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Delete(Guid paymentId)
        {
            var ret = PaymentRecordService.Delete(paymentId);
            if (ret == true)
            {
                return Json(new { ret = true });
            }
            else
            {
                return Json(new { ret = false, msg = "删除失败。" });
            }
        }

        /// <summary>
        /// 修改渠道金额
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="amount">金额</param>
        /// <param name="amountType">金额类型</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateAmount(Guid orderId,decimal amount,int amountType)
        {
            var ret = OrderService.UpdateQDAmount(orderId, amount, amountType);
            if (ret == 1)
            {
                return Json(new { ret = true });
            }
            else if (ret == 2)
            {
                return Json(new { ret = false, msg = "系统错误！" });
            }
            else if (ret == 3)
            {
                return Json(new { ret = false, msg = "金额不能小于已经存在的金额" });
            }
            else
            {
                return Json(new { ret = false, msg = "修改失败！" });
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="paymentId">付款单</param>
        /// <param name="approved"></param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Approval(Guid paymentId, bool approved)
        {
            var ret = PaymentRecordService.Approval(paymentId, approved, this.UserId, this.UserUser, "");
            if (ret == true)
            {
                return Json(new { ret = true });
            }
            else
            {
                return Json(new { ret = false, msg = "系统错误。" });
            }
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<SupplierListDto> GetUserList(SystemTypeEnum type)
        {
            List<SupplierListDto> list = new List<SupplierListDto>();
            SupplierSearchDto req = new SupplierSearchDto
            {
                Classify = type,
                Limit = int.MaxValue,
                Page = 1,
                Status = 2
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Enterprise/GetSupplierPageList",
                JsonConvert.SerializeObject(req),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.Data != null)
            {
                var res = ret.Data.ToString().ToObject<GridDataResponse>();
                if (res != null)
                {
                    list = res.Data.ToString().ToList<SupplierListDto>();
                }
            }
            return list;
        }
    }
}