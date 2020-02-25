using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ERPApi.DAL
{
    /// <summary>Універсальний репозиторій для роботи з БД</summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal ProjectsDBContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ProjectsDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        /// <summary>Повертає запит типу TEntity</summary>
        /// <param name="filter">Параметр фільтрації</param>
        /// <param name="orderBy">Параметр сортування</param>
        public virtual IQueryable<TEntity> Get(
           IEnumerable< Expression<Func<TEntity, bool>>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = null;

            if (filter != null)
            {
                foreach (var item in filter)
                {
                    query = dbSet.Where(item);
                }
               
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query == null ? dbSet.Include(includeProperty) : query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return query == null ? orderBy(dbSet) : orderBy(query);
            }
            else
            {
                return query; 
            }
        }
        /// <summary>
        /// Пощук об'єкту в БД
        /// </summary>
        /// <param name="id">id об'єкту</param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        /// <summary>
        /// Вставка нового елементу
        /// </summary>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        /// <summary>Видалення об'єкту з БД</summary>
        /// <param name="id">id - об'єкту</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}