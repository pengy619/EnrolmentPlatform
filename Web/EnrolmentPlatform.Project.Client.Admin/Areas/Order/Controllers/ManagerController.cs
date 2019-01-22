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
            ViewBag.TrainingList = GetUserList(SystemTypeEnum.TrainingInstitutions);
            //学习中心
            ViewBag.LearningList = GetUserList(SystemTypeEnum.LearningCenter);
            return View();
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
            tempRow.Cells[5].SetCellValue(tempDataRow.XueHao);
            tempRow.Cells[6].SetCellValue(tempDataRow.FromChannelName);
            tempRow.Cells[7].SetCellValue(tempDataRow.ToLearningCenterName);
            tempRow.Cells[8].SetCellValue(tempDataRow.StatusName);
            tempRow.Cells[9].SetCellValue(tempDataRow.CreateTimeStr);
            tempRow.Cells[10].SetCellValue(tempDataRow.CreateUserName);

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
                row.Cells[5].SetCellValue(dto.XueHao);
                row.Cells[6].SetCellValue(dto.FromChannelName);
                row.Cells[7].SetCellValue(dto.ToLearningCenterName);
                row.Cells[8].SetCellValue(dto.StatusName);
                row.Cells[9].SetCellValue(dto.CreateTimeStr);
                row.Cells[10].SetCellValue(dto.CreateUserName);
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
        /// 保存图片
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
            ISheet sheet = hssfworkbook.GetSheetAt(9);
            if (sheet.LastRowNum == 1)
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
                    var createDate = row.GetCell(9).ToString().Trim();
                    if (!string.IsNullOrEmpty(createDate))
                    {
                        dto.CreateDate = DateTime.Parse(createDate);
                    }
                    else
                    {
                        dto.CreateDate = DateTime.Now;
                    }

                    var luquDate = row.GetCell(10).ToString().Trim();
                    if (!string.IsNullOrEmpty(luquDate))
                    {
                        dto.LuquDate = DateTime.Parse(luquDate);
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
                    dto.Remark = row.GetCell(20).ToString().Trim();
                    dto.FromChannelName = row.GetCell(21).ToString().Trim();
                    dto.ToLearningCenterName = row.GetCell(22).ToString().Trim();
                    dto.StudentNo = row.GetCell(23).ToString().Trim();
                    dto.UserName = row.GetCell(24).ToString().Trim();
                    dto.Password = row.GetCell(25).ToString().Trim();
                    dto.JiGouAmount = Convert.ToDecimal(row.GetCell(26).NumericCellValue);
                    dto.JiGouPayedAmount = Convert.ToDecimal(row.GetCell(27).NumericCellValue);
                    dto.ZhongXinAmount = Convert.ToDecimal(row.GetCell(28).NumericCellValue);
                    dto.ZhongXinPayedAmount = Convert.ToDecimal(row.GetCell(29).NumericCellValue);

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
    }
}