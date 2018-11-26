using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}