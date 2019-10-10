using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        /// 发布文章
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PublishArticles(List<Guid> idList)
        {
            var ret = ArticleService.PublishArticles(idList);
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

        #region 公告附件

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveAttachment(HttpPostedFileBase file)
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
            return Json(new { ret = true, FileName = file.FileName, FilePath = fullUrl });
        }

        #endregion
    }
}