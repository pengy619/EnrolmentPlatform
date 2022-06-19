using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
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

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Order.Controllers
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
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Search(OrderListReqDto param)
        {
            int reCount = 0;
            param.Status = OrderStatusEnum.Join;
            param.ToLearningCenterId = this.SupplierId;
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

        #region 导出

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public ActionResult Export(string ids, OrderListReqDto req)
        {
            //报名单信息
            if (!string.IsNullOrWhiteSpace(ids))
            {
                List<Guid> orderIds = ids.Split('|').ToList().ConvertAll(x => Guid.Parse(x));
                req.OrderIds = orderIds;
            }
            req.Page = 1;
            req.Limit = int.MaxValue;
            req.ToLearningCenterId = this.SupplierId;
            int reCount = 0;
            List<OrderImageListDto> orderList = OrderService.GetStudentImageList(req, ref reCount);
            if (orderList == null || orderList.Count == 0)
            {
                return Content("导出失败！");
            }

            #region 导出zip

            //所有的图片
            Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> dicItem1 = new Dictionary<string, string>();
            Dictionary<string, string> dicItem2 = new Dictionary<string, string>();

            #region 获得所有图片

            foreach (var dto in orderList)
            {
                if (!string.IsNullOrWhiteSpace(dto.BiYeXueJiImg))
                {
                    var p1 = GetLocalPic(dto.BiYeXueJiImg, dto);
                    if (p1 != null)
                    {
                        dicItem1.Add($"{dto.IDCardNo}.jpg", p1);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.BiYePhoto))
                {
                    var p1 = GetLocalPic(dto.BiYePhoto, dto);
                    if (p1 != null)
                    {
                        dicItem2.Add($"{dto.IDCardNo}.jpg", p1);
                    }
                }
            }

            //分两个文件夹存放照片
            if (dicItem1.Count > 0)
            {
                dic.Add("学信网学籍截图", dicItem1);
            }
            if (dicItem2.Count > 0)
            {
                dic.Add("毕业照片", dicItem2);
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