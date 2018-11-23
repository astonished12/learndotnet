using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace EventApp.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IEnumerable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> where);
    }
}
