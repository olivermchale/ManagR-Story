using StoryService.Models.ViewModels.Charts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Repository.Interfaces
{
    public interface IChartsRepository
    {
        public Task<ChartVm> GetBurndownChart(Guid id);
        public Task<bool> SaveDailyChartData();
    }
}
