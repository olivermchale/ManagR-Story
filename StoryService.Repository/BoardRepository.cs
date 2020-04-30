using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoryService.Data;
using StoryService.Models.Dtos;
using StoryService.Models.ViewModels;
using StoryService.Models.ViewModels.Board;
using StoryService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryService.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private StoryServiceDb _context;
        private ILogger<BoardRepository> _logger;
        public BoardRepository(StoryServiceDb context, ILogger<BoardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BoardVm> GetBoard(Guid BoardId)
        {
            try
            {
                var stories = await _context.AgileItems
                .Where(item => item.BoardId == BoardId && item.AgileItemType == Models.Types.AgileItemType.Story && item.IsActive == true)
                .Select(b => new BoardStoryVm
                {
                    Id = b.Id,
                    Priority = b.Priority,
                    Status = b.Status,
                    StoryPoints = b.StoryPoints,
                    AssigneeId = b.AssigneeId,
                    AssigneeName = b.AssigneeName,
                    Title = b.Title,
                }).ToListAsync();

                foreach (var story in stories)
                {
                    var tasks = await _context.AgileItems
                                .Where(item => item.ParentId == story.Id)
                                .Select(t => new BoardTaskVm
                                {
                                    AssigneeId = t.AssigneeId,
                                    AssigneeName = t.AssigneeName,
                                    Description = t.Description,
                                    Id = t.Id,
                                    Order = t.Order,
                                    Priority = t.Priority,
                                    Status = t.Status,
                                    Title = t.Title
                                }).ToListAsync();
                    story.Tasks = OrderTasksByStatus(tasks);
                }

                return new BoardVm
                {
                    Stories = stories
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Exception when getting board, Exception:" + e + "Stack trace:" + e.StackTrace, "board: " + BoardId);
            }
            return null;


        }

        public async Task<TopologyVm> GetBoardTopology (Guid BoardId)
        {
            try
            {
                var superStoryCount = 0;
                var storyCount = 0;
                var taskCount = 0;
                var superStories = await _context.AgileItems
                    .Where(item => item.BoardId == BoardId && item.AgileItemType == Models.Types.AgileItemType.SuperStory && item.IsActive == true)
                    .Select(b => new SuperStoryVm
                    {
                        Id = b.Id,
                        Title = b.Title,
                    }).ToListAsync();

                superStoryCount = superStories.Count;

                foreach (var superStory in superStories)
                {
                    var stories = await _context.AgileItems
                                .Where(item => item.ParentId == superStory.Id)
                                .Select(t => new StoryVm
                                {
                                    Id = t.Id,
                                    Title = t.Title
                                }).ToListAsync();
                    storyCount += stories.Count;

                    foreach (var story in stories)
                    {
                        var tasks = await _context.AgileItems
                            .Where(item => item.ParentId == story.Id)
                                .Select(t => new TaskVm
                                {
                                    Id = t.Id,
                                    Title = t.Title
                                }).ToListAsync();
                        taskCount += tasks.Count;
                        story.Tasks = tasks;
                    }
                    superStory.Stories = stories;
                }

                var boardTitle = await _context.Boards.Where(b => b.Id == BoardId).Select(b => b.BoardName).FirstOrDefaultAsync();

                return new TopologyVm
                {
                    SuperStories = superStories,
                    BoardTitle = boardTitle,
                    StoryCount = storyCount,
                    SuperStoryCount = superStoryCount,
                    TaskCount = taskCount
                };
            }

            catch(Exception e)
            {
                _logger.LogError("Exception when getting board, Exception:" + e + "Stack trace:" + e.StackTrace, "board: " + BoardId);
            }
            return null;
        }

        public async Task<BoardNameListVm> GetBoardNames()
        {
            var boardNames = await _context.Boards
                .Where(b => b.IsActive == true)
                .Select(b => new BoardNameVm
                {
                    BoardName = b.BoardName,
                    Id = b.Id
                }).ToListAsync();

            return new BoardNameListVm
            {
                boardNames = boardNames
            };
        }

        private static BoardTaskListVm OrderTasksByStatus(List<BoardTaskVm> tasks)
        {
            return new BoardTaskListVm
            {
                Todo = tasks.Where(s => s.Status == Models.Types.Status.Pending).ToList(),
                InProgress = tasks.Where(s => s.Status == Models.Types.Status.InProgress).ToList(),
                Done = tasks.Where(s => s.Status == Models.Types.Status.Complete).ToList(),
                Blocked = tasks.Where(s => s.Status == Models.Types.Status.Blocked).ToList()
            };
        }
    }
}
