using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data
{
    public class EventAppDataContextFactory : IDesignTimeDbContextFactory<EventAppDataContext>
    {
        public EventAppDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventAppDataContext>();
            optionsBuilder.UseSqlServer<EventAppDataContext>("Server=DESKTOP-4FMT0IA\\SQLEXPRESS;Database=internship;User Id=steven;Integrated Security=false;Trusted_Connection=false; password=password");

            return new EventAppDataContext(optionsBuilder.Options);
        }
    }
}
