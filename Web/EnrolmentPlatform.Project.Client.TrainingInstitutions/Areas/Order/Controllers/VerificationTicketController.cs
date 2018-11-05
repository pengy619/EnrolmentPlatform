using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Order.Controllers
{
    public class VerificationTicketController : BaseController
    {
        /// <summary>
        /// 票务核销
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string ticketToken = null)
        {
            ViewBag.TicketToken = ticketToken;
            return View();
        }
        /// <summary>
        /// 门票核销异步搜索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> Search(TicketOrderVerificationSearchParamDTO param)
        {
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForTicket/TouristCenterGetTicketVerificationList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return msg.Data.ToString();
        }
        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<JsonResult> SupplierSetVerificat(SupplierVerificationDTO dto)
        {
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserName;
            dto.VerificationMode = (int)VerificationModeEnum.TouristServiceCenter;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForTicket/SupplierSetVerificat", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierSetInvalid(SupplierInvalidDTO dto)
        {
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserName;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForTicket/SupplierSetInvalid", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }

        #region VerificationExcel
        public async Task<ActionResult> VerificationExportToExcel(TicketOrderVerificationSearchParamDTO param)
        {
            param.Page = 1;
            param.Limit = int.MaxValue;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForTicket/TouristCenterGetTicketVerificationList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                GridDataResponse gridDataResponse = JsonConvert.DeserializeObject<GridDataResponse>(msg.Data.ToString());
                if (gridDataResponse.Data == null)
                {
                    return Content("<script>alert('没有有效的数据！');history.go(-1);</script>");
                }
                List<TicketOrderVerificationInfo> list = JsonConvert.DeserializeObject<List<TicketOrderVerificationInfo>>(gridDataResponse.Data.ToString());
                if (list != null && !list.Any())
                {
                    return Content("<script>alert('没有有效的数据！');history.go(-1);</script>");
                }
                HSSFWorkbook hssfWorkbook = GetVerificationWorkbook(list);
                byte[] data = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    hssfWorkbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    data = ms.GetBuffer();
                }
                return File(data, "application/vnd.ms-excel", string.Format("门票订单核销列表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmsss")));

            }
            else
            {
                return Content("<script>alert('导出失败！');history.go(-1);</script>");
            }

        }
        private HSSFWorkbook GetVerificationWorkbook(List<TicketOrderVerificationInfo> list)
        {
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("门票订单核销列表");
            //初始化样式---标题公共样式
            ICellStyle style = hssfWorkbook.CreateCellStyle();
            //设置填充颜色
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.CornflowerBlue.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //垂直居中样式
            ICellStyle cellstyle = hssfWorkbook.CreateCellStyle();//设置垂直居中格式
            cellstyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            //开始设置表头
            IRow rowHeader = sheet.CreateRow(0);
            IRow s_row1 = sheet.CreateRow(0);
            //新建数组保存标题
            string[] s_strTitle = { "订单编号", "下单账户", "产品编号", "产品名称", "供应商类型", "供应商", "取票码", "票号", "有效日期", "核销状态", "核销时间", "核销方式" };
            //加载标题行
            for (int i = 0; i < s_strTitle.Length; i++)
            {
                ICell cell = s_row1.CreateCell(i);
                cell.SetCellValue(s_strTitle[i]);
                cell.CellStyle = style;
            }
            //设置列宽
            sheet.SetColumnWidth(0, 3000);
            sheet.SetColumnWidth(3, 5000);
            sheet.SetColumnWidth(5, 5000);
            sheet.SetColumnWidth(9, 5000);
            if (list != null && list.Any())
            {
                int s_rowindex = 1;
                //开始加载数据
                foreach (var item in list)
                {
                    IRow row = sheet.CreateRow(s_rowindex);
                    for (int j = 0; j < s_strTitle.Length; j++)
                    {
                        ICell cell = row.CreateCell(j);

                        switch (j)
                        {
                            case 0:
                                cell.SetCellValue(item.OrderNo);
                                cell.CellStyle = cellstyle;
                                break;
                            case 1:
                                cell.SetCellValue(item.CreatorAccount);
                                cell.CellStyle = cellstyle;
                                break;
                            case 2:
                                cell.SetCellValue(item.ProductNo);
                                cell.CellStyle = cellstyle;
                                break;
                            case 3:
                                cell.SetCellValue(item.ProductName);
                                cell.CellStyle = cellstyle;
                                break;
                            case 4:
                                cell.SetCellValue(item.SupplierTypeCH);
                                cell.CellStyle = cellstyle;
                                break;
                            case 5:
                                cell.SetCellValue(item.SupplierName);
                                cell.CellStyle = cellstyle;
                                break;
                            case 6:
                                cell.SetCellValue(item.UnifiedCheckCode);
                                cell.CellStyle = cellstyle;
                                break;
                            case 7:
                                cell.SetCellValue(item.TicketToken);
                                cell.CellStyle = cellstyle;
                                break;
                            case 8:
                                cell.SetCellValue(item.ExpiryDate.ToDateString());
                                cell.CellStyle = cellstyle;
                                break;
                            case 9:
                                cell.SetCellValue(item.StatusCH);
                                cell.CellStyle = cellstyle;
                                break;
                            case 10:
                                cell.SetCellValue(item.VerificationDate.ToDateTimeString());
                                cell.CellStyle = cellstyle;
                                break;
                            case 11:
                                cell.SetCellValue(item.PatternCH);
                                cell.CellStyle = cellstyle;
                                break;
                            default:
                                break;
                        };
                    }
                    s_rowindex++;
                }
            }
            return hssfWorkbook;
        }
        #endregion
    }
}