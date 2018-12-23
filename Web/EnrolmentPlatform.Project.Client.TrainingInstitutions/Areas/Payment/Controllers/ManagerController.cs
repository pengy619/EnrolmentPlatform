using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Payment.Controllers
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
        /// 缴费查看
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
        /// 查询学习管理列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Search(OrderListReqDto param)
        {
            int reCount = 0;
            param.FromChannelId = this.EnterpriseId;
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
            param.PaymentSource = 1;
            param.PaymentSourceId = this.EnterpriseId;
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
            param.FromChannelId = this.EnterpriseId;
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
            tempRow.Cells[1].SetCellValue(tempDataRow.BatchName);
            tempRow.Cells[2].SetCellValue(tempDataRow.SchoolName);
            tempRow.Cells[3].SetCellValue(tempDataRow.LevelName);
            tempRow.Cells[4].SetCellValue(tempDataRow.MajorName);
            tempRow.Cells[5].SetCellValue(tempDataRow.TotalAmount.ToString("N2"));
            tempRow.Cells[6].SetCellValue(tempDataRow.PayedAmount.ToString("N2"));
            tempRow.Cells[7].SetCellValue(tempDataRow.UnPayedAmount.ToString("N2"));
            tempRow.Cells[8].SetCellValue(tempDataRow.ApprovalAmount.ToString("N2"));
            tempRow.Cells[9].SetCellValue(tempDataRow.StatusName);
            tempRow.Cells[10].SetCellValue(tempDataRow.CreateTimeStr);
            tempRow.Cells[11].SetCellValue(tempDataRow.CreateUserName);

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
                row.Cells[1].SetCellValue(dto.BatchName);
                row.Cells[2].SetCellValue(dto.SchoolName);
                row.Cells[3].SetCellValue(dto.LevelName);
                row.Cells[4].SetCellValue(dto.MajorName);
                row.Cells[5].SetCellValue(dto.TotalAmount.ToString("N2"));
                row.Cells[6].SetCellValue(dto.PayedAmount.ToString("N2"));
                row.Cells[7].SetCellValue(dto.UnPayedAmount.ToString("N2"));
                row.Cells[8].SetCellValue(dto.ApprovalAmount.ToString("N2"));
                row.Cells[9].SetCellValue(dto.StatusName);
                row.Cells[10].SetCellValue(dto.CreateTimeStr);
                row.Cells[11].SetCellValue(dto.CreateUserName);
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
                UserName = this.UserName,
                CreateTime = DateTime.Now,
                PaymentSource = 1,
                PaymentSourceId = this.EnterpriseId,
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
    }
}