using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public BoardRepository(StoryServiceDb context)
        {
            _context = context;
        }

        //public async Task<List<AgileItemVm>> GetBoard(Guid boardId)
        //{
        //    List<AgileItemVm> superStories = new List<AgileItemVm>();
        //    var allAgileItems = await _context.AgileItems
        //        .Where(s => s.IsActive == true)
        //        .Select(c => new AgileItemVm(c))
        //        .ToListAsync();

        //    if(allAgileItems == null)
        //    {
        //        return null;
        //    }

        //    var hashedAgileItems = allAgileItems.ToDictionary(x => x.Id, x => x);

        //    for (int i = 0; i < allAgileItems.Count; i++)
        //    {
        //        var agileItem = allAgileItems[i];
        //        if (hashedAgileItems.ContainsKey(agileItem.ParentId))
        //        {
        //            hashedAgileItems[agileItem.ParentId].AgileItems.Add(agileItem);
        //        }
        //        else
        //        {
        //            superStories.Add(agileItem);
        //        }
        //    }
        //    return superStories;
        //}

        public async Task<BoardVm> GetBoard(Guid BoardId)
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

            foreach(var story in stories)
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

        public static BoardTaskListVm OrderTasksByStatus(List<BoardTaskVm> tasks)
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
