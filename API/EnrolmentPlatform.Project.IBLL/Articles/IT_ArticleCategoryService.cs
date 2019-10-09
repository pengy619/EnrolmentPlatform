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
    /// 文章分类服务接口
    /// </summary>
    public interface IT_ArticleCategoryService : IBaseService<T_ArticleCategory>
    {
        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <returns></returns>
        IList<ArticleCategoryDto> GetArticleCategoryList();

        /// <summary>
        /// 获取栏目分页列表
        /// </summary>
        /// <returns></returns>
        GridDataResponse GetArticleCategoryPageList(GridDataRequest req);

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg AddArticleCategory(ArticleCategoryDto dto);

        /// <summary>
        /// 编辑栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg UpdateArticleCategory(ArticleCategoryDto dto);

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultMsg DeleteArticleCategory(Guid id);
    }
}
