using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
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
    public class ArticleController : BaseController
    {
        /// <summary>
        /// 内容管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //内容栏目
            ViewBag.ArticleCategories = ArticleCategoryService.GetArticleCategoryList().ToList();
            return View();
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public string ArticleList(ArticleSearchDto param)
        {
            var grd = ArticleService.GetArticlePageList(param);
            return grd.ToJson();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteArticles(List<Guid> idList)
        {
            var ret = ArticleService.DeleteArticles(idList);
            return Json(ret);
        }

        /// <summary>
        /// 内容添加修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? id)
        {
            //内容栏目
            ViewBag.ArticleCategories = ArticleCategoryService.GetArticleCategoryList().ToList();

            ArticleDto dto = new ArticleDto();
            if (id.HasValue)
            {
                dto = GetArticleInfo(id.Value);
            }
            return View(dto);
        }

        /// <summary>
        /// 添加/编辑文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddOrEditArticle(ArticleDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var ret = ArticleService.AddOrEditArticle(dto);
            return Json(ret);
        }

        /// <summary>
        /// 内容详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid id)
        {
            var dto = GetArticleInfo(id);
            return View(dto);
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ArticleDto GetArticleInfo(Guid id)
        {
            return ArticleService.GetArticleById(id);
        }
    }
}