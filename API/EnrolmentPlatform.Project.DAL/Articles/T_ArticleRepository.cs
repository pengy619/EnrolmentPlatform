using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Article;
using EnrolmentPlatform.Project.IDAL.Articles;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DAL.Articles
{
    public class T_ArticleRepository : BaseRepository<T_Article>, IT_ArticleRepository
    {
        /// <summary>
        /// 获取渠道商文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public GridDataResponse GetDistributorArticlePageList(DistributorArticleSearchDto param)
        {
            GridDataResponse res = new GridDataResponse();
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = from a in _dbcontext.T_Article
                               join b in _dbcontext.T_ArticleCategory
                               on a.ClassifyId equals b.Id
                               where b.CateName.Trim() == param.ClassifyName
                               && a.Status == (int)ArticleStatusEnum.Publish
                               select new ArticleDto
                               {
                                   ArticleId = a.Id,
                                   Title = a.Title,
                                   ClassifyName = b.CateName,
                                   Abstract = a.Abstract,
                                   FilePath = a.FilePath,
                                   CreatorAccount = a.CreatorAccount,
                                   CreatorTime = a.CreatorTime,
                                   Status = a.Status
                               };

            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, "CreatorTime", false);
            res.Count = _tIQueryable.Count();
            _tIQueryable = _tIQueryable.Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            res.Data = _tIQueryable.ToList();
            return res;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public GridDataResponse GetArticlePageList(ArticleSearchDto param)
        {
            var res = new GridDataResponse();
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = from a in _dbcontext.T_Article
                               join b in _dbcontext.T_ArticleCategory
                               on a.ClassifyId equals b.Id
                               where (param.ClassifyId.HasValue ? a.ClassifyId.Equals(param.ClassifyId.Value) : true)
                               && (!(param.Title == null || param.Title.Trim() == string.Empty) ? a.Title.Contains(param.Title) : true)
                               && (param.StartDate.HasValue ? a.CreatorTime >= param.StartDate.Value : true)
                               && (param.EndDate.HasValue ? a.CreatorTime < param.EndDate.Value : true)
                               select new ArticleDto
                               {
                                   ArticleId = a.Id,
                                   Title = a.Title,
                                   ClassifyName = b.CateName,
                                   CreatorAccount = a.CreatorAccount,
                                   CreatorTime = a.CreatorTime,
                                   Status = a.Status
                               };

            res.Count = _tIQueryable.Count();
            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, "CreatorTime", false);
            _tIQueryable = _tIQueryable.Skip((param.Page - 1) * param.Limit).Take(param.Limit);
            res.Data = _tIQueryable.ToList();
            return res;
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArticleDto GetArticleById(Guid id)
        {
            var _dbcontext = base.GetDbContext();
            var query = from a in _dbcontext.T_Article
                        join b in _dbcontext.T_ArticleCategory
                        on a.ClassifyId equals b.Id
                        where a.Id.Equals(id)
                        select new ArticleDto
                        {
                            ArticleId = a.Id,
                            Title = a.Title,
                            ClassifyId = a.ClassifyId,
                            ClassifyName = b.CateName,
                            Content = a.Content,
                            CreatorTime = a.CreatorTime,
                            Abstract = a.Abstract,
                            FilePath = a.FilePath
                        };
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 通过栏目分类下最新一条文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ArticleDto GetArticleByCateName(ArticleByNameSearchDto dto)
        {
            var _dbcontext = base.GetDbContext();
            var _tIQueryable = from a in _dbcontext.T_Article
                               where a.ClassifyId == dto.CategoryId
                               && a.Status == (int)ArticleStatusEnum.Publish
                               select new ArticleDto
                               {
                                   ArticleId = a.Id,
                                   Title = a.Title,
                                   FilePath = a.FilePath,
                                   Content = a.Content,
                                   CreatorAccount = a.CreatorAccount,
                                   CreatorTime = a.CreatorTime,
                                   Status = a.Status
                               };

            return _tIQueryable.OrderByDescending(t => t.CreatorTime).FirstOrDefault();
        }
    }
}
