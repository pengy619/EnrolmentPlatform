using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Content.Controllers
{
    public class ArticleCategoryController : BaseController
    {
        /// <summary>
        /// 栏目管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <returns></returns>
        public string ArticleCategoryList(GridDataRequest req)
        {
            var grd= ArticleCategoryService.GetArticleCategoryPageList(req);
            return grd.ToJson();
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddArticleCategory(ArticleCategoryDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var ret = ArticleCategoryService.AddArticleCategory(dto);
            return Json(ret);
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateArticleCategory(ArticleCategoryDto dto)
        {
            var ret = ArticleCategoryService.UpdateArticleCategory(dto);
            return Json(ret);
        }

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteArticleCategory(Guid categoryId)
        {
            var ret = ArticleCategoryService.DeleteArticleCategory(categoryId);
            return Json(ret);
        }
    }
}