using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.StatisticsService.StatisticDtos;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services.StatisticsService
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository<EventType> eventTypeRepo;
        private readonly IUnitOfWork unitOfWork;

        public StatisticsService(IRepository<EventType> eventTypeRepo, IUnitOfWork unitOfWork)
        {
            this.eventTypeRepo = eventTypeRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<EventTypeStatictisDto>> GetEventTypeStas()
        {
            return await eventTypeRepo.Query().Include(e => e.Events)
                                      .Select(x => new EventTypeStatictisDto() { Name = x.Name, Count = x.Events.Count })
                                      .ToListAsync();

                                      
        }
    }
}
