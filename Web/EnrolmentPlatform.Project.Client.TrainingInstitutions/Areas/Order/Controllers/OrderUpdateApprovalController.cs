using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Order.Controllers
{
    public class OrderUpdateApprovalController : BaseController
    {
        /// <summary>
        /// 订单修改审核列表
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
        public string Search(OrderUpdateApprovalReq param)
        {
            int reCount = 0;
            param.FromChannelId = this.EnterpriseId;
            List<OrderUpdateApprovalListDto> list = OrderApprovalService.GetOrderUpdateApprovalList(param, out reCount);
            if (list == null)
            {
                list = new List<OrderUpdateApprovalListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="approvalId">approvalId</param>
        /// <param name="action">action</param>
        /// <returns></returns>
        public ActionResult OrderInfo(Guid? orderId,Guid? approvalId,string action)
        {
            if (orderId.HasValue == false && approvalId.HasValue == false)
            {
                return RedirectToAction("Index", "OrderUpdateApproval");
            }

            OrderDto orderInfo = null;
            if (orderId.HasValue == true)
            {
                var orderApproval = OrderApprovalService.GetOrderApplyApprovalInfoByOrderId(orderId.Value);
                if (orderApproval != null)
                {
                    return OrderInfo(null, orderApproval.ApprovalId, "update");
                }
                orderInfo = OrderService.GetOrder(orderId.Value);
            }
            else
            {
                var orderApproval = OrderApprovalService.GetOrderApplyApprovalInfo(approvalId.Value);
                if (orderApproval == null)
                {
                    return RedirectToAction("Index", "OrderUpdateApproval");
                }
                orderInfo = OrderService.GetOrder(orderApproval.OrderId);
                orderInfo.Address = orderApproval.Address;
                orderInfo.BiYeZhengBianHao = orderApproval.BiYeZhengBianHao;
                orderInfo.CreateUserName = orderApproval.ZhaoShengLaoShi;
                orderInfo.Email = orderApproval.Email;
                orderInfo.GongZuoDanWei = orderApproval.GongZuoDanWei;
                orderInfo.GraduateSchool = orderApproval.GraduateSchool;
                orderInfo.HighesDegree = orderApproval.HighesDegree;
                orderInfo.IDCardNo = orderApproval.IDCardNo;
                orderInfo.JiGuan = orderApproval.JiGuan;
                orderInfo.MinZu = orderApproval.MinZu;
                orderInfo.Phone = orderApproval.Phone;
                orderInfo.Remark = orderApproval.Remark;
                orderInfo.Sex = orderApproval.Sex;
                orderInfo.StudentName = orderApproval.StudentName;
                orderInfo.TencentNo = orderApproval.TencentNo;
                orderInfo.SuoDuZhuanYe = orderApproval.SuoDuZhuanYe;
                orderInfo.IsTvUniversity = orderApproval.IsTvUniversity;
                orderInfo.GraduationTime = orderApproval.GraduationTime;
                ViewBag.OrderApprovalId = orderApproval.ApprovalId.Value;
            }

            //订单信息
            ViewBag.OrderInfo = orderInfo;
            //批次
            ViewBag.BatchList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.Batch);
            //学校
            ViewBag.SchoolList = MetadataService.GetList(DTO.Enums.Basics.MetadataTypeEnum.School);
            return View();
        }

        /// <summary>
        /// 订单图片信息
        /// </summary>
        /// <param name="approvalId">approvalId</param>
        /// <param name="action">action</param>
        /// <returns></returns>
        public ActionResult OrderImageInfo(Guid approvalId, string action)
        {
            var approval = OrderApprovalService.GetOrderApplyApprovalInfo(approvalId);
            if (approval == null)
            {
                return RedirectToAction("Index", "OrderUpdateApproval");
            }

            //报名单信息
            var orderInfo = OrderService.GetOrder(approval.OrderId);
            orderInfo.Address = approval.Address;
            orderInfo.BiYeZhengBianHao = approval.BiYeZhengBianHao;
            orderInfo.CreateUserName = approval.ZhaoShengLaoShi;
            orderInfo.Email = approval.Email;
            orderInfo.GongZuoDanWei = approval.GongZuoDanWei;
            orderInfo.GraduateSchool = approval.GraduateSchool;
            orderInfo.HighesDegree = approval.HighesDegree;
            orderInfo.IDCardNo = approval.IDCardNo;
            orderInfo.JiGuan = approval.JiGuan;
            orderInfo.MinZu = approval.MinZu;
            orderInfo.Phone = approval.Phone;
            orderInfo.Remark = approval.Remark;
            orderInfo.Sex = approval.Sex;
            orderInfo.StudentName = approval.StudentName;
            orderInfo.TencentNo = approval.TencentNo;
            orderInfo.SuoDuZhuanYe = approval.SuoDuZhuanYe;
            orderInfo.IsTvUniversity = approval.IsTvUniversity;
            orderInfo.GraduationTime = approval.GraduationTime;
            ViewBag.OrderApprovalId = approval.ApprovalId.Value;
            ViewBag.OrderInfo = orderInfo;
            ViewBag.ApprovalInfo = approval;

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
            ViewBag.ImageDto = OrderApprovalService.GetOrderImageApplyApprovalInfo(approval.ApprovalId.Value);
            ViewBag.ApprovalId = approvalId;
            return View();
        }

        /// <summary>
        /// 删除报名单
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(Guid[] ids)
        {
            var ret = OrderApprovalService.Delete(ids.ToList());
            if (ret.IsSuccess == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "删除失败。" });
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Submit(Guid[] ids)
        {
            var ret = OrderApprovalService.Submit(ids.ToList());
            if (ret.IsSuccess == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = ret.Info });
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(OrderApprovalDto dto)
        {
            dto.UserId = this.UserId;
            var ret = OrderApprovalService.Save(dto);
            if (ret.IsSuccess == true)
            {
                return Json(new { ret = true, data = ret.Data.ToString() });
            }
            else
            {
                return Json(new { ret = false, msg = ret.Info });
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

        #region 图片处理

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="approvalId">订单审批ID</param>
        /// <param name="type">类型</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public JsonResult SaveImage(Guid approvalId, int type, HttpPostedFileBase file)
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
            OrderApprovalImgDto imageDto = OrderApprovalService.GetOrderImageApplyApprovalInfo(approvalId);
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

            if (OrderApprovalService.SaveImage(imageDto).IsSuccess == true)
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
        /// <param name="approvalId">订单审批ID</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public JsonResult SaveAttachment(Guid approvalId, HttpPostedFileBase file)
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
                ForeignKeyId = approvalId,
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
        /// <param name="approvalId"></param>
        /// <returns></returns>
        public string FileList(Guid approvalId)
        {
            List<FileDto> list = FileService.GetFileList(approvalId);
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

        #endregion
    }
}