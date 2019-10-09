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
        public async Task<string> ArticleCategoryList(GridDataRequest req)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ArticleCategory/GetArticleCategoryPageList",
                JsonConvert.SerializeObject(req),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddArticleCategory(ArticleCategoryDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/ArticleCategory/AddArticleCategory",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateArticleCategory(ArticleCategoryDto dto)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/ArticleCategory/UpdateArticleCategory",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteArticleCategory(Guid categoryId)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/ArticleCategory/DeleteArticleCategory",
                JsonConvert.SerializeObject(categoryId),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }
    }
}