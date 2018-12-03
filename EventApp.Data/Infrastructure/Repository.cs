using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(EventAppDataContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<T> Query()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    
    }
}
