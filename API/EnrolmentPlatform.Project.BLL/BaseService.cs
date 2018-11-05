using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using EnrolmentPlatform.Project.Domain;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.IDAL;
using System.Data;
using Autofac;

namespace EnrolmentPlatform.Project.BLL
{
    public abstract class BaseService<T> : IDisposable where T : Entity, new()
    {
        public BaseService()
        {
            this.DisposableObjects = new List<IDisposable>();
            this.SetCurrentRepository();
        }

        public IBaseRepository<T> CurrentRepository;

        /// <summary>
        /// 是否开启日志
        /// </summary>
        public bool LogChangesDuringSave { get; set; }
        /// <summary>
        /// 日志业务名称
        /// </summary>
        public string BusinessName { get; set; }

        public abstract bool SetCurrentRepository();

        public T FindEntityById(Guid Id, E_DbClassify dbClassify = E_DbClassify.Write, bool includDelete = false)
        {
            return this.CurrentRepository.FindEntityById(Id, dbClassify, includDelete);
        }
        public T FindEntityByIdToDB(Guid Id, E_DbClassify dbClassify = E_DbClassify.Write, bool includDelete = false)
        {
            return this.CurrentRepository.FindEntityByIdToDB(Id, dbClassify, includDelete);
        }
        /// <summary>
        /// 页面实体
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="whereLambda"></param>
        /// <param name="includDelete"></param>
        /// <returns>第一个为上一个 第二个为当前 第三个为下一个 为这置顶 置尾</returns>
        public virtual IList<T> FindPrevNextById(Guid Id,
            Expression<Func<T, bool>> whereLambda,
            E_DbClassify dbClassify = E_DbClassify.Write
            , bool includDelete = false)
        {
            return this.CurrentRepository.FindPrevNextById(Id, whereLambda, dbClassify, includDelete);
        }
        //查询
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write)
        {
            return this.CurrentRepository.LoadEntities(whereLambda, dbClassify);
        }

        public IQueryable<T> LoadPageEntities<S>(
            Expression<Func<T, bool>> whereLambada,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC,
            E_DbClassify dbClassify = E_DbClassify.Write
            )
        {
            return this.CurrentRepository.LoadPageEntities<S>(
                whereLambada,
                orderBy,
                pageSize,
                pageIndex,
                out totalCount,
                isASC,
                dbClassify
                );
        }
        public IQueryable<T> LoadPageEntitiesOrderByField(
            Expression<Func<T, bool>> whereLambada,
            string field,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC,
            E_DbClassify dbClassify = E_DbClassify.Write
         )
        {
            return this.CurrentRepository.LoadPageEntitiesOrderByField(
                whereLambada,
                field,
                pageSize,
                pageIndex,
                out totalCount,
                isASC,
                dbClassify
                );
        }

        //查询总数量
        public int Count(Expression<Func<T, bool>> predicate, E_DbClassify dbClassify = E_DbClassify.Write)
        {
            return this.CurrentRepository.Count(predicate, dbClassify);
        }

        //添加
        public int AddEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.AddEntity(entity, dbClassify, businessName, logChangesDuringSave, moduleKey);

        }

        //批量添加
        public int AddEntities(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.AddEntities(entities, dbClassify, businessName, logChangesDuringSave, moduleKey);

        }
        public int BulkInsert(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write)
        {
            return this.CurrentRepository.BulkInsert(entities, dbClassify);
        }
        public int BulkInsert(IList<T> entities, IDbTransaction transaction)
        {
            return this.CurrentRepository.BulkInsert(entities, transaction);
        }

        //删除
        public int PhysicsDeleteEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.PhysicsDeleteEntity(entity, dbClassify, businessName, logChangesDuringSave, moduleKey);

        }

        //批量删除
        public int PhysicsDeleteBy(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.PhysicsDeleteBy(whereLambda, dbClassify, businessName, logChangesDuringSave, moduleKey);

        }
        public int LogicDeleteEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.LogicDeleteEntity(entity, dbClassify, businessName, logChangesDuringSave, moduleKey);

        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public int LogicDeleteBy(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.LogicDeleteBy(whereLambda, dbClassify, businessName, logChangesDuringSave, moduleKey);
        }
        //更新
        public int UpdateEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.UpdateEntity(entity, dbClassify, businessName, logChangesDuringSave, moduleKey);
        }
        public int UpdateEntities(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            return this.CurrentRepository.UpdateEntities(entities, dbClassify, businessName, logChangesDuringSave, moduleKey);
        }
        public IQueryable<T> QuerySql(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras)
        {
            return this.CurrentRepository.QuerySql(sql, dbClassify, paras);
        }


        /// <summary>
        /// 执行存储过程或sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int ExecSql(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras)
        {
            return this.CurrentRepository.ExecSql(sql, dbClassify, paras);
        }
        public List<TResult> SqlQuery<TResult>(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras)
        {
            return this.CurrentRepository.SqlQuery<TResult>(sql, dbClassify, paras);
        }

        public IList<IDisposable> DisposableObjects { get; private set; }

        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        public void Dispose()
        {
            foreach (IDisposable obj in this.DisposableObjects)
            {
                if (obj != null)
                {
                    obj.Dispose();
                }
            }
        }
    }
}
