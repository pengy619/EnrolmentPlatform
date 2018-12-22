using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
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
        public ActionResult UserPaymentRecord()
        {
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
            List<Guid> orderList = new List<Guid>();
            foreach (var item in orderIdArr)
            {
                orderList.Add(Guid.Parse(item));
            }
            ViewBag.PersonCount = orderList.Count;
            ViewBag.OrderList = orderList.ToJson();
            return View();
        }

        /// <summary>
        /// 缴费查看
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentDetail()
        {
            return View();
        }

        /// <summary>
        /// 查询列表
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
    }
}