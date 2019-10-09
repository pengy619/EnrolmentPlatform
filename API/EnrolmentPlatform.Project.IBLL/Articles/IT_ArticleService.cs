using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IBLL.Articles
{
    /// <summary>
    /// 文章服务接口
    /// </summary>
    public interface IT_ArticleService : IBaseService<T_Article>
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        GridDataResponse GetArticlePageList(ArticleSearchDto param);

        /// <summary>
        /// 添加/编辑文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg AddOrEditArticle(ArticleDto dto);

        /// <summary>
        /// 根据Id获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ArticleDto GetArticleById(Guid id);

        /// <summary>
        /// 批量删除文章
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        ResultMsg DeleteArticles(List<Guid> idList);
    }
}
