using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Order.Controllers
{
    public class ManagerController : BaseController
    {
        /// <summary>
        /// 列表界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
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
            param.ToLearningCenterId = this.SupplierId;
            List<OrderListDto> list = OrderService.GetStudentList(param, ref reCount);
            if (list == null || list.Count == 0)
            {
                return Content("没有任何可以导出的数据！");
            }

            #region 导出

            HSSFWorkbook hssfworkbook = null;
            try
            {
                using (FileStream file = new FileStream(this.Server.MapPath("~/Temp/OrderTempNew.xls"), FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception ex)
            {
            }

            ISheet sheet = hssfworkbook.GetSheetAt(0);
            IRow firstRow = sheet.GetRow(0);

            //增加自定义列
            var customerFieldList = CustomerFieldService.GetFullList();
            int customerFieldStart = 24;
            if (customerFieldList != null && customerFieldList.Count > 0)
            {
                for (int i = 0; i < customerFieldList.Count; i++)
                {
                    var customerCell = firstRow.CreateCell(customerFieldStart + i);
                    customerCell.CellStyle = firstRow.Cells.First().CellStyle;
                    customerCell.SetCellValue(customerFieldList[i].Name);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                //创建列
                for (int j = 0; j < firstRow.LastCellNum; j++)
                {
                    ICell cell = row.CreateCell(j, CellType.String);
                    if (j >= customerFieldStart)
                    {
                        cell.SetCellValue("");
                    }
                }
                var dto = list[i];
                row.Cells[0].SetCellValue(dto.StudentName);
                row.Cells[1].SetCellValue(dto.IDCardNo);
                row.Cells[2].SetCellValue(dto.Phone);
                row.Cells[3].SetCellValue(dto.TencentNo);
                row.Cells[4].SetCellValue(dto.Email);
                row.Cells[5].SetCellValue(dto.BatchName);
                row.Cells[6].SetCellValue(dto.SchoolName);
                row.Cells[7].SetCellValue(dto.LevelName);
                row.Cells[8].SetCellValue(dto.MajorName);
                row.Cells[9].SetCellValue(dto.CreateTimeStr);
                row.Cells[10].SetCellValue(dto.JoinTimeStr);
                row.Cells[11].SetCellValue(dto.Sex);
                row.Cells[12].SetCellValue(dto.MinZu);
                row.Cells[13].SetCellValue(dto.JiGuan);
                row.Cells[14].SetCellValue(dto.HighesDegree);
                row.Cells[15].SetCellValue(dto.GraduateSchool);
                row.Cells[16].SetCellValue(dto.BiYeZhengBianHao);
                row.Cells[17].SetCellValue(dto.GraduationTimeStr);
                row.Cells[18].SetCellValue(dto.Address);
                row.Cells[19].SetCellValue(dto.GongZuoDanWei);
                row.Cells[20].SetCellValue(dto.StatusName);
                row.Cells[21].SetCellValue(dto.XueHao);
                row.Cells[22].SetCellValue(dto.UserName);
                row.Cells[23].SetCellValue(dto.Password);

                //自定义字段
                if (!string.IsNullOrWhiteSpace(dto.CustomerField))
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    var dic = javaScriptSerializer.Deserialize<Dictionary<string, string>>(dto.CustomerField);
                    if (customerFieldList != null && customerFieldList.Count > 0)
                    {
                        for (int j = 0; j < customerFieldList.Count; j++)
                        {
                            string title = firstRow.Cells[customerFieldStart + j].StringCellValue;
                            if (dic.Keys.Contains(title))
                            {
                                row.Cells[customerFieldStart + j].SetCellValue(dic[title]);
                            }
                        }
                    }
                }
            }

            //导出
            this.NPOIExport("报名单列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", hssfworkbook, new List<HSSFSheet> { (HSSFSheet)sheet });

            #endregion

            return null;
        }

        /// <summary>
        /// 操作界面
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns></returns>
        public ActionResult Option(Guid? orderId)
        {
            if (orderId.HasValue)
            {
                ViewBag.OrderInfo = OrderService.GetOrder(orderId.Value);
            }

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
            param.ToLearningCenterId = this.SupplierId;
            param.Field = "ToLearningCenterTime";
            List<OrderListDto> list = OrderService.GetStudentList(param, ref reCount);
            if (list == null)
            {
                list = new List<OrderListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 获得订单统计
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrderStatistics(OrderListReqDto param)
        {
            param.ToLearningCenterId = this.SupplierId;
            var data = OrderService.GetOrderStatistics(param);
            return Json(data);
        }

        /// <summary>
        /// 报名单拒绝
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="reason">拒绝理由</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Reject(Guid[] ids, string reason)
        {
            var ret = OrderService.Reject(ids.ToList(), this.UserId, reason);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "拒绝失败。" });
            }
        }

        /// <summary>
        /// 报名单退学
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Leave(Guid[] ids)
        {
            var ret = OrderService.Leave(ids.ToList(), this.UserId);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "退学失败。" });
            }
        }

        /// <summary>
        /// 报名单初审
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Audit(Guid[] ids)
        {
            var ret = OrderService.Audit(ids.ToList(), this.UserId);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "初审失败。" });
            }
        }

        ///// <summary>
        ///// 报名单录取
        ///// </summary>
        ///// <param name="ids">ID集合</param>
        ///// <returns>1：成功，2：错误</returns>
        //[HttpPost]
        //public JsonResult LuQu(Guid[] ids)
        //{
        //    var ret = OrderService.Luqu(ids.ToList(), this.UserId);
        //    if (ret == true)
        //    {
        //        return Json(new { ret = 1 });
        //    }
        //    else
        //    {
        //        return Json(new { ret = 0, msg = "录取失败。" });
        //    }
        //}

        /// <summary>
        /// 报名单录取
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="xuehao">xuehao</param>
        /// <param name="zhanghao">zhanghao</param>
        /// <param name="mima">mima</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LuQuConfirm(Guid orderId, string xuehao, string zhanghao, string mima)
        {
            var ret = OrderService.Luqu(orderId, xuehao, zhanghao, mima, this.UserId);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "匹配不到收费策略，录取失败。" });
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