using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using EnrolmentPlatform.Project.Domain;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.Infrastructure;
using EntityFramework.Extensions;
using EntityFramework.BulkInsert;
using EntityFramework.BulkInsert.Extensions;
using System.Data;
using System.Data.Entity.Infrastructure;
namespace EnrolmentPlatform.Project.DAL
{
    public class BaseRepository<T> : IDisposable where T : Entity, new()
    {
 
        public virtual T FindEntityById(
            Guid Id,
            E_DbClassify dbClassify = E_DbClassify.Write,
            bool includDelete = false
            )
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            T result = _db.Set<T>().AsNoTracking().FirstOrDefault(it => it.Id == Id && (!includDelete ? it.IsDelete == false : true));
            return result;
        }

        protected EnrolmentPlatformDbContext GetDbContext(E_DbClassify dbClassify = E_DbClassify.Write)
        {
            IDbContextFactory dbContextFactory = DIContainer.Resolve<IDbContextFactory>();
            dbContextFactory.DbClassify = dbClassify;
            EnrolmentPlatformDbContext _db = dbContextFactory.GetCurrentThreadInstance();
            return _db;
        }
        public virtual T FindEntityByIdToDB(Guid Id, E_DbClassify dbClassify = E_DbClassify.Write, bool includDelete = false)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            T result = _db.Set<T>().AsNoTracking().FirstOrDefault(it => it.Id == Id && (!includDelete ? it.IsDelete == false : true));
            return result;
        }
        //查询
        public virtual IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            IQueryable<T> result = _db.Set<T>().AsNoTracking().Where(whereLambda);
            return result;
        }
        /// <summary>
        /// 页面实体
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="whereLambda"></param>
        /// <param name="includDelete"></param>
        /// <returns>第一个为上一个 第二个为当前 第三个为下一个 为这置顶 置尾</returns>
        public virtual IList<T> FindPrevNextById(Guid Id, Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, bool includDelete = false)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);

            IList<T> result = new List<T>();
            T current = FindEntityById(Id, dbClassify, includDelete);
            var data = _db.Set<T>().AsNoTracking();
            //上一个
            T prev = data.OrderBy(it => it.CreatorTime).Where(it => it.CreatorTime > current.CreatorTime && (!includDelete ? it.IsDelete == false : true)).Where(whereLambda).FirstOrDefault();
            //下一个 
            T next = data.OrderByDescending(it => it.CreatorTime).Where(it => it.CreatorTime < current.CreatorTime && (!includDelete ? it.IsDelete == false : true)).Where(whereLambda).FirstOrDefault();
            result.Add(prev);
            result.Add(current);
            result.Add(next);
            return result;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="wherelambda"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="isASC"></param>
        /// <returns></returns>
        public virtual IQueryable<T> LoadPageEntities<S>(
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC,
            E_DbClassify dbClassify = E_DbClassify.Write
            )
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            IQueryable<T> _tIQueryable = _db.Set<T>().AsNoTracking().Where(whereLambda);
            totalCount = _tIQueryable.Count();
            if (isASC)
            {
                _tIQueryable = _tIQueryable.OrderBy(orderBy);
            }
            else
            {
                _tIQueryable = _tIQueryable.OrderByDescending(orderBy);
            }
            _tIQueryable = _tIQueryable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return _tIQueryable;
        }

        public virtual IQueryable<T> LoadPageEntitiesOrderByField(
            Expression<Func<T, bool>> whereLambda,
            string field,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC,
            E_DbClassify dbClassify = E_DbClassify.Write
            )
        {
            field = field.IsEmpty() ? "Unix" : field;

            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            IQueryable<T> _tIQueryable = _db.Set<T>().AsNoTracking().Where(whereLambda);
            totalCount = _tIQueryable.Count();
            _tIQueryable = ExtLinq.ApplyOrder(_tIQueryable, field, isASC);
            _tIQueryable = _tIQueryable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return _tIQueryable;
        }

        /// <summary>
        /// 动态获取如model对象中的XX对象的XX属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private object GetValue<TM>(TM model, string field)
        {
            if (field.Split('.').Length == 1)
                return model.GetType().GetProperty(field).GetValue(model, null);

            int index = field.IndexOf('.');
            string f1 = field.Substring(0, index);
            string f2 = field.Substring(index + 1);
            object obj = model.GetType().GetProperty(f1).GetValue(model, null);
            return GetValue(obj, f2);
        }
        //查询总数量
        public virtual int Count(Expression<Func<T, bool>> predicate, E_DbClassify dbClassify = E_DbClassify.Write)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            return _db.Set<T>().AsNoTracking().Where(predicate).Count();
        }

        //添加
        public virtual int AddEntity(
            T entity,
            E_DbClassify dbClassify = E_DbClassify.Write,
            string businessName = "",
            bool logChangesDuringSave = true,
            string moduleKey = null
            )
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            int result = 0;
            #region 初始化值
            if (entity.CreatorTime.Equals(DateTime.MinValue))
            {
                entity.CreatorTime = DateTime.Now;
            }
            entity.Unix = DateTime.Now.ConvertDateTimeInt();
            //entity.IsDelete = false;
            entity.LastModifyTime = DateTime.Now;
            entity.LastModifyUserId = Guid.Empty;
            entity.DeleteTime = DateTime.MaxValue;
            entity.DeleteUserId = Guid.Empty;
            #endregion

            _db.Set<T>().Attach(entity);
            _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
            _db.Entry(entity).State = EntityState.Added;
            _db.LogChangesDuringSave = logChangesDuringSave;
            _db.BusinessName = businessName;

            result = _db.SaveChanges();
           
            return result;
        }
        /// <summary>
        /// 批量添加 每10条记录提交一次
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int AddEntities(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            int result = 0;
            _db.LogChangesDuringSave = logChangesDuringSave;
            _db.BusinessName = businessName;
            _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
            for (int i = 0; i < entities.Count(); i++)
            {
                #region 初始化值
                entities[i].CreatorTime = DateTime.Now;
                entities[i].Unix = DateTime.Now.ConvertDateTimeInt();
                entities[i].IsDelete = false;
                entities[i].LastModifyTime = DateTime.Now;
                entities[i].LastModifyUserId = Guid.Empty;
                entities[i].DeleteTime = DateTime.MaxValue;
                entities[i].DeleteUserId = Guid.Empty;
                #endregion
                              
            }
            _db.Set<T>().AddRange(entities);
            result += _db.SaveChanges();
            return result;
        }
        /// <summary>
        /// 批量新增 不能使用事务
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="dbClassify"></param>
        /// <param name="businessName"></param>
        /// <param name="logChangesDuringSave"></param>
        /// <param name="moduleKey"></param>
        /// <returns></returns>
        public virtual int BulkInsert(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            int result = 0;
            for (int i = 0; i < entities.Count(); i++)
            {
               
                #region 初始化值
                entities[i].CreatorTime = DateTime.Now;
                entities[i].Unix = DateTime.Now.ConvertDateTimeInt();
                entities[i].IsDelete = false;
                entities[i].LastModifyTime = DateTime.Now;
                entities[i].LastModifyUserId = Guid.Empty;
                entities[i].DeleteTime = DateTime.Now;
                entities[i].DeleteUserId = Guid.Empty;
                #endregion
               
            }
            _db.BulkInsert<T>(entities);
            result += _db.SaveChanges();
            return result;
        }
        /// <summary>
        /// 批量插入 使用事务  _dbContextFactory.GetCurrentThreadInstance().Database.BeginTransaction()
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual int BulkInsert(IList<T> entities, IDbTransaction transaction)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(E_DbClassify.Write);
            int result = 0;
            for (int i = 0; i < entities.Count(); i++)
            {

                #region 初始化值
                entities[i].CreatorTime = DateTime.Now;
                entities[i].Unix = DateTime.Now.ConvertDateTimeInt();
                entities[i].IsDelete = false;
                entities[i].LastModifyTime = DateTime.Now;
                entities[i].LastModifyUserId = Guid.Empty;
                entities[i].DeleteTime = DateTime.Now;
                entities[i].DeleteUserId = Guid.Empty;
                #endregion

            }
            _db.BulkInsert<T>(entities, transaction);
            result += _db.SaveChanges();
            return result;
        }
        //物理删除
        public virtual int PhysicsDeleteEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            int result = 0;
            _db.LogChangesDuringSave = logChangesDuringSave;
            _db.BusinessName = businessName;
            _db.Set<T>().Attach(entity);
            _db.Entry(entity).State = EntityState.Deleted;
            _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
            result = _db.SaveChanges();
            if (result > 0)
            {
                result++;
            }
            return result;
        }

        //批量物理删除
        public virtual int PhysicsDeleteBy(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            var entitiesToDelete = _db.Set<T>().Where(whereLambda);
            _db.LogChangesDuringSave = logChangesDuringSave;
            _db.BusinessName = businessName;
            _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
            int n = entitiesToDelete.Delete();
            return n;
        }

        //逻辑删除
        public virtual int LogicDeleteEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            int result = 0;
            _db.LogChangesDuringSave = logChangesDuringSave;
            _db.BusinessName = businessName;
            var model = _db.Set<T>().FirstOrDefault(it => it.Id == entity.Id);
            #region 初始化值
            model.DeleteTime = DateTime.Now;
            model.IsDelete = true;
            #endregion
            _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
            result = _db.SaveChanges();
            return result;
        }
        //批量逻辑删除
        public virtual int LogicDeleteBy(Expression<Func<T, bool>> whereLambda, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            var entitiesToDelete = _db.Set<T>().Where(whereLambda);
            _db.LogChangesDuringSave = logChangesDuringSave;
            _db.BusinessName = businessName;
            _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
            DateTime dt = DateTime.Now;
            int n = entitiesToDelete.Update(t => new T { IsDelete = true, DeleteTime = dt });
            return n;
        }

        //更新
        public virtual int UpdateEntity(T entity, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            if (entity != null)
            {
                _db.LogChangesDuringSave = logChangesDuringSave;
                _db.BusinessName = businessName;
                #region 初始化值
                entity.LastModifyTime = DateTime.Now;

                #endregion

                RemoveHoldingEntityInContext(entity);
                _db.Set<T>().Attach(entity);
                _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
                _db.Entry(entity).State = EntityState.Modified;
            
            }
            return _db.SaveChanges();
        }

        //批量更新 每10条记录更新一次
        public virtual int UpdateEntities(IList<T> entities, E_DbClassify dbClassify = E_DbClassify.Write, string businessName = "", bool logChangesDuringSave = true, string moduleKey = null)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            int result = 0;
            _db.LogChangesDuringSave = logChangesDuringSave;
            _db.BusinessName = businessName;
            _db.ModuleKey = moduleKey ?? Guid.Empty.ToString();
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] == null) continue;
                _db.Entry(entities[i]).State = EntityState.Modified;
              
            }
            result += _db.SaveChanges();
            return result;
        }
        public virtual IQueryable<T> QuerySql(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            return _db.Database.SqlQuery<T>(sql, paras).AsQueryable();
        }

        /// <summary>
        /// 执行存储过程或sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public virtual int ExecSql(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            return _db.Database.ExecuteSqlCommand(sql, paras);
        }
        public virtual List<TResult> SqlQuery<TResult>(string sql, E_DbClassify dbClassify = E_DbClassify.Write, params SqlParameter[] paras)
        {
            EnrolmentPlatformDbContext _db = GetDbContext(dbClassify);
            return _db.Database.SqlQuery<TResult>(sql, paras).ToList();
        }

        private Boolean RemoveHoldingEntityInContext(T entity, E_DbClassify dbClassify = E_DbClassify.Write)
        {
            var objContext = ((IObjectContextAdapter)GetDbContext(dbClassify)).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }
        public void Dispose()
        {

        }
    }
}
