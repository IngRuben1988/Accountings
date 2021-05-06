using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTACore.GenericRepository.Repositories
{
    public class AsyncsRepository<TEntity> where TEntity : class
    {
        // GEtting context from npa.context()
        protected vtclubdbEntities context;

        internal DbSet<TEntity> dbSet;

        public AsyncsRepository(vtclubdbEntities _context)
        {
            this.context = _context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }

            }
            catch (Exception e)
            {
                Log.Error("No se puede realizar la consulta", e);
                throw new Exception("No se puede realizar la consulta", e);
            }


        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }


        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertLog(TEntity entity)
        {
            dbSet.Add(entity);
        }

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

        /********************** ASYNCHRONUS METHODS ***********************/

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;// context.Set<TEntity>();
                // context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error("Get Async -> No se puede realizar consulta asíncrona. ", e.InnerException);
                throw new Exception("No se puede realizar consulta asíncrona.");
            }

        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {

            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity t)
        {
            context.Set<TEntity>().Add(t);
            await context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().Where(match).ToListAsync();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity t, object key)
        {
            if (t == null)
                return null;
            TEntity exist = await context.Set<TEntity>().FindAsync(key);
            if (exist != null)
            {
                context.Entry(exist).CurrentValues.SetValues(t);
                await context.SaveChangesAsync();
            }
            return exist;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity t)
        {
            //context.Set<TEntity>().Add(t);

            context.Set<TEntity>().Attach(t);
            context.Entry(t).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return t;

        }

        public async Task<int> CountAsync()
        {
            return await context.Set<TEntity>().CountAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        //public static implicit operator AsyncsRepository<TEntity>(VirtualRepository<tblattachments> v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}