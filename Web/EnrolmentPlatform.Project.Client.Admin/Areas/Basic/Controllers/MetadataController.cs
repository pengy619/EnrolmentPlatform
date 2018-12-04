using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Basics;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var data = metadataService.GetPagedList(req);
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
            var ret = metadataService.Add(dto);
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
            var ret = metadataService.Update(dto);
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
            var ret = metadataService.Delete(idList);
            return Json(ret);
        }

        /// <summary>
        /// 配置层次及专业
        /// </summary>
        /// <returns></returns>
        public ActionResult SchoolConfig(Guid schoolId)
        {
            return View();
        }

        public ActionResult GetList()
        {
            List<JSTree> listtree = new List<JSTree>();
            var levelList = metadataService.GetList(MetadataTypeEnum.Level);
            var majorList = metadataService.GetList(MetadataTypeEnum.Major);
            if (levelList != null && levelList.Any())
            {
                foreach (var level in levelList)
                {
                    JSTree jstree = new JSTree();
                    jstree.id = level.Id;
                    jstree.text = level.Name;
                    jstree.li_attr = new LiAttr() { level = 0 };
                    jstree.state = new State() { opened = true };
                    if (majorList != null && majorList.Any())
                    {
                        List<JSTree> listchild = new List<JSTree>();
                        foreach (var major in majorList)
                        {
                            JSTree jstreeitem = new JSTree();
                            jstreeitem.id = Guid.NewGuid();
                            jstreeitem.text = major.Name;
                            jstreeitem.state = new State() { opened = false };
                            jstreeitem.li_attr = new LiAttr() { parentId = level.Id, level = 1 };
                            listchild.Add(jstreeitem);
                        }
                        jstree.children = listchild;
                    }
                    listtree.Add(jstree);
                }
            }
            return Json(listtree, JsonRequestBehavior.AllowGet);
        }
    }
}