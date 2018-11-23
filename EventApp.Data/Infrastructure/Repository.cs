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
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> @where)
        {
            return dbSet.Where(where).ToList();
        }
    }
}
