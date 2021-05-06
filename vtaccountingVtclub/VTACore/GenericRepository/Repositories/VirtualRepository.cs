using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VTAworldpass.VTAServices.GenericRepository.Contract;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTACore.GenericRepository.Repositories
{
    public class VirtualRepository<T> : IGenericRepository<T> where T : class
    {
        vtclubdbEntities context = null;
        private DbSet<T> entities = null;

        public VirtualRepository(vtclubdbEntities context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = entities;
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


        public IEnumerable<T> GetAll()
        {
            return this.entities.ToList();
        }

        public T GetById(object id)
        {
            return this.entities.Find(id);
        }

        public void Insert(T model)
        {
            context.Entry(model).State = EntityState.Added;
        }

        public virtual void InsertMulti(List<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public void Update(T model)
        {
            context.Entry(model).State = EntityState.Modified;
        }

        //public void Delete(T id)
        //{
        //    T existing = this.entities.Find(id);
        //    this.entities.Remove(existing);
        //}

        public void Delete(object id)
        {
            T existing = this.entities.Find(id);
            this.entities.Remove(existing);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                entities.Attach(entityToDelete);
            }
            entities.Remove(entityToDelete);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = entities;
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
                Log.Error("No se puede realizar consulta asíncrona. ", e);
                throw new Exception("No se puede realizar consulta asíncrona.");
            }

        }



        public virtual async Task<int> DeleteAsync(T t)
        {
            context.Set<T>().Remove(t);
            return await context.SaveChangesAsync();
        }

        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                context.Entry(exist).CurrentValues.SetValues(t);
                await context.SaveChangesAsync();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsync(T t)
        {
            context.Set<T>().Attach(t);
            context.Entry(t).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual async Task<T> InsertAsync(T t)
        {
            context.Set<T>().Add(t);
            await context.SaveChangesAsync();
            return t;
        }
    }
}