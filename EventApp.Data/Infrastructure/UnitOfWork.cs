using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventAppDataContext context;

        public UnitOfWork(EventAppDataContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
