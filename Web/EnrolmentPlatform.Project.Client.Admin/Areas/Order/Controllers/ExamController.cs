using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Order.Controllers
{
    public class ExamController : BaseController
    {
        /// <summary>
        /// 考试管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 考试列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string ExamSearch(ExamSearchDto req)
        {
            var data = ExamService.GetPagedList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 考试名单
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? id)
        {
            return View(id ?? Guid.Empty);
        }

        /// <summary>
        /// 导入名单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ImportList(string name, HttpPostedFileBase file)
        {
            List<ExamInfoDto> examList = new List<ExamInfoDto>();
            IWorkbook workbook = null;
            string fileName = file.FileName;
            using (Stream inputStream = file.InputStream)
            {
                try
                {
                    //把excel文件中的数据写入workbook中
                    if (fileName.IndexOf(".xlsx") > 0)
                    {
                        workbook = new XSSFWorkbook(inputStream);
                    }
                    else if (fileName.IndexOf(".xls") > 0)
                    {
                        workbook = new HSSFWorkbook(inputStream);
                    }
                    //读取当前表数据
                    ISheet sheet = workbook.GetSheetAt(0);
                    for (int i = 1; i <= sheet.LastRowNum; i++)  //LastRowNum 是当前表的总行数
                    {
                        //读取当前行数据
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            ExamInfoDto examInfo = new ExamInfoDto();
                            //遍历每行中每列的数据
                            for (int j = 0; j < row.LastCellNum; j++)
                            {
                                //获取单元格
                                ICell cell = row.GetCell(j);
                                //获取单元格的值
                                cell.SetCellType(CellType.String);
                                string cellValue = cell.StringCellValue;
                                switch (j)
                                {
                                    case 0:
                                        examInfo.StudentName = cellValue;
                                        break;
                                    case 1:
                                        examInfo.StudentNo = cellValue;
                                        break;
                                    case 2:
                                        examInfo.BatchName = cellValue;
                                        break;
                                    case 3:
                                        examInfo.LevelName = cellValue;
                                        break;
                                    case 4:
                                        examInfo.MajorName = cellValue;
                                        break;
                                    case 5:
                                        examInfo.UserName = cellValue;
                                        break;
                                    case 6:
                                        examInfo.ExamPlace = cellValue;
                                        break;
                                    case 7:
                                        examInfo.MailAddress = cellValue;
                                        break;
                                    case 8:
                                        examInfo.ReturnAddress = cellValue;
                                        break;
                                }
                            }
                            examList.Add(examInfo);
                        }
                    }
                    workbook.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            var dto = new AddExamDto
            {
                Name = name,
                ExamList = examList,
                CreatorUserId = this.UserId,
                CreatorAccount = this.UserAccount
            };
            var ret = ExamService.Add(dto);
            return Json(ret);
        }

        /// <summary>
        /// 考试名单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string ExamListSearch(ExamListSearchDto req)
        {
            var data = ExamService.GetExamList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 导出名单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExportList(Guid id)
        {
            var req = new ExamListSearchDto
            {
                Page = 1,
                Limit = int.MaxValue,
                ExamId = id
            };
            List<ExamInfoDto> list = (List<ExamInfoDto>)ExamService.GetExamList(req).Data;
            if (list == null || list.Count == 0)
            {
                return Content("没有任何可以导出的数据！");
            }

            #region 导出

            HSSFWorkbook hssfworkbook = null;
            try
            {
                using (FileStream file = new FileStream(this.Server.MapPath("~/Temp/ExamTemp.xls"), FileMode.Open, FileAccess.Read))
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
                var item = list[i];
                row.Cells[0].SetCellValue(item.StudentName);
                row.Cells[1].SetCellValue(item.StudentNo);
                row.Cells[2].SetCellValue(item.BatchName);
                row.Cells[3].SetCellValue(item.LevelName);
                row.Cells[4].SetCellValue(item.MajorName);
                row.Cells[5].SetCellValue(item.UserName);
                row.Cells[6].SetCellValue(item.ExamPlace);
                row.Cells[7].SetCellValue(item.MailAddress);
                row.Cells[8].SetCellValue(item.ReturnAddress);
            }

            //导出
            this.NPOIExport("考试名单" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", hssfworkbook, new List<HSSFSheet> { (HSSFSheet)sheet });

            #endregion

            return null;
        }
    }
}