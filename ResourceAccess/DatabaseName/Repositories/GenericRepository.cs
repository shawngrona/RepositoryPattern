using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ResourceAccess.DatabaseName.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {

        internal System.Data.Entity.DbContext m_DBContext;
        internal DbSet<TEntity> m_DBSet;

        public GenericRepository(System.Data.Entity.DbContext context)
        {
            m_DBContext = context;
            m_DBSet = m_DBContext.Set<TEntity>();

        }


        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {

            IQueryable<TEntity> query = m_DBSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return orderBy(query).ToList();

            var result = query.ToList();

            return result;
        }

        public virtual TEntity GetByID(object id)
        {
            return m_DBSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            m_DBSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = m_DBSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void DeleteAll()
        {
            IEnumerable<TEntity> all = Get();
            foreach (TEntity ent in all)
            {
                Delete(ent);
            }


        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (m_DBContext.Entry(entityToDelete).State == EntityState.Detached)
                m_DBSet.Attach(entityToDelete);

            m_DBSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            m_DBSet.Attach(entityToUpdate);
            m_DBContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
