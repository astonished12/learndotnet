using EventApp.Services.StatisticsService.StatisticDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<List<EventTypeStatictisDto>> GetEventTypeStas();
    }
}
