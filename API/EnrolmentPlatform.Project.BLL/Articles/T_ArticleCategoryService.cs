using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
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
    /// 文章分类服务类
    /// </summary>
    public class T_ArticleCategoryService : BaseService<T_ArticleCategory>, IT_ArticleCategoryService, IInterceptorLogic
    {
        private IT_ArticleRepository _articleRepository;

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_ArticleCategoryRepository>();
            _articleRepository = DIContainer.Resolve<IT_ArticleRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            base.AddDisposableObject(_articleRepository);
            return true;
        }

        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <returns></returns>
       
        public IList<ArticleCategoryDto> GetArticleCategoryList()
        {
            var list = this.CurrentRepository.LoadEntities(o => !o.IsDelete).Select(o => new ArticleCategoryDto
            {
                CategoryId = o.Id,
                CategoryName = o.CateName
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取栏目分页列表
        /// </summary>
        /// <returns></returns>
       
        public GridDataResponse GetArticleCategoryPageList(GridDataRequest req)
        {
            GridDataResponse res = new GridDataResponse();
            res.Data = this.CurrentRepository.LoadPageEntitiesOrderByField(t => !t.IsDelete,
               req.Field,
               req.Limit,
               req.Page,
               out int records,
               (req.Sort ?? "desc").ToLower().Equals("asc")
                ).Select(t => new ArticleCategoryDto
                {
                    CategoryId = t.Id,
                    CategoryName = t.CateName
                }).ToList();
            res.Count = records;
            return res;
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg AddArticleCategory(ArticleCategoryDto dto)
        {
            ResultMsg _resultMsg = new ResultMsg();
            bool isExist = CurrentRepository.Count(t => t.CateName == dto.CategoryName) > 0;
            if (isExist)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = "栏目名称已存在";
                return _resultMsg;
            }
            var entity = new T_ArticleCategory
            {
                Id = Guid.NewGuid(),
                CateName = dto.CategoryName,
                CreatorUserId = dto.CreatorUserId,
                CreatorAccount = dto.CreatorAccount
            };
            _resultMsg.IsSuccess = CurrentRepository.AddEntity(entity) > 0;
            return _resultMsg;
        }

        /// <summary>
        /// 编辑栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg UpdateArticleCategory(ArticleCategoryDto dto)
        {
            ResultMsg _resultMsg = new ResultMsg();
            bool isExist = CurrentRepository.Count(t => !t.Id.Equals(dto.CategoryId) && t.CateName == dto.CategoryName) > 0;
            if (isExist)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = "栏目名称已存在";
                return _resultMsg;
            }
            var entity = CurrentRepository.FindEntityById(dto.CategoryId);
            entity.CateName = dto.CategoryName;
            _resultMsg.IsSuccess = CurrentRepository.UpdateEntity(entity) > 0;
            return _resultMsg;
        }

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMsg DeleteArticleCategory(Guid id)
        {
            ResultMsg _resultMsg = new ResultMsg();
            if (_articleRepository.Count(t => t.ClassifyId == id) > 0)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = "该栏目已被使用不允许删除";
                return _resultMsg;
            }
            _resultMsg.IsSuccess = CurrentRepository.PhysicsDeleteBy(t => t.Id.Equals(id)) > 0;
            return _resultMsg;
        }
    }
}
