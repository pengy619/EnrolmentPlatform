using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
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
        /// 查看界面
        /// </summary>
        /// <returns></returns>
        public ActionResult View(Guid orderId)
        {
            //报名单信息
            var order = OrderService.GetOrder(orderId);
            ViewBag.OrderInfo = order;
            //照片信息
            ViewBag.ImageDto = OrderService.FindOrderImage(orderId);
            //自定义字段
            ViewBag.CustomFields = CustomerFieldService.GetAllList(new DTO.Basics.GetAllListSearchDto() { SchoolId = order.SchoolId });
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
            if (this.IsMaster == false)
            {
                param.UserId = this.UserId;
            }
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
            var schoolIds = list.Select(t => t.SchoolId).Distinct().ToList();
            var customerFieldList = CustomerFieldService.GetFullList(schoolIds);
            int customerFieldStart = 25;
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
                row.Cells[15].SetCellValue(dto.SuoDuZhuanYe);
                row.Cells[16].SetCellValue(dto.GraduateSchool);
                row.Cells[17].SetCellValue(dto.BiYeZhengBianHao);
                row.Cells[18].SetCellValue(dto.Address);
                row.Cells[19].SetCellValue(dto.GongZuoDanWei);
                row.Cells[20].SetCellValue(dto.StatusName);
                row.Cells[21].SetCellValue(dto.CreateUserName);
                row.Cells[22].SetCellValue(dto.XueHao);
                row.Cells[23].SetCellValue(dto.UserName);
                row.Cells[24].SetCellValue(dto.Password);

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
            bool updateApply = false;
            if (orderId.HasValue)
            {
                var orderInfo = OrderService.GetOrder(orderId.Value);
                updateApply = (orderInfo != null && Request.QueryString["action"] == "1" && orderInfo.Status != (int)OrderStatusEnum.Init && orderInfo.Status != (int)OrderStatusEnum.Reject);
                ViewBag.OrderInfo = orderInfo;

                //如果是修改审核
                if (updateApply == true)
                {
                    //查看是否有在审核的订单修改审核
                    var orderApproval = OrderApprovalService.GetOrderApplyApprovalInfoByOrderId(orderId.Value);
                    if (orderApproval != null)
                    {
                        ViewBag.ApprovalId = orderApproval.ApprovalId.Value.ToString();
                        //if (orderApproval.ApprovalStatus.Value == 0)
                        //{
                            return Redirect("/Order/OrderUpdateApproval/OrderInfo?action=update&approvalId=" + orderApproval.ApprovalId.Value.ToString());
                        //}
                        //else
                        //{
                        //    return Redirect("/Order/OrderUpdateApproval/OrderInfo?action=view&approvalId=" + orderApproval.ApprovalId.Value.ToString());
                        //}
                    }
                    else
                    {
                        return Redirect("/Order/OrderUpdateApproval/OrderInfo?action=update&orderId=" + orderInfo.OrderId);
                    }
                }
            }

            ViewBag.UpdateApply = updateApply;

            //批次
            ViewBag.BatchList = MetadataService.GetEnableList(DTO.Enums.Basics.MetadataTypeEnum.Batch);
            //学校
            ViewBag.SchoolList = MetadataService.GetSchoolListByTags(null, this.EnterpriseId);

            return View();
        }

        /// <summary>
        /// 根据学习形式获取学校列表
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public JsonResult GetSchoolListByTags(string tags)
        {
            var data = MetadataService.GetSchoolListByTags(tags, this.EnterpriseId);
            return Json(data, JsonRequestBehavior.AllowGet);
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
            if (this.IsMaster == false)
            {
                param.UserId = this.UserId;
            }
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
            param.FromChannelId = this.EnterpriseId;
            if (this.IsMaster == false)
            {
                param.UserId = this.UserId;
            }
            var data = OrderService.GetOrderStatistics(param);
            return Json(data);
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
        /// <param name="dto">dto</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult SubmitOrder(SubmitOrderDto dto)
        {
            dto.UserId = this.UserId;
            var ret = OrderService.SubmitOrder(dto);
            if (ret.IsSuccess)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = ret.Info });
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
            //如果没有填写招生老师（主账号取机构名,子账号取用户名）
            if (string.IsNullOrWhiteSpace(order.CreateUserName))
            {
                order.CreateUserName = this.IsMaster ? this.EnterpriseName : this.UserName;
            }
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
                    if (!row.GetCell(9).IsEmpty())
                    {
                        dto.CreateDate = row.GetCell(9).ToDate();
                    }
                    dto.Sex = row.GetCell(10).ToString().Trim();
                    dto.MinZu = row.GetCell(11).ToString().Trim();
                    dto.JiGuan = row.GetCell(12).ToString().Trim();
                    dto.HighesDegree = row.GetCell(13).ToString().Trim();
                    dto.SuoDuZhuanYe = row.GetCell(14).ToString().Trim();
                    dto.GraduateSchool = row.GetCell(15).ToString().Trim();
                    dto.BiYeZhengBianHao = row.GetCell(16).ToString().Trim();
                    dto.Address = row.GetCell(17).ToString().Trim();
                    dto.GongZuoDanWei = row.GetCell(18).ToString().Trim();
                    dto.IsTvUniversity = row.GetCell(19).ToString().Trim();
                    if (!row.GetCell(20).IsEmpty())
                    {
                        dto.GraduationTime = row.GetCell(20).ToDate();
                    }
                    dto.CreateUserName = row.GetCell(21).ToString().Trim();
                    if (row.GetCell(22) != null)
                    {
                        dto.Remark = row.GetCell(22).ToString().Trim();
                    }

                    if (string.IsNullOrWhiteSpace(dto.StudentName) && string.IsNullOrWhiteSpace(dto.IDCardNo)
                        && string.IsNullOrWhiteSpace(dto.Phone) && string.IsNullOrWhiteSpace(dto.TencentNo)
                        && string.IsNullOrWhiteSpace(dto.Email) && string.IsNullOrWhiteSpace(dto.BatchName)
                        && string.IsNullOrWhiteSpace(dto.SchoolName) && string.IsNullOrWhiteSpace(dto.LevelName)
                        && string.IsNullOrWhiteSpace(dto.MajorName)
                        && string.IsNullOrWhiteSpace(dto.Sex) && string.IsNullOrWhiteSpace(dto.MinZu)
                        && string.IsNullOrWhiteSpace(dto.JiGuan) && string.IsNullOrWhiteSpace(dto.HighesDegree)
                        && string.IsNullOrWhiteSpace(dto.GraduateSchool) && string.IsNullOrWhiteSpace(dto.BiYeZhengBianHao)
                        && string.IsNullOrWhiteSpace(dto.Address) && string.IsNullOrWhiteSpace(dto.GongZuoDanWei))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(dto.StudentName) || string.IsNullOrWhiteSpace(dto.IDCardNo)
                        || string.IsNullOrWhiteSpace(dto.Phone) || string.IsNullOrWhiteSpace(dto.TencentNo)
                        || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.BatchName)
                        || string.IsNullOrWhiteSpace(dto.SchoolName) || string.IsNullOrWhiteSpace(dto.LevelName)
                        || string.IsNullOrWhiteSpace(dto.MajorName)
                        || string.IsNullOrWhiteSpace(dto.Sex) || string.IsNullOrWhiteSpace(dto.MinZu)
                        || string.IsNullOrWhiteSpace(dto.JiGuan) || string.IsNullOrWhiteSpace(dto.HighesDegree)
                        || string.IsNullOrWhiteSpace(dto.GraduateSchool) || string.IsNullOrWhiteSpace(dto.BiYeZhengBianHao)
                        || string.IsNullOrWhiteSpace(dto.Address) || string.IsNullOrWhiteSpace(dto.GongZuoDanWei))
                    {
                        return Json(new { ret = false, msg = "第" + (i + 1).ToString() + "行的数据填写不完整！" });
                    }

                    //如果没有填写招生老师（主账号取机构名,子账号取用户名）
                    if (string.IsNullOrWhiteSpace(dto.CreateUserName))
                    {
                        dto.CreateUserName = this.IsMaster ? this.EnterpriseName : this.UserName;
                    }

                    list.Add(dto);
                }
                catch (Exception ex)
                {
                    return Json(new { ret = false, msg = "第" + (i + 1).ToString() + "行的数据错误！" });
                }
            }

            string msg = OrderService.JiGouUpload(new JiGouOrderUploadDto
            {
                OrderUploadList = list,
                FromChannelId = this.EnterpriseId,
                CreatorUserId = this.UserId
            });
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Json(new { ret = false, msg = msg });
            }

            return Json(new { ret = true });
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

        #region 学员账号

        /// <summary>
        /// 学员账号
        /// </summary>
        /// <returns></returns>
        public ActionResult Account()
        {
            return View();
        }

        /// <summary>
        /// 学员账号列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string AccountList(AccountListReqDto param)
        {
            int reCount = 0;
            param.FromChannelId = this.EnterpriseId;
            if (this.IsMaster == false)
            {
                param.UserId = this.UserId;
            }
            List<AccountListDto> list = OrderService.GetAccountList(param, ref reCount);
            if (list == null)
            {
                list = new List<AccountListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 修改学员账号
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAccount(UpdateAccountDto dto)
        {
            dto.UserId = this.UserId;
            var ret = OrderService.UpdateAccount(dto);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "修改失败。" });
            }
        }

        /// <summary>
        /// 导出账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExportAccount(AccountListReqDto param)
        {
            int reCount = 0;
            param.Page = 1;
            param.Limit = int.MaxValue;
            param.FromChannelId = this.EnterpriseId;
            if (this.IsMaster == false)
            {
                param.UserId = this.UserId;
            }
            List<AccountListDto> list = OrderService.GetAccountList(param, ref reCount);
            if (list == null || list.Count == 0)
            {
                return Content("没有任何可以导出的数据！");
            }

            DataTable table = list.ToDataTable();
            ExcelHelper excel = new ExcelHelper();
            List<ExcelColumn> col = new List<ExcelColumn>()
            {
                new ExcelColumn("StudentName","学生姓名"),
                new ExcelColumn("Phone","手机号码"),
                new ExcelColumn("IDCardNo","证件号码"),
                new ExcelColumn("UserName","账号"),
                new ExcelColumn("Password","密码"),
            };
            excel.ExportToExcel(table, "学员账号", col);

            return new EmptyResult();
        }

        #endregion

        /// <summary>
        /// 根据层次、专业查询学校列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetSchoolItemList(SchoolItemListReqDto param)
        {
            int reCount;
            param.EnterpriseId = this.EnterpriseId;
            List<SchoolItemListDto> list = LevelService.GetSchoolItemList(param, out reCount);
            if (list == null)
            {
                list = new List<SchoolItemListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }
    }
}