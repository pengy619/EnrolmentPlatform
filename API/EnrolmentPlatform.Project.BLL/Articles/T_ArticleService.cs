using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.IBLL.Articles;
using EnrolmentPlatform.Project.IDAL.Articles;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.BLL.Articles
{
    /// <summary>
    /// 文章服务类
    /// </summary>
    public class T_ArticleService : BaseService<T_Article>, IT_ArticleService, IInterceptorLogic
    {
        private IT_ArticleCategoryRepository _articleCategoryRepository;

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_ArticleRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            _articleCategoryRepository = DIContainer.Resolve<IT_ArticleCategoryRepository>();
            base.AddDisposableObject(_articleCategoryRepository);
            return true;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public GridDataResponse GetArticlePageList(ArticleSearchDto param)
        {
            return (this.CurrentRepository as IT_ArticleRepository).GetArticlePageList(param);
        }

        /// <summary>
        /// 添加/编辑文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg AddOrEditArticle(ArticleDto dto)
        {
            ResultMsg _resultMsg = new ResultMsg();
            var result = 0;
            if (dto.ArticleId.IsEmpty())
            {
                var entity = dto.MapTo<ArticleDto, T_Article>();
                entity.Id = Guid.NewGuid();
                result = CurrentRepository.AddEntity(entity);
            }
            else
            {
                var entity = CurrentRepository.FindEntityById(dto.ArticleId);
                entity.Title = dto.Title;
                entity.ClassifyId = dto.ClassifyId;
                entity.Content = dto.Content;
                entity.Status = dto.Status;
                entity.Abstract = dto.Abstract;
                entity.FilePath = dto.FilePath;
                result = CurrentRepository.UpdateEntity(entity);
            }
            _resultMsg.IsSuccess = result > 0;
            return _resultMsg;
        }

        /// <summary>
        /// 根据Id获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArticleDto GetArticleById(Guid id)
        {
            return (this.CurrentRepository as IT_ArticleRepository).GetArticleById(id);
        }

        /// <summary>
        /// 批量删除文章
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>

        public ResultMsg DeleteArticles(List<Guid> idList)
        {
            ResultMsg _resultMsg = new ResultMsg();
            int result = CurrentRepository.PhysicsDeleteBy(t => idList.Contains(t.Id));
            _resultMsg.IsSuccess = result > 0;
            return _resultMsg;
        }
    }
}
