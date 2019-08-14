using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Zip;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Order.Controllers
{
    public class ImageController : BaseController
    {
        /// <summary>
        /// 照片列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
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
                //报名单信息
                var orderInfo = OrderService.GetOrder(orderId.Value);
                ViewBag.OrderInfo = orderInfo;

                //批次
                var batchList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Batch);
                ViewBag.BatchName = batchList.Find(a => a.Id == orderInfo.BatchId).Name;

                //学校
                var schoolList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.School);
                //层级
                var levelList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Level);
                //专业
                var majorList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Major);
                var biyeInfo = schoolList.Find(a => a.Id == orderInfo.SchoolId).Name + " " + levelList.Find(a => a.Id == orderInfo.LevelId).Name
                    + " " + majorList.Find(a => a.Id == orderInfo.MajorId).Name;
                ViewBag.BiYeInfo = biyeInfo;

                //照片信息
                ViewBag.ImageDto = OrderService.FindOrderImage(orderId.Value);
            }
            else
            {
                return RedirectToAction("Index", "Image");
            }

            return View();
        }

        #region 导出

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public ActionResult Export(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids) || ids.Split('|').Count() == 0)
            {
                return Content("导出失败！");
            }
            var arr = ids.Split('|');
            List<Guid> orderIds = new List<Guid>();
            foreach (var item in arr)
            {
                orderIds.Add(Guid.Parse(item));
            }

            //报名单信息
            OrderListReqDto req = new OrderListReqDto();
            req.OrderIds = orderIds;
            req.Page = 1;
            req.FromChannelId = this.EnterpriseId;
            req.Limit = int.MaxValue;
            int reCount = 0;
            List<OrderImageListDto> orderList = OrderService.GetStudentImageList(req,ref reCount);
            if (orderList == null || orderList.Count == 0)
            {
                return Content("导出失败！");
            }

            #region 导出zip

            //所有的图片
            Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();

            #region 获得所有图片

            foreach (var dto in orderList)
            {
                Dictionary<string, string> dicItem = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(dto.LiangCunLanDiImg))
                {
                    var p1 = GetLocalPic(dto.LiangCunLanDiImg, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("两寸蓝底.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.IDCard1))
                {
                    var p1 = GetLocalPic(dto.IDCard1, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("身份证正面.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.IDCard2))
                {
                    var p1 = GetLocalPic(dto.IDCard2, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("身份证反面.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.TouXiang))
                {
                    var p1 = GetLocalPic(dto.TouXiang, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("录取通知书.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.BiYeZhengImg))
                {
                    var p1 = GetLocalPic(dto.BiYeZhengImg, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("毕业证.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.MianKaoYingYuImg))
                {
                    var p1 = GetLocalPic(dto.MianKaoYingYuImg, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("社保/居住证正面.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.MianKaoJiSuanJiImg))
                {
                    var p1 = GetLocalPic(dto.MianKaoJiSuanJiImg, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("社保/居住证反面.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.XueXinWangImg))
                {
                    var p1 = GetLocalPic(dto.XueXinWangImg, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("学信网截图.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.QiTa))
                {
                    var p1 = GetLocalPic(dto.QiTa, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("其他.jpg", p1);
                    }
                }

                //附件
                List<FileDto> fileList = FileService.GetFileList(dto.OrderId);
                if (fileList != null && fileList.Any())
                {
                    foreach (var item in fileList)
                    {
                        var p1 = GetLocalPic(item.FilePath, dto);
                        if (p1 != null)
                        {
                            dicItem.Add(item.FileName, p1);
                        }
                    }
                }

                //文件夹名称
                string key = dto.StudentName;
                if (dic.Keys.Contains(key))
                {
                    key = "_" + dto.OrderId.ToString();
                }

                //如果有照片
                if (dicItem.Count > 0)
                {
                    dic.Add(key, dicItem);
                }
            }

            #endregion

            //创建临时目录
            string tempPath = Path.Combine(this.Server.MapPath("~/Temp"), "TempData");
            DirectoryInfo di = new DirectoryInfo(tempPath);
            if (di.Exists == true)
            {
                di.Delete(true);
            }
            di.Create();

            string fileName = "照片包" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".zip";
            string fullZipFile = Path.Combine(tempPath, fileName);
            string msg = ZipHelper.ZipFile(dic, fullZipFile);
            if (msg != "")
            {
                return Content("导出失败："+ msg);
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            Response.WriteFile(fullZipFile);
            Response.Flush();
            Response.End();
            Response.Close();

            #endregion

            #region 导出excel

            ////导出模板地址
            //HSSFWorkbook hssfworkbook = null;
            //try
            //{
            //    using (FileStream file = new FileStream(this.Server.MapPath("~/Temp/OrderImageTemp.xls"), FileMode.Open, FileAccess.Read))
            //    {
            //        hssfworkbook = new HSSFWorkbook(file);
            //    }

            //    #region 图片处理

            //    //所有的图片
            //    List<PicIndex> picIndexs = new List<PicIndex>();
            //    foreach (var dto in orderList)
            //    {
            //        if (!string.IsNullOrWhiteSpace(dto.LiangCunLanDiImg))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "1", dto.LiangCunLanDiImg, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.IDCard1))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "2", dto.IDCard1, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.IDCard2))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "3", dto.IDCard2, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.TouXiang))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "4", dto.TouXiang, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.BiYeZhengImg))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "5", dto.BiYeZhengImg, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.MianKaoYingYuImg))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "6", dto.MianKaoYingYuImg, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.MianKaoJiSuanJiImg))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "7", dto.MianKaoJiSuanJiImg, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.XueXinWangImg))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "8", dto.XueXinWangImg, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(dto.QiTa))
            //        {
            //            var p1 = GetPicIndex(hssfworkbook, "9", dto.QiTa, dto);
            //            if (p1 != null)
            //            {
            //                picIndexs.Add(p1);
            //            }
            //        }
            //    }

            //    #endregion

            //    int startRow = 2;
            //    HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheet("data");
            //    IDrawing patriarch = sheet.CreateDrawingPatriarch();

            //    HSSFRow tempRow = (HSSFRow)sheet.GetRow(startRow);
            //    for (int a = 1; a < orderList.Count; a++)
            //    {
            //        HSSFRow row = (HSSFRow)sheet.CreateRow(startRow + a);
            //        row.HeightInPoints = tempRow.HeightInPoints;
            //        row.Height = tempRow.Height;

            //        //创建列
            //        for (int c = 0; c < tempRow.Cells.Count; c++)
            //        {
            //            ICell cell = row.CreateCell(c);
            //            ICell sourceCell = tempRow.GetCell(c);
            //            cell.CellStyle = sourceCell.CellStyle;
            //            cell.SetCellType(sourceCell.CellType);
            //        }
            //    }

            //    for (int i = 0; i < orderList.Count; i++)
            //    {
            //        var dataRow = orderList[i];
            //        int rowIndex = startRow + i;
            //        HSSFRow row= (HSSFRow)sheet.GetRow(rowIndex);
            //        row.Cells[0].SetCellValue(dataRow.StudentName);
            //        row.Cells[1].SetCellValue(dataRow.BatchName);
            //        row.Cells[2].SetCellValue(dataRow.SchoolName);
            //        row.Cells[3].SetCellValue(dataRow.LevelName);
            //        row.Cells[4].SetCellValue(dataRow.MajorName);
            //        var p1 = picIndexs.FirstOrDefault(a => a.ImageType == "1" && a.OrderId == dataRow.OrderId);
            //        if (p1 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 5, rowIndex, 6, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p1.PictureIndex);
            //        }

            //        var p2 = picIndexs.FirstOrDefault(a => a.ImageType == "2" && a.OrderId == dataRow.OrderId);
            //        if (p2 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 6, rowIndex, 7, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p2.PictureIndex);
            //        }

            //        var p3 = picIndexs.FirstOrDefault(a => a.ImageType == "3" && a.OrderId == dataRow.OrderId);
            //        if (p3 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 7, rowIndex, 8, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p3.PictureIndex);
            //        }

            //        var p4 = picIndexs.FirstOrDefault(a => a.ImageType == "4" && a.OrderId == dataRow.OrderId);
            //        if (p4 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 8, rowIndex, 9, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p4.PictureIndex);
            //        }

            //        var p5 = picIndexs.FirstOrDefault(a => a.ImageType == "5" && a.OrderId == dataRow.OrderId);
            //        if (p5 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 9, rowIndex, 10, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p5.PictureIndex);
            //        }

            //        var p6 = picIndexs.FirstOrDefault(a => a.ImageType == "6" && a.OrderId == dataRow.OrderId);
            //        if (p6 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 10, rowIndex, 11, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p6.PictureIndex);
            //        }

            //        var p7 = picIndexs.FirstOrDefault(a => a.ImageType == "7" && a.OrderId == dataRow.OrderId);
            //        if (p7 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 11, rowIndex, 12, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p7.PictureIndex);
            //        }

            //        var p8 = picIndexs.FirstOrDefault(a => a.ImageType == "8" && a.OrderId == dataRow.OrderId);
            //        if (p8 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 12, rowIndex, 13, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p8.PictureIndex);
            //        }

            //        var p9 = picIndexs.FirstOrDefault(a => a.ImageType == "9" && a.OrderId == dataRow.OrderId);
            //        if (p9 != null)
            //        {
            //            HSSFClientAnchor anchor = new HSSFClientAnchor(5, 2, 0, 0, 13, rowIndex, 14, (rowIndex + 1));
            //            patriarch.CreatePicture(anchor, p9.PictureIndex);
            //        }

            //        row.Cells[14].SetCellValue(dataRow.StatusName);
            //        row.Cells[15].SetCellValue(dataRow.CreateTimeStr);
            //        row.Cells[16].SetCellValue(dataRow.CreateUserName);
            //    }
            //    //导出
            //    this.NPOIExport("报名单照片列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", hssfworkbook, new List<HSSFSheet> { sheet });
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    return Content("导出失败！");
            //}

            #endregion

            return Content("导出失败！");
        }

        /// <summary>
        /// 报名单某项图片处理
        /// </summary>
        /// <param name="url">图片远程地址</param>
        /// <param name="orderDto">订单信息</param>
        /// <returns></returns>
        private string GetLocalPic(string url, OrderImageListDto orderDto)
        {
            //循环获得每个TTSBasic内PartCode所关联的图片
            string localPath = Path.Combine(this.Server.MapPath("~/Content/Upload/OrderImage") + "/" + (orderDto.BatchName + "-" + orderDto.SchoolName
                 + "-" + orderDto.LevelName + "-" + orderDto.MajorName));
            string localFile = Path.Combine(localPath, Path.GetFileName(url));

            //本地不存在，需要去远程服务器查找
            if (System.IO.File.Exists(localFile) == false)
            {
                //WebClient my = new WebClient();
                //byte[] mybyte;
                //mybyte = my.DownloadData(url);
                //MemoryStream ms = new MemoryStream(mybyte);
                //System.Drawing.Image img;
                //img = System.Drawing.Image.FromStream(ms);
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                //img.Save(localFile, ImageFormat.Jpeg);
                WebClient client = new WebClient();
                client.DownloadFile(url, localFile);
            }
            return localFile;
        }

        #region EXCEL导出图片时用到

        ///// <summary>
        ///// 报名单某项图片处理
        ///// </summary>
        ///// <param name="hssfworkbook">excel</param>
        ///// <param name="type">图片类型</param>
        ///// <param name="url">图片远程地址</param>
        ///// <param name="orderDto">订单信息</param>
        ///// <returns></returns>
        //private PicIndex GetPicIndex(HSSFWorkbook hssfworkbook,string type,string url, OrderImageListDto orderDto)
        //{
        //    //循环获得每个TTSBasic内PartCode所关联的图片
        //    string localPath = Path.Combine(this.Server.MapPath("~/Content/Upload/OrderImage") + "/" + (orderDto.BatchName + "-" + orderDto.SchoolName
        //         + "-" + orderDto.LevelName + "-" + orderDto.MajorName));
        //    string localFile = Path.Combine(localPath, Path.GetFileName(url));

        //    byte[] bytes = null;

        //    //本地不存在，需要去远程服务器查找
        //    if (System.IO.File.Exists(localFile) == false)
        //    {
        //        WebClient my = new WebClient();
        //        byte[] mybyte;
        //        mybyte = my.DownloadData(url);
        //        MemoryStream ms = new MemoryStream(mybyte);
        //        System.Drawing.Image img;
        //        img = System.Drawing.Image.FromStream(ms);
        //        if (Directory.Exists(localPath) == false)
        //        {
        //            Directory.CreateDirectory(localPath);
        //        }
        //        img.Save(localFile, ImageFormat.Jpeg);
        //        bytes = mybyte;
        //    }
        //    else
        //    {
        //        bytes = System.IO.File.ReadAllBytes(localFile);
        //    }

        //    int picIndex = hssfworkbook.AddPicture(bytes, PictureType.JPEG);
        //    PicIndex picIndexc = new PicIndex();
        //    picIndexc.OrderId = orderDto.OrderId;
        //    picIndexc.ImageType = type;
        //    picIndexc.PictureIndex = picIndex;
        //    return picIndexc;
        //}

        #endregion

        #endregion

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Search(OrderListReqDto param)
        {
            int reCount = 0;
            param.FromChannelId = this.EnterpriseId;
            List<OrderImageListDto> list = OrderService.GetStudentImageList(param, ref reCount);
            if (list == null)
            {
                list = new List<OrderImageListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="type">类型</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public JsonResult SaveImage(Guid orderId, int type, HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            string fileServerUrl = System.Configuration.ConfigurationManager.AppSettings["FileDoMain"];
            string fileName = Guid.NewGuid().ToString() + "." + file.FileName.Split('.')[1].ToString();
            System.Collections.Generic.Dictionary<object, object> parames = new System.Collections.Generic.Dictionary<object, object>();
            parames.Add("fromType", System.Configuration.ConfigurationManager.AppSettings["FileFrom"]);
            parames.Add("postFileKey", System.Configuration.ConfigurationManager.AppSettings["PostFileKey"]);
            var _saveRet = EnrolmentPlatform.Project.Infrastructure.HttpMethods.HttpPost(fileServerUrl + "/UpLoad/Index", parames, fileName, data);
            EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg _saveResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg>(_saveRet);
            if (_saveResult.IsSuccess == false)
            {
                return Json(new { ret = false, msg = _saveResult.Info });
            }

            //图片完整地址
            string fullUrl = fileServerUrl + "/" + _saveResult.Info;

            //修改报名单图片
            OrderImageDto imageDto = OrderService.FindOrderImage(orderId);
            if (type == 1)
            {
                imageDto.IDCard1 = fullUrl;
            }
            else if (type == 2)
            {
                imageDto.IDCard2 = fullUrl;
            }
            else if (type == 3)
            {
                imageDto.LiangCunLanDiImg = fullUrl;
            }
            else if (type == 4)
            {
                imageDto.BiYeZhengImg = fullUrl;
            }
            else if (type == 5)
            {
                imageDto.MianKaoYingYuImg = fullUrl;
            }
            else if (type == 6)
            {
                imageDto.MianKaoJiSuanJiImg = fullUrl;
            }
            else if (type == 7)
            {
                imageDto.XueXinWangImg = fullUrl;
            }
            else if (type == 8)
            {
                imageDto.TouXiang = fullUrl;
            }
            else if (type == 9)
            {
                imageDto.QiTa = fullUrl;
            }
            if (OrderService.UpdateImage(imageDto) == true)
            {

                //处理上传图片
                return Json(new { ret = true, msg = "上传成功！", url = fullUrl });
            }
            else
            {
                return Json(new { ret = false, msg = "上传失败！" });
            }
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public JsonResult SaveAttachment(Guid orderId, HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            string fileServerUrl = System.Configuration.ConfigurationManager.AppSettings["FileDoMain"];
            string fileName = Guid.NewGuid().ToString() + "." + file.FileName.Split('.')[1].ToString();
            System.Collections.Generic.Dictionary<object, object> parames = new System.Collections.Generic.Dictionary<object, object>();
            parames.Add("fromType", System.Configuration.ConfigurationManager.AppSettings["FileFrom"]);
            parames.Add("postFileKey", System.Configuration.ConfigurationManager.AppSettings["PostFileKey"]);
            var _saveRet = EnrolmentPlatform.Project.Infrastructure.HttpMethods.HttpPost(fileServerUrl + "/UpLoad/Index", parames, fileName, data);
            EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg _saveResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg>(_saveRet);
            if (_saveResult.IsSuccess == false)
            {
                return Json(new { ret = false, msg = _saveResult.Info });
            }

            //文件完整地址
            string fullUrl = fileServerUrl + "/" + _saveResult.Info;

            //保存文件
            var ret = FileService.AddFile(new FileDto
            {
                ForeignKeyId = orderId,
                FilePath = fullUrl,
                FileName = file.FileName,
                CreatorUserId = this.UserId,
                CreatorAccount = this.UserAccount
            });
            return Json(new { ret = ret.IsSuccess, msg = ret.Info });
        }

        /// <summary>
        /// 文件列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public string FileList(Guid orderId)
        {
            List<FileDto> list = FileService.GetFileList(orderId);
            GridDataResponse grid = new GridDataResponse
            {
                Count = list.Count,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteFile(Guid id)
        {
            var ret = FileService.DeleteFileById(id);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "删除失败。" });
            }
        }
    }
}