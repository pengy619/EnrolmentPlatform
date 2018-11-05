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
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Order.Controllers
{
    public class CateringController : BaseController
    {
        /// <summary>
        /// 餐饮订单
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
        public async Task<string> Search(SearchParamForCateringOrderDTO param)
        {
            param.SupplierId = base.SupplierId;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderForCatering/GetCateringSupplierOrderList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return msg.Data.ToString();
        }
        /// <summary>
        /// 餐饮订单详情
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Detail(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("orderId", id.ToString());
            parames.Add("supplierId", base.SupplierId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/OrderForCatering/SupplierGetCateringOrderById", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                return View(msg.Data.ToString().ToObject<OrderDetailForCateringDTO>());
            }
            else
            {
                throw new Exception(msg.Info);
            }
            
        }
        /// <summary>
        /// 核销管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Verification()
        {
            return View();
        }
        /// <summary>
        /// 核销异步搜索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> VerificationSearch(CateringOrderVerificationSearchParamDTO param)
        {
            param.SupplierId = base.SupplierId;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForCatering/SupplierGetCateringVerificationList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
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
            dto.UpdateUserName = base.UserAccount;
            dto.VerificationMode = (int)VerificationModeEnum.SupplierBackstage;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForCatering/SupplierSetVerificat", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
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
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForCatering/SupplierSetInvalid", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }

        #region 订单操作
        /// <summary>
        /// 供应商修改价格
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierUpdatePrice(UpdateCateringOrderPriceDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderForCatering/SupplierUpdatePrice", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        /// <summary>
        /// 供应商申请退款
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierRefund(SupplierCateringOrderRefundDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderForCatering/SupplierRefund", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        /// <summary>
        /// 供应商处理C端用户的取消
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SupplierHandleRefund(SupplierCateringOrderHandleRefundDTO dto)
        {
            dto.SupplierId = base.SupplierId;
            dto.UpdateUserId = base.UserId;
            dto.UpdateUserName = base.UserAccount;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderForCatering/SupplierHandleRefund", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        #endregion

        #region OrderExcel
        public async Task<ActionResult> ExportToExcel(SearchParamForCateringOrderDTO param)
        {
            param.SupplierId = base.SupplierId;
            param.Page = 1;
            param.Limit = int.MaxValue;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderForCatering/GetCateringSupplierOrderList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                GridDataResponse gridDataResponse = JsonConvert.DeserializeObject<GridDataResponse>(msg.Data.ToString());
                if (gridDataResponse.Data == null)
                {
                    return Content("<script>alert('没有有效的数据！');history.go(-1);</script>");
                }
                List<OrderForCateringDTO> list = JsonConvert.DeserializeObject<List<OrderForCateringDTO>>(gridDataResponse.Data.ToString());
                if (list != null && !list.Any())
                {
                    return Content("<script>alert('没有有效的数据！');history.go(-1);</script>");
                }
                HSSFWorkbook hssfWorkbook = GetWorkbook(list);
                byte[] data = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    hssfWorkbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    data = ms.GetBuffer();
                }
                return File(data, "application/vnd.ms-excel", string.Format("餐饮订单列表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmsss")));

            }
            else
            {
                return Content("<script>alert('导出失败！');history.go(-1);</script>");
            }

        }
        private HSSFWorkbook GetWorkbook(List<OrderForCateringDTO> list)
        {
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("餐饮订单列表");
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
            string[] s_strTitle = { "订单编号", "日期", "产品信息", "数量", "金额", "状态" };
            //加载标题行
            for (int i = 0; i < s_strTitle.Length; i++)
            {
                ICell cell = s_row1.CreateCell(i);
                cell.SetCellValue(s_strTitle[i]);
                cell.CellStyle = style;
            }
            //设置列宽
            sheet.SetColumnWidth(0, 7000);
            sheet.SetColumnWidth(1, 5000);
            sheet.SetColumnWidth(1, 5000);
            sheet.SetColumnWidth(10, 7000);
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
                                cell.SetCellValue(item.CreatorTime.ToDateTimeString());

                                cell.CellStyle = cellstyle;
                                break;
                            case 2:
                                cell.SetCellValue(item.ProductName);
                                cell.CellStyle = cellstyle;
                                break;
                            case 3:
                                cell.SetCellValue(item.TotalQuantity.ToString());
                                cell.CellStyle = cellstyle;
                                break;
                            case 4:
                                cell.SetCellValue(item.UpdateTotalAmount != 0 ? item.UpdateTotalAmount.ToDouble() : item.TotalAmount.ToDouble());
                                cell.CellStyle = cellstyle;
                                break;
                            case 5:
                                cell.SetCellValue(item.OrderStatusCH);
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

        #region VerificationExcel
        public async Task<ActionResult> VerificationExportToExcel(CateringOrderVerificationSearchParamDTO param)
        {
            param.SupplierId = base.SupplierId;
            param.Page = 1;
            param.Limit = int.MaxValue;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/VerificationForCatering/SupplierGetCateringVerificationList", JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                GridDataResponse gridDataResponse = JsonConvert.DeserializeObject<GridDataResponse>(msg.Data.ToString());
                if (gridDataResponse.Data == null)
                {
                    return Content("<script>alert('没有有效的数据！');history.go(-1);</script>");
                }
                List<VerificationForCateringDTO> list = JsonConvert.DeserializeObject<List<VerificationForCateringDTO>>(gridDataResponse.Data.ToString());
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
                return File(data, "application/vnd.ms-excel", string.Format("餐饮订单核销列表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmsss")));

            }
            else
            {
                return Content("<script>alert('导出失败！');history.go(-1);</script>");
            }

        }
        private HSSFWorkbook GetVerificationWorkbook(List<VerificationForCateringDTO> list)
        {
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("餐饮订单核销列表");
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
            string[] s_strTitle = { "订单编号", "下单账户", "产品信息", "消费券号", "有效日期", "核销状态", "核销时间", "核销方式" };
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
                                cell.SetCellValue(item.ProductName);
                                cell.CellStyle = cellstyle;
                                break;
                            case 3:
                                cell.SetCellValue(item.CateringToken);
                                cell.CellStyle = cellstyle;
                                break;
                            case 4:
                                cell.SetCellValue(item.ExpiryDate);
                                cell.CellStyle = cellstyle;
                                break;
                            case 5:
                                cell.SetCellValue(item.StatusCH);
                                cell.CellStyle = cellstyle;
                                break;
                            case 6:
                                cell.SetCellValue(item.VerificationDate.ToDateTimeString());
                                cell.CellStyle = cellstyle;
                                break;
                            case 7:
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