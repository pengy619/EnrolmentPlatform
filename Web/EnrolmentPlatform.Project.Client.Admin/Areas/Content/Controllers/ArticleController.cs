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
            ViewBag.ArticleCategories = GetArticleCategoryList();

            return View();
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> ArticleList(ArticleSearchDto param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Article/GetArticlePageList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteArticles(List<Guid> idList)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Article/DeleteArticles",
                JsonConvert.SerializeObject(idList),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }

        /// <summary>
        /// 内容添加修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? id)
        {
            //内容栏目
            ViewBag.ArticleCategories = GetArticleCategoryList();

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
        public async Task<ActionResult> AddOrEditArticle(ArticleDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Article/AddOrEditArticle",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
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
            ArticleDto dto = new ArticleDto();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("id", id.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Article/GetArticleById", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                dto = ret.Data.ToString().ToObject<ArticleDto>();
            }
            return dto;
        }

        private List<ArticleCategoryDto> GetArticleCategoryList()
        {
            List<ArticleCategoryDto> list = new List<ArticleCategoryDto>();
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ArticleCategory/GetArticleCategoryList", "", "",
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ArticleCategoryDto>();
            }
            return list;
        }
    }
}