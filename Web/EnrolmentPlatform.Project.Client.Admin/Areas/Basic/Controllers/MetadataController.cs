﻿using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Basic.Controllers
{
    public class MetadataController : BaseController
    {
        /// <summary>
        /// 学校管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SchoolManage()
        {
            return View();
        }

        /// <summary>
        /// 层次管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LevelManage()
        {
            return View();
        }

        /// <summary>
        /// 专业管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MajorManage()
        {
            return View();
        }

        /// <summary>
        /// 批次管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchManage()
        {
            return View();
        }

        /// <summary>
        /// 元数据列表
        /// </summary>
        /// <returns></returns>
        public string MetadataList(MetadataSearchDto req)
        {
            var data = MetadataService.GetPagedList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 添加元数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddMetadata(MetadataDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var ret = MetadataService.Add(dto);
            return Json(ret);
        }

        /// <summary>
        /// 更新元数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateMetadata(MetadataDto dto)
        {
            var ret = MetadataService.Update(dto);
            return Json(ret);
        }

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMetadata(List<Guid> idList)
        {
            var ret = MetadataService.Delete(idList);
            return Json(ret);
        }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EnableOrDisable(Guid id, bool isEnable)
        {
            var ret = MetadataService.EnableOrDisable(id, isEnable);
            return Json(ret);
        }

        /// <summary>
        /// 配置层次及专业
        /// </summary>
        /// <returns></returns>
        public ActionResult SchoolConfig(Guid schoolId)
        {
            return View(schoolId);
        }

        public JsonResult GetSchoolConfigTreeData(Guid schoolId)
        {
            var levelList = MetadataService.GetList(MetadataTypeEnum.Level);
            var majorList = MetadataService.GetList(MetadataTypeEnum.Major);
            //已选集合
            var selectedList = SchoolConfigService.GetSchoolConfigList(schoolId);
            var listtree = (from a in levelList
                            select new JSTree
                            {
                                id = a.Id,
                                text = a.Name,
                                li_attr = new LiAttr() { level = 0 },
                                state = new State() { opened = false },
                                children = (from b in majorList
                                            select new JSTree
                                            {
                                                id = Guid.NewGuid(),
                                                text = b.Name,
                                                li_attr = new LiAttr() { parentId = a.Id, level = 1, itemId = b.Id },
                                                state = new State() { opened = false, selected = selectedList.Exists(t => t.Key == a.Id && t.Value == b.Id) }
                                            }).ToList()
                            }).ToList();
            return Json(listtree, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveConfig(SchoolConfigDto dto)
        {
            var ret = SchoolConfigService.SaveConfig(dto);
            return Json(ret);
        }

        /// <summary>
        /// 配置是否存在
        /// </summary>
        /// <returns></returns>
        public JsonResult ConfigIsExist(Guid schoolId)
        {
            bool isExist = SchoolConfigService.FindSubItemById(schoolId).Count() > 0;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 设置费用策略
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargeStrategy(Guid schoolId)
        {
            //招生机构
            ViewBag.InstitutionList = EnterpriseService.GetUserList(SystemTypeEnum.TrainingInstitutions);
            //学院中心
            ViewBag.LearningCenterList = EnterpriseService.GetUserList(SystemTypeEnum.LearningCenter);

            return View(schoolId);
        }

        public JsonResult GetTreeData(Guid schoolId)
        {
            var allItem = SchoolConfigService.GetAllList().ToList();
            var listtree = (from a in allItem
                            where a.ParentId == schoolId
                            select new JSTree
                            {
                                id = a.Id,
                                text = a.ItemName,
                                li_attr = new LiAttr() { level = 0 },
                                state = new State() { opened = true },
                                children = (from b in allItem
                                            where b.ParentId == a.Id
                                            select new JSTree
                                            {
                                                id = b.Id,
                                                text = b.ItemName,
                                                li_attr = new LiAttr() { parentId = a.ItemId, level = 1, itemId = b.ItemId },
                                                state = new State() { opened = false }
                                            }).ToList()
                            }).ToList();
            return Json(listtree, JsonRequestBehavior.AllowGet);
        }

        #region 收费策略

        /// <summary>
        /// 通用费用策略列表
        /// </summary>
        /// <returns></returns>
        public string ChargeList(ChargeStrategySearchDto req)
        {
            var data = ChargeStrategyService.GetPagedList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 机构费用策略列表
        /// </summary>
        /// <returns></returns>
        public string InstitutionChargeList(ChargeStrategySearchDto req)
        {
            var data = ChargeStrategyService.GetInstitutionPagedList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 学院中心费用策略列表
        /// </summary>
        /// <returns></returns>
        public string LearningCenterChargeList(ChargeStrategySearchDto req)
        {
            var data = ChargeStrategyService.GetLearningCenterPagedList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 添加收费策略
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCharge(ChargeStrategyDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var ret = ChargeStrategyService.Add(dto);
            return Json(ret);
        }

        /// <summary>
        /// 删除收费策略
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCharge(Guid id)
        {
            var ret = ChargeStrategyService.Delete(id);
            return Json(ret);
        }

        #endregion

        #region 指标设置

        /// <summary>
        /// 指标设置
        /// </summary>
        /// <param name="schoolId">schoolId</param>
        /// <returns></returns>
        public ActionResult StockSetting(Guid schoolId)
        {
            ViewBag.SchoolId = schoolId;
            var res = MetadataService.GetPagedList(new MetadataSearchDto()
            {
                Type = MetadataTypeEnum.Batch,
                Limit = Int32.MaxValue,
                Page = 1
            });
            ViewBag.BatchList = (List<MetadataDto>)res.Data;
            return View();
        }

        /// <summary>
        /// 指标设置列表
        /// </summary>
        /// <returns></returns>
        public string StockSettingList(StockSettingSearchDto req)
        {
            var data = StockSettingService.GetList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 保存指标设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveStockSetting(StockSettingDto dto)
        {
            dto.UserId = this.UserId;
            dto.UserName = this.UserUser;
            ResultMsg msg = null;
            if (dto.BatchId == Guid.Empty || dto.SchoolId == Guid.Empty || dto.LevelId == Guid.Empty || dto.MajorId == Guid.Empty)
            {
                return Json(new ResultMsg() { IsSuccess = false, Info = "数据错误。" });
            }
            if (dto.StockSettingId.HasValue == true)
            {
                msg = StockSettingService.Update(dto);
            }
            else
            {
                msg = StockSettingService.Add(dto);
            }
            return Json(msg);
        }

        /// <summary>
        /// 删除指标设置
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStockSetting(Guid id)
        {
            var ret = StockSettingService.Delete(id);
            return Json(ret);
        }
        #endregion

        #region 自定义字段设置

        /// <summary>
        /// 自定义字段
        /// </summary>
        /// <param name="schoolId">schoolId</param>
        /// <returns></returns>
        public ActionResult CustomerField(Guid schoolId)
        {
            ViewBag.SchoolId = schoolId;
            return View();
        }

        /// <summary>
        /// 自定义字段列表
        /// </summary>
        /// <returns></returns>
        public string CustomerFieldList(GetAllListSearchDto req)
        {
            var data = CustomerFieldService.GetList(req);
            return data.ToJson();
        }

        /// <summary>
        /// 删除自定义字段
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCustomerField(Guid id)
        {
            var ret = CustomerFieldService.Delete(id);
            return Json(ret);
        }

        /// <summary>
        /// 保存自定义字段
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveCustomerField(CustomerFieldDto dto)
        {
            dto.UserId = this.UserId;
            dto.UserName = this.UserUser;
            ResultMsg msg = null;
            if (dto.Id.HasValue == true)
            {
                msg = CustomerFieldService.Update(dto);
            }
            else
            {
                msg = CustomerFieldService.Add(dto);
            }
            return Json(msg);
        }

        #endregion

        #region 学校证件配置

        /// <summary>
        /// 学校证件配置
        /// </summary>
        /// <returns></returns>
        public ActionResult SchoolImageConfig(Guid schoolId)
        {
            //学校必须上传的证件
            var imageTypes = MetadataService.GetSchoolImageTypes(schoolId);
            ViewBag.SchoolImageTypes = JsonConvert.SerializeObject(imageTypes);
            return View(schoolId);
        }

        /// <summary>
        /// 保存学校证件配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveSchoolImageConfig(SchoolImageConfigDto dto)
        {
            dto.CreatorUserId = this.UserId;
            var ret = MetadataService.SaveSchoolImageConfig(dto);
            return Json(ret);
        }
        #endregion
    }
}