using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Order.Controllers
{
    public class GraduationController : BaseController
    {
        /// <summary>
        /// 毕业管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 上传毕业照
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid orderId)
        {
            //报名单信息
            ViewBag.OrderInfo = OrderService.GetOrder(orderId);

            //照片信息
            ViewBag.ImageDto = OrderService.FindOrderImage(orderId);

            return View();
        }

        /// <summary>
        /// 保存毕业照片
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
            Dictionary<object, object> parames = new Dictionary<object, object>();
            parames.Add("fromType", System.Configuration.ConfigurationManager.AppSettings["FileFrom"]);
            parames.Add("postFileKey", System.Configuration.ConfigurationManager.AppSettings["PostFileKey"]);
            var _saveRet = HttpMethods.HttpPost(fileServerUrl + "/UpLoad/Index", parames, fileName, data);
            HttpResponseMsg _saveResult = Newtonsoft.Json.JsonConvert.DeserializeObject<HttpResponseMsg>(_saveRet);
            if (_saveResult.IsSuccess == false)
            {
                return Json(new { ret = false, msg = _saveResult.Info });
            }

            //图片完整地址
            string fullUrl = fileServerUrl + "/" + _saveResult.Info;

            OrderImageDto imageDto = OrderService.FindOrderImage(orderId);
            if (type == 1)
            {
                imageDto.BiYeXueJiImg = fullUrl;
            }
            else if (type == 2)
            {
                imageDto.BiYePhoto = fullUrl;
            }
            if (OrderService.UpdateBiYeImage(imageDto) == true)
            {
                return Json(new { ret = true, msg = "上传成功！", url = fullUrl });
            }
            else
            {
                return Json(new { ret = false, msg = "上传失败！" });
            }
        }

        #region 导出

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public ActionResult Export(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids) || ids.Split('|').Length == 0)
            {
                return Content("参数错误！");
            }
            List<Guid> orderIds = ids.Split('|').ToList().ConvertAll(x => Guid.Parse(x));
            //报名单信息
            OrderListReqDto req = new OrderListReqDto();
            req.OrderIds = orderIds;
            req.Page = 1;
            req.FromChannelId = this.EnterpriseId;
            req.Limit = int.MaxValue;
            if (this.IsMaster == false)
            {
                req.UserId = this.UserId;
            }
            int reCount = 0;
            List<OrderImageListDto> orderList = OrderService.GetStudentImageList(req, ref reCount);
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
                if (!string.IsNullOrWhiteSpace(dto.BiYeXueJiImg))
                {
                    var p1 = GetLocalPic(dto.BiYeXueJiImg, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("学信网学籍截图.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.BiYePhoto))
                {
                    var p1 = GetLocalPic(dto.BiYePhoto, dto);
                    if (p1 != null)
                    {
                        dicItem.Add("毕业照片.jpg", p1);
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
                return Content("导出失败：" + msg);
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            Response.WriteFile(fullZipFile);
            Response.Flush();
            Response.End();
            Response.Close();

            #endregion

            return new EmptyResult();
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
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                WebClient client = new WebClient();
                client.DownloadFile(url, localFile);
            }
            return localFile;
        }

        #endregion
    }
}