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

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Order.Controllers
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
            param.FromChannelId = this.EnterpriseId;
            param.Limit = int.MaxValue;
            List<OrderListDto> list = OrderService.GetStudentList(param, ref reCount);
            if (list == null || list.Count == 0)
            {
                return Content("没有任何可以导出的数据！");
            }

            #region 导出

            HSSFWorkbook hssfworkbook = null;
            try
            {
                using (FileStream file = new FileStream(this.Server.MapPath("~/Temp/OrderTemp.xls"), FileMode.Open, FileAccess.Read))
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
            tempRow.Cells[5].SetCellValue(tempDataRow.StatusName);
            tempRow.Cells[6].SetCellValue(tempDataRow.CreateTimeStr);
            tempRow.Cells[7].SetCellValue(tempDataRow.CreateUserName);

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
                row.Cells[5].SetCellValue(dto.StatusName);
                row.Cells[6].SetCellValue(dto.CreateTimeStr);
                row.Cells[7].SetCellValue(dto.CreateUserName);
            }

            //导出
            this.NPOIExport("报名单列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", hssfworkbook, new List<HSSFSheet> { sheet });

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

            //批次
            ViewBag.BatchList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Batch);
            //学校
            ViewBag.SchoolList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.School);

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
        /// 删除报名单
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Delete(Guid[] ids)
        {
            var ret = OrderService.Delete(ids);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "删除失败。" });
            }
        }

        /// <summary>
        /// 报名单报名
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult SubmitOrder(Guid[] ids)
        {
            var ret = OrderService.SubmitOrder(ids.ToList(),this.UserId);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "报名失败。" });
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

        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="order">order</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveOrder(OrderDto order)
        {
            //当前订单信息
            order.FromTypeName = "机构";
            order.FromChannelId = this.EnterpriseId;
            //如果有填写招生老师
            if (!string.IsNullOrWhiteSpace(order.UserName))
            {
                //给定输入的招生老师
                order.UserName = this.UserName;
            }
            order.UserId = this.UserId;

            var ret = 1;
            if (order.OrderId.HasValue == false)
            {
                ret = OrderService.AddOrder(order);
            }
            else
            {
                ret= OrderService.UpdateOrder(order);
            }

            //1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入
            if (ret == 2)
            {
                return Json(new { ret = false, msg = "找不到当前时间段的价格策略。" });
            }
            else if (ret == 3)
            {
                return Json(new { ret = false, msg = "添加失败。" });
            }
            else if (ret == 4)
            {
                return Json(new { ret = false, msg = "同一批次重复录入。" });
            }
            return Json(new { ret = true });
        }
    }
}