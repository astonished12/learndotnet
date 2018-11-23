using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
