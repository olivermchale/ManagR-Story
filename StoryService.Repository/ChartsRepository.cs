using Microsoft.EntityFrameworkCore;
using StoryService.Data;
using StoryService.Models.Dtos;
using StoryService.Models.ViewModels.Charts;
using StoryService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Repository
{
    public class ChartsRepository : IChartsRepository
    {
        private StoryServiceDb _context;
        public ChartsRepository(StoryServiceDb context)
        {
            _context = context;
        }

        // called by the background service every 24 hours
        // not performance important, can take as long as it needs as work is done on inexpensive background thread.
        public async Task<bool> SaveDailyChartData()
        {
            try
            {
                var boardIds = await _context.Boards.Where(b => b.IsActive == true)
                     .Select(i => i.Id)
                     .ToListAsync();

                foreach (var boardId in boardIds)
                {
                    var loggedTimes = await _context.AgileItems.Where(s => s.BoardId == boardId && s.IsActive == true).Select(t => t.LoggedTime).ToListAsync();
                    int totalTime = 0;
                    foreach (var loggedTime in loggedTimes)
                    {
                        if (loggedTime != null)
                        {
                            totalTime += Convert.ToInt32(loggedTime);
                        }
                    }
                    var completeTasks = await _context.AgileItems.Where(s => s.BoardId == boardId == s.IsActive == true && s.Status == Models.Types.Status.Complete).CountAsync();

                    var dailyData = new DailyDataDto
                    {
                        Id = Guid.NewGuid(),
                        BoardId = boardId,
                        Date = DateTime.Now,
                        LoggedHours = totalTime,
                        TasksComplete = completeTasks,
                    };

                    _context.DailyData.Add(dailyData);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                // exception saving daily data
            }
            return false;
        }

        public async Task<ChartVm> GetBurndownChart(Guid id)
        {
            try
            {
                var board = await _context.Boards.Where(b => b.Id == id && b.IsActive == true).FirstOrDefaultAsync();

                var from = board.BoardStart;
                var to = board.BoardEnd;
                var totalDays = (to - from).Days < 7 ? (to - from).Days : 7;

                var taskData = new ChartDataVm();
                taskData.Label = "Actual Hours";
                List<string> dateLables = new List<string>();
                for (int i = 0; i < totalDays; i++)
                {
                   taskData.Data.Add(await _context.DailyData
                        .Where(e => e.Date.Date == DateTime.Now.AddDays(i * -1).Date && e.BoardId == id)
                        .Select(x => x.LoggedHours)
                        .FirstOrDefaultAsync());
                    dateLables.Add("Day " + (i + 1));
                }

                var timeList = await _context.AgileItems
                    .Where(c => c.IsActive && c.BoardId == id && c.AgileItemType == Models.Types.AgileItemType.Task)
                    .Select(x => Convert.ToInt32(x.EstimatedTime)).ToListAsync();
                var countTasks = 0;
                foreach (var time in timeList)
                {
                    countTasks += time;
                }
                
                var timePerDay = countTasks / totalDays;
                var estimatedTaskData = new ChartDataVm();
                estimatedTaskData.Label = "Estimated Hours";
                for(int i = 0; i < totalDays; i++)
                {
                    if(i != 0)
                    {
                        estimatedTaskData.Data.Add(estimatedTaskData.Data[i - 1] + timePerDay);
                    } else
                    {
                        estimatedTaskData.Data.Add(timePerDay);
                    }            
                }

                estimatedTaskData.Data.Reverse();

                return new ChartVm
                {
                    ChartData = taskData,
                    ChartComparisonData = estimatedTaskData,
                    ChartTitle = board.BoardName,
                    DataLabels = dateLables,
                };
            }
            catch (Exception e)
            {
                // exception getting burndown chart
            }
            return null;
        }
    }
}
