using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using EnrolmentPlatform.Project.Domain;
using EnrolmentPlatform.Project.Domain.EFContext;
using System.Data;

namespace EnrolmentPlatform.Project.IBLL
{
    public interface IBaseService<T> : IDisposable where T : Entity, new()
    {

        /// <summary>
        /// 查询单个实体ToCache
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        T FindEntityById(Guid Id, E_DbClassify dbClassify = E_DbClassify.Write, bool includDelete = false);
        /// <summary>
        /// 得到单个实体ToDB 应用在修改
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        T FindEntityByIdToDB(Guid Id, E_DbClassify dbClassify = E_DbClassify.Write, bool includDelete = false);
        /// <summary>
        /// 页面实体
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="whereLambda"></param>
        /// <param name="includDelete"></param>
        /// <returns>第一个为上一个 第二个为当前 第三个为下一个 为这置顶 置尾</returns>
        IList<T> FindPrevNextById(Guid Id, Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, bool includDelete = false);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="Wherelambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> wherelambda, E_DbClassify dbClassify = E_DbClassify.Write);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="wherelambda">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="pageSize">一页显示大小</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="totalCount">总记录条数</param>
        /// <param name="isASC">升序降序</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<S>(
            Expression<Func<T, bool>> wherelambda,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC,
            E_DbClassify dbClassify = E_DbClassify.Write
            );
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="wherelambda">条件表达式</param>
        /// <param name="field">字段名称</param>
        /// <param name="pageSize">一页显示大小</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="totalCount">总记录条数</param>
        /// <param name="isASC">升序降序</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntitiesOrderByField(
            Expression<Func<T, bool>> wherelambda,
            string field,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC,
            E_DbClassify dbClassify = E_DbClassify.Write
            );
        /// <summary>
        /// 得到总记录数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate, E_DbClassify dbClassify = E_DbClassify.Write);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int AddEntity(T Entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="Entities"></param>
        /// <returns></returns>
        int AddEntities(IList<T> Entities, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);
        int BulkInsert(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write);
        int BulkInsert(IList<T> entities, IDbTransaction transaction);
        
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int PhysicsDeleteEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);
        /// <summary>
        /// 批量物理删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        int PhysicsDeleteBy(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int LogicDeleteEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        int LogicDeleteBy(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);
        //更新
        int UpdateEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);
        int UpdateEntities(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null);

        IQueryable<T> QuerySql(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras);

        /// <summary>
        /// 执行存储过程或sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        int ExecSql(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras);
    }
}
