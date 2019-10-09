using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IDAL.Articles
{
    public interface IT_ArticleRepository : IBaseRepository<T_Article>
    {
        /// <summary>
        /// 获取渠道商文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        GridDataResponse GetDistributorArticlePageList(DistributorArticleSearchDto param);

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        GridDataResponse GetArticlePageList(ArticleSearchDto param);

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ArticleDto GetArticleById(Guid id);

        /// <summary>
        /// 通过栏目名称获取文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ArticleDto GetArticleByCateName(ArticleByNameSearchDto dto);
    }
}
