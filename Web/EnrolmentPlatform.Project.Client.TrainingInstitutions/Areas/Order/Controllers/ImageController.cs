using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;

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

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public ActionResult Export(string ids)
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
                return Json(new { ret = false, msg = "上传失败！" });
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
    }
}