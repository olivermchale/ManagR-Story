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

                if(board == null)
                {
                    return null;
                }

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
                taskData.Data.Reverse();

                return new ChartVm
                {
                    ChartData = taskData,
                    ChartComparisonData = estimatedTaskData,
                    ChartTitle = board.BoardName + " Burndown",
                    DataLabels = dateLables,
                };
            }
            catch (Exception e)
            {
                // exception getting burndown chart
            }
            return null;
        }

        public async Task<ChartVm> GetCompleteTasksChart(Guid id)
        {
            try
            {
                var board = await _context.Boards.Where(b => b.Id == id && b.IsActive == true).FirstOrDefaultAsync();

                if (board == null)
                {
                    return null;
                }

                var from = board.BoardStart;
                var to = board.BoardEnd;
                var totalDays = (to - from).Days < 7 ? (to - from).Days : 7;

                var taskData = new ChartDataVm();
                taskData.Label = "Actual Tasks Complete";
                List<string> dateLables = new List<string>();
                for (int i = 0; i < totalDays; i++)
                {
                    taskData.Data.Add(await _context.DailyData
                         .Where(e => e.Date.Date == DateTime.Now.AddDays(i * -1).Date && e.BoardId == id)
                         .Select(x => x.TasksComplete)
                         .FirstOrDefaultAsync());
                    dateLables.Add("Day " + (i + 1));
                }

                var countTasks = await _context.AgileItems
                    .Where(c => c.IsActive && c.BoardId == id && c.AgileItemType == Models.Types.AgileItemType.Task)
                    .CountAsync();

                var tasksPerDay = countTasks / totalDays;
                var estimatedTaskData = new ChartDataVm();
                estimatedTaskData.Label = "Estimated Tasks Complete";
                for (int i = 0; i < totalDays; i++)
                {
                    if (i != 0)
                    {
                        estimatedTaskData.Data.Add(estimatedTaskData.Data[i - 1] + tasksPerDay);
                    }
                    else
                    {
                        estimatedTaskData.Data.Add(tasksPerDay);
                    }
                }

                estimatedTaskData.Data.Reverse();
                taskData.Data.Reverse();

                return new ChartVm
                {
                    ChartData = taskData,
                    ChartComparisonData = estimatedTaskData,
                    ChartTitle = board.BoardName + " Progress",
                    DataLabels = dateLables,
                };
            }
            catch (Exception e)
            {
                // exception getting burndown chart
            }
            return null;
        }

        public async Task<AnalyticsVm> GetAnalytics(Guid id)
        {
            try
            {
                var totalStories = await _context.AgileItems.Where(s => s.IsActive == true && s.BoardId == id && s.AgileItemType == Models.Types.AgileItemType.Story)
                    .CountAsync();
                var completeStories = await _context.AgileItems.Where(s => s.IsActive == true && s.BoardId == id && s.Status == Models.Types.Status.Complete && s.AgileItemType == Models.Types.AgileItemType.Story)
                    .CountAsync(); ;

                var totalTasks = await _context.AgileItems.Where(s => s.IsActive == true && s.BoardId == id && s.AgileItemType == Models.Types.AgileItemType.Task)
                    .CountAsync();
                var completeTasks = await _context.AgileItems.Where(s => s.IsActive == true && s.BoardId == id && s.Status == Models.Types.Status.Complete && s.AgileItemType == Models.Types.AgileItemType.Task)
                    .CountAsync(); ;

                var estimatedHours = await _context.AgileItems.Where(s => s.IsActive == true && s.BoardId == id)
                    .Select(x => x.EstimatedTime)
                    .SumAsync();

                var loggedHours = await _context.AgileItems.Where(s => s.IsActive == true && s.BoardId == id)
                    .Select(x => x.LoggedTime)
                    .SumAsync();

                var board = await _context.Boards.Where(b => b.Id == id && b.IsActive == true).FirstOrDefaultAsync();
                if (board != null)
                {
                    return new AnalyticsVm
                    {
                        TotalStories = totalStories,
                        CompleteStories = completeStories,
                        TotalTasks = totalTasks,
                        CompleteTasks = completeTasks,
                        EstimatedHours = Convert.ToInt32(estimatedHours),
                        LoggedHours = Convert.ToInt32(loggedHours),
                        BoardTitle = board.BoardName,
                        StartDate = board.BoardStart,
                        EndDate = board.BoardEnd
                    };
                }


            }
            catch (Exception e)
            {
                // exception getting analytics
            }
            return null;


        }
    }
}
