using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            tempRow.Cells[1].SetCellValue(tempDataRow.IDCardNo);
            tempRow.Cells[2].SetCellValue(tempDataRow.BatchName);
            tempRow.Cells[3].SetCellValue(tempDataRow.SchoolName);
            tempRow.Cells[4].SetCellValue(tempDataRow.LevelName);
            tempRow.Cells[5].SetCellValue(tempDataRow.MajorName);
            tempRow.Cells[6].SetCellValue(tempDataRow.Sex);
            tempRow.Cells[7].SetCellValue(tempDataRow.MinZu);
            tempRow.Cells[8].SetCellValue(tempDataRow.JiGuan);
            tempRow.Cells[9].SetCellValue(tempDataRow.HighesDegree);
            tempRow.Cells[10].SetCellValue(tempDataRow.GraduateSchool);
            tempRow.Cells[11].SetCellValue(tempDataRow.BiYeZhengBianHao);
            tempRow.Cells[12].SetCellValue(tempDataRow.Address);
            tempRow.Cells[13].SetCellValue(tempDataRow.GongZuoDanWei);
            tempRow.Cells[14].SetCellValue(tempDataRow.StatusName);
            tempRow.Cells[15].SetCellValue(tempDataRow.CreateTimeStr);
            //tempRow.Cells[7].SetCellValue(tempDataRow.CreateUserName);

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
                row.Cells[1].SetCellValue(dto.IDCardNo);
                row.Cells[2].SetCellValue(dto.BatchName);
                row.Cells[3].SetCellValue(dto.SchoolName);
                row.Cells[4].SetCellValue(dto.LevelName);
                row.Cells[5].SetCellValue(dto.MajorName);
                row.Cells[6].SetCellValue(dto.Sex);
                row.Cells[7].SetCellValue(dto.MinZu);
                row.Cells[8].SetCellValue(dto.JiGuan);
                row.Cells[9].SetCellValue(dto.HighesDegree);
                row.Cells[10].SetCellValue(dto.GraduateSchool);
                row.Cells[11].SetCellValue(dto.BiYeZhengBianHao);
                row.Cells[12].SetCellValue(dto.Address);
                row.Cells[13].SetCellValue(dto.GongZuoDanWei);
                row.Cells[14].SetCellValue(dto.StatusName);
                row.Cells[15].SetCellValue(dto.CreateTimeStr);
                //row.Cells[7].SetCellValue(dto.CreateUserName);
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
            param.ToLearningCenterId = this.SupplierId;
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
                return Json(new { ret = 0, msg = "录取失败。" });
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