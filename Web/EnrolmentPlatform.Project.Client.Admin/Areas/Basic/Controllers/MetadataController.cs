using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Basics;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
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
        /// 配置层次及专业
        /// </summary>
        /// <returns></returns>
        public ActionResult SchoolConfig(Guid schoolId)
        {
            //第一级为层次
            StringBuilder sb = new StringBuilder("[");
            var levelList = MetadataService.GetList(MetadataTypeEnum.Level);
            var majorList = MetadataService.GetList(MetadataTypeEnum.Major);
            for (int i = 0; i < levelList.Count; i++)
            {
                //一级模块组装
                var item = levelList[i];
                sb.Append("{ title: \"" + item.Name + "\", value: \"" + item.Id + "\", data: [");

                //第二级为专业
                for (int j = 0; j < majorList.Count; j++)
                {
                    //二级菜单组装
                    var item2 = majorList[j];
                    sb.Append("{ title: \"" + item2.Name + "\", value: \"" + item.Id + "_" + item2.Id + "\", data: [");

                    sb.Append("]}");
                    if (j != majorList.Count - 1)
                    {
                        sb.Append(",");
                    }
                }

                sb.Append("]}");
                if (i != levelList.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("]");
            ViewBag.DataList = sb.ToString();

            //已选集合
            var selectedList = new List<string>();
            var allItem = SchoolConfigService.GetAllList().ToList();
            var levelIds = allItem.Where(t => t.ParentId == schoolId).Select(t => new { t.Id, t.ItemId }).ToList();
            if (levelIds != null && levelIds.Any())
            {
                foreach (var item in levelIds)
                {
                    var majorIds = allItem.Where(t => t.ParentId == item.Id).Select(t => item.ItemId + "_" + t.ItemId).ToList();
                    selectedList.Add(item.ItemId.ToString());
                    selectedList.AddRange(majorIds);
                }
            }
            ViewBag.SelectedIds = string.Join(",", selectedList);

            return View(schoolId);
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

        /// <summary>
        /// 收费策略列表
        /// </summary>
        /// <returns></returns>
        public string ChargeList(ChargeStrategySearchDto req)
        {
            var data = ChargeStrategyService.GetPagedList(req);
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
    }
}