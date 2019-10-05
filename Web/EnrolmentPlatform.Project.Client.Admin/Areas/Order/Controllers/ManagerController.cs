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
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Order.Controllers
{
    public class ManagerController : BaseController
    {
        /// <summary>
        /// 列表界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //招生机构
            ViewBag.TrainingList = EnterpriseService.GetUserList(SystemTypeEnum.TrainingInstitutions);
            //学习中心
            ViewBag.LearningList = EnterpriseService.GetUserList(SystemTypeEnum.LearningCenter);
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
            param.IsChannel = true;
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
            for (int i = 0; i < list.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                //创建列
                for (int j = 0; j < firstRow.LastCellNum; j++)
                {
                    ICell cell = row.CreateCell(j, CellType.String);
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
                row.Cells[17].SetCellValue(dto.Address);
                row.Cells[18].SetCellValue(dto.GongZuoDanWei);
                row.Cells[19].SetCellValue(dto.FromChannelName);
                row.Cells[20].SetCellValue(dto.ToLearningCenterName);
                row.Cells[21].SetCellValue(dto.StatusName);
                row.Cells[22].SetCellValue(dto.CreateUserName);
                row.Cells[23].SetCellValue(dto.XueHao);
                row.Cells[24].SetCellValue(dto.UserName);
                row.Cells[25].SetCellValue(dto.Password);
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
            else
            {
                return RedirectToAction("Index", "Manager");
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
            param.IsChannel = true;
            param.Field = "EnrollTime";
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
        /// 报名单报送
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="learningCenterId">学院中心ID</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult ToLearningCenter(Guid[] ids, Guid learningCenterId)
        {
            var ret = OrderService.ToLearningCenter(ids.ToList(), learningCenterId, this.UserId);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "报送学院中心失败。" });
            }
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
        /// 报名单毕业
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Graduated(Guid[] ids)
        {
            var ret = OrderService.Graduated(ids.ToList(), this.UserId);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "毕业操作失败。" });
            }
        }

        /// <summary>
        /// 修改渠道
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult UpdateTrainingInstitutions(Guid[] ids, Guid trainingInstitutionsId)
        {
            var ret = OrderService.UpdateTrainingInstitutions(ids, trainingInstitutionsId, this.UserId);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "渠道修改失败。" });
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
        /// 保存订单
        /// </summary>
        /// <param name="order">order</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveOrder(OrderDto order)
        {
            //当前订单信息
            order.FromTypeName = "渠道";
            order.FromChannelId = null;
            order.UserName = this.UserUser;
            order.UserId = this.UserId;

            var ret = 1;
            if (order.OrderId.HasValue == false)
            {
                ret = OrderService.AddOrder(order);
            }
            else
            {
                ret = OrderService.UpdateOrder(order);
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

        /// <summary>
        /// 导入报名单
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public JsonResult Upload(HttpPostedFileBase file)
        {
            //没有文件
            if (string.IsNullOrWhiteSpace(file.FileName))
            {
               return Json(new { ret = false, msg = "请上传文件！" });
            }

            string strFileExtension = Path.GetExtension(file.FileName).ToLower();

            //验证是否是.xls文件
            if (strFileExtension != ".xls" && strFileExtension != ".xlsx")
            {
                return Json(new { ret = false, msg = "文件格式错误！" });
            }

            //上传的excel文件
            IWorkbook hssfworkbook = WorkbookFactory.Create(file.InputStream);
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            if (sheet.LastRowNum < 1)
            {
                return Json(new { ret = false, msg = "没有任何数据！" });
            }

            //订单上传数据
            List<OrderUploadDto> list = new List<OrderUploadDto>();
            for (var i = 1; i <= sheet.LastRowNum; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
                    OrderUploadDto dto = new OrderUploadDto();
                    dto.StudentName = row.GetCell(0).ToString().Trim();
                    dto.IDCardNo = row.GetCell(1).ToString().Trim();
                    dto.Phone = row.GetCell(2).ToString().Trim();
                    dto.TencentNo = row.GetCell(3).ToString().Trim();
                    dto.Email = row.GetCell(4).ToString().Trim();
                    dto.BatchName = row.GetCell(5).ToString().Trim();
                    dto.SchoolName = row.GetCell(6).ToString().Trim();
                    dto.LevelName = row.GetCell(7).ToString().Trim();
                    dto.MajorName = row.GetCell(8).ToString().Trim();
                    if (row.GetCell(9) != null && !string.IsNullOrEmpty(row.GetCell(9).ToString()))
                    {
                        dto.CreateDate = row.GetCell(9).DateCellValue;
                    }
                    else
                    {
                        dto.CreateDate = DateTime.Now;
                    }
                    if (row.GetCell(10) != null && !string.IsNullOrEmpty(row.GetCell(10).ToString()))
                    {
                        dto.LuquDate = row.GetCell(10).DateCellValue;
                    }
                    else
                    {
                        dto.LuquDate = DateTime.Now;
                    }
                    dto.CreateUserName = row.GetCell(11).ToString().Trim();
                    dto.Sex = row.GetCell(12).ToString().Trim();
                    dto.MinZu = row.GetCell(13).ToString().Trim();
                    dto.JiGuan = row.GetCell(14).ToString().Trim();
                    dto.HighesDegree = row.GetCell(15).ToString().Trim();
                    dto.GraduateSchool = row.GetCell(16).ToString().Trim();
                    dto.BiYeZhengBianHao = row.GetCell(17).ToString().Trim();
                    dto.Address = row.GetCell(18).ToString().Trim();
                    dto.GongZuoDanWei = row.GetCell(19).ToString().Trim();
                    if (row.GetCell(20) != null)
                    {
                        dto.Remark = row.GetCell(20).ToString().Trim();
                    }
                    dto.FromChannelName = row.GetCell(21).ToString().Trim();
                    dto.ToLearningCenterName = row.GetCell(22).ToString().Trim();
                    dto.StudentNo = row.GetCell(23).ToString().Trim();
                    dto.UserName = row.GetCell(24).ToString().Trim();
                    dto.Password = row.GetCell(25).ToString().Trim();
                    dto.JiGouAmount = Convert.ToDecimal(row.GetCell(26).NumericCellValue);
                    dto.JiGouPayedAmount = Convert.ToDecimal(row.GetCell(27).NumericCellValue);
                    dto.ZhongXinAmount = Convert.ToDecimal(row.GetCell(28).NumericCellValue);
                    dto.ZhongXinPayedAmount = Convert.ToDecimal(row.GetCell(29).NumericCellValue);

                    if (string.IsNullOrWhiteSpace(dto.StudentName) && string.IsNullOrWhiteSpace(dto.IDCardNo)
                        && string.IsNullOrWhiteSpace(dto.Phone) && string.IsNullOrWhiteSpace(dto.TencentNo)
                        && string.IsNullOrWhiteSpace(dto.Email) && string.IsNullOrWhiteSpace(dto.BatchName)
                        && string.IsNullOrWhiteSpace(dto.SchoolName) && string.IsNullOrWhiteSpace(dto.LevelName)
                        && string.IsNullOrWhiteSpace(dto.MajorName) && string.IsNullOrWhiteSpace(dto.CreateUserName)
                        && string.IsNullOrWhiteSpace(dto.Sex) && string.IsNullOrWhiteSpace(dto.MinZu)
                        && string.IsNullOrWhiteSpace(dto.JiGuan) && string.IsNullOrWhiteSpace(dto.HighesDegree)
                        && string.IsNullOrWhiteSpace(dto.GraduateSchool) && string.IsNullOrWhiteSpace(dto.BiYeZhengBianHao)
                        && string.IsNullOrWhiteSpace(dto.Address) && string.IsNullOrWhiteSpace(dto.GongZuoDanWei)
                        && string.IsNullOrWhiteSpace(dto.FromChannelName) && string.IsNullOrWhiteSpace(dto.ToLearningCenterName)
                        && string.IsNullOrWhiteSpace(dto.StudentNo) && string.IsNullOrWhiteSpace(dto.UserName)
                        && string.IsNullOrWhiteSpace(dto.Password))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(dto.StudentName) || string.IsNullOrWhiteSpace(dto.IDCardNo)
                        || string.IsNullOrWhiteSpace(dto.Phone) || string.IsNullOrWhiteSpace(dto.TencentNo)
                        || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.BatchName)
                        || string.IsNullOrWhiteSpace(dto.SchoolName) || string.IsNullOrWhiteSpace(dto.LevelName)
                        || string.IsNullOrWhiteSpace(dto.MajorName) || string.IsNullOrWhiteSpace(dto.CreateUserName)
                        || string.IsNullOrWhiteSpace(dto.Sex) || string.IsNullOrWhiteSpace(dto.MinZu)
                        || string.IsNullOrWhiteSpace(dto.JiGuan) || string.IsNullOrWhiteSpace(dto.HighesDegree)
                        || string.IsNullOrWhiteSpace(dto.GraduateSchool) || string.IsNullOrWhiteSpace(dto.BiYeZhengBianHao)
                        || string.IsNullOrWhiteSpace(dto.Address) || string.IsNullOrWhiteSpace(dto.GongZuoDanWei)
                        || string.IsNullOrWhiteSpace(dto.FromChannelName) || string.IsNullOrWhiteSpace(dto.ToLearningCenterName)
                        || string.IsNullOrWhiteSpace(dto.StudentNo) || string.IsNullOrWhiteSpace(dto.UserName)
                        || string.IsNullOrWhiteSpace(dto.Password))
                    {
                        return Json(new { ret = false, msg = "第" + (i + 1).ToString() + "行的数据填写不完整！" });
                    }

                    if (dto.JiGouAmount < dto.JiGouPayedAmount || dto.ZhongXinAmount < dto.ZhongXinPayedAmount)
                    {
                        return Json(new { ret = false, msg = "第" + (i + 1).ToString() + "行的金额数据错误！" });
                    }

                    list.Add(dto);
                }
                catch (Exception ex)
                {
                    return Json(new { ret = false, msg = "第" + (i + 1).ToString() + "行的数据错误！" });
                }
            }

            string msg = OrderService.Upload(list);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Json(new { ret = false, msg = msg });
            }

            return Json(new { ret = true });
        }

        /// <summary>
        /// 录取导入
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public JsonResult LuquUpload(HttpPostedFileBase file)
        {
            //没有文件
            if (string.IsNullOrWhiteSpace(file.FileName))
            {
                return Json(new { ret = false, msg = "请上传文件！" });
            }

            string strFileExtension = Path.GetExtension(file.FileName).ToLower();

            //验证是否是.xls文件
            if (strFileExtension != ".xls" && strFileExtension != ".xlsx")
            {
                return Json(new { ret = false, msg = "文件格式错误！" });
            }

            //上传的excel文件
            IWorkbook hssfworkbook = WorkbookFactory.Create(file.InputStream);
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            if (sheet.LastRowNum < 1)
            {
                return Json(new { ret = false, msg = "没有任何数据！" });
            }

            //订单录取数据
            List<OrderLuQuUploadDto> list = new List<OrderLuQuUploadDto>();
            for (var i = 1; i <= sheet.LastRowNum; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
                    OrderLuQuUploadDto dto = new OrderLuQuUploadDto();
                    dto.StudentName = row.GetCell(0).ToString().Trim();
                    dto.IDCardNo = row.GetCell(1).ToString().Trim();
                    dto.BatchName = row.GetCell(2).ToString().Trim();
                    dto.SchoolName = row.GetCell(3).ToString().Trim();
                    dto.LevelName = row.GetCell(4).ToString().Trim();
                    dto.MajorName = row.GetCell(5).ToString().Trim();
                    dto.StudentNo = row.GetCell(6).ToString().Trim();
                    dto.UserName = row.GetCell(7).ToString().Trim();
                    dto.Password = row.GetCell(8).ToString().Trim();

                    if (row.GetCell(9) != null && !string.IsNullOrEmpty(row.GetCell(9).ToString()))
                    {
                        dto.LuquDate = Convert.ToDateTime(row.GetCell(9).ToString());
                    }
                    else
                    {
                        dto.LuquDate = DateTime.Now;
                    }

                    if (string.IsNullOrWhiteSpace(dto.StudentName) && string.IsNullOrWhiteSpace(dto.IDCardNo)
                        && string.IsNullOrWhiteSpace(dto.BatchName) && string.IsNullOrWhiteSpace(dto.SchoolName)
                        && string.IsNullOrWhiteSpace(dto.LevelName) && string.IsNullOrWhiteSpace(dto.MajorName)
                        && string.IsNullOrWhiteSpace(dto.StudentNo) && string.IsNullOrWhiteSpace(dto.UserName)
                        && string.IsNullOrWhiteSpace(dto.Password))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(dto.StudentName) || string.IsNullOrWhiteSpace(dto.IDCardNo)
                    || string.IsNullOrWhiteSpace(dto.BatchName) || string.IsNullOrWhiteSpace(dto.SchoolName)
                    || string.IsNullOrWhiteSpace(dto.LevelName) || string.IsNullOrWhiteSpace(dto.MajorName)
                    || string.IsNullOrWhiteSpace(dto.StudentNo) || string.IsNullOrWhiteSpace(dto.UserName)
                    || string.IsNullOrWhiteSpace(dto.Password))
                    {
                        return Json(new { ret = false, msg = "第" + (i + 1).ToString() + "行的数据填写不完整！" });
                    }

                    list.Add(dto);
                }
                catch (Exception ex)
                {
                    return Json(new { ret = false, msg = "第" + (i + 1).ToString() + "行的数据错误！" });
                }
            }

            string msg = OrderService.LuQuUpload(list);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Json(new { ret = false, msg = msg });
            }

            return Json(new { ret = true });
        }
    }
}