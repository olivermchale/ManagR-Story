using Microsoft.EntityFrameworkCore;
using StoryService.Data;
using StoryService.Models.Dtos;
using StoryService.Models.ViewModels;
using StoryService.Models.ViewModels.Board;
using StoryService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Repository
{
    public class AgileItemRepository : IAgileItemRepository
    {
        private StoryServiceDb _context;
        public AgileItemRepository(StoryServiceDb context)
        {
            _context = context;
        }

        public async Task<bool> CreateAgileItem(CreateAgileItemDto agileItem)
        {
            var fullItem = CreateServerSideAgileItem(agileItem);
            try
            {
                if (fullItem.AgileItemType != Models.Types.AgileItemType.SuperStory)
                {
                    var parent = await _context.AgileItems.Where(i => i.Id == fullItem.ParentId).FirstOrDefaultAsync();
                    var board = await _context.Boards.Where(b => b.Id == fullItem.BoardId).FirstOrDefaultAsync();

                    if (parent == null || board == null)
                    {
                        throw new Exception("Invalid parent Id");
                    }
                }

                await _context.AgileItems.AddAsync(fullItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                // Exception when creating a new agile item
            }
            return false;
        }

        public async Task<bool> CreateStoryWithTasks(CreateStoryWithTasksDto storyAndTasks)
        {
            try
            {
                // Create the story in DB first
                await _context.AgileItems.AddAsync(storyAndTasks.Story);
                // Now Add all tasks
                foreach (var task in storyAndTasks.Tasks)
                {
                    await _context.AddAsync(task);
                }
                // Now save all records to db
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                // Exception when creating story with tasks
            }
            return false;
        }

        public async Task<List<AgileItemShortVm>> SearchForAgileItem(SearchAgileItemDto search)
        {
            try
            {
                if (search.ItemType == Models.Types.AgileItemType.SuperStory)
                {
                    return await _context.AgileItems
                     .Where(a => a.AgileItemType == search.ItemType && a.BoardId == search.BoardId)
                     .Where(a => a.Title.Contains(search.SearchQuery)
                    || a.Description.Contains(search.SearchQuery)
                    || a.AssigneeName.Contains(search.SearchQuery))
                    .Select(i => new AgileItemShortVm()
                    {
                        Id = i.Id,
                        Title = i.Title
                    }).Take(5).ToListAsync();
                }
                else
                {
                    var items = await _context.AgileItems
                     .Where(a => a.AgileItemType == search.ItemType && a.BoardId == search.BoardId)
                     .Where(a => a.Title.Contains(search.SearchQuery)
                    || a.Description.Contains(search.SearchQuery)
                    || a.AssigneeName.Contains(search.SearchQuery))
                    .Select(i => new AgileItemShortVm()
                    {
                        Id = i.Id,
                        Title = i.Title
                    }).Take(5).ToListAsync();

                    foreach (var item in items)
                    {
                        item.Order = await _context.AgileItems
                            .Where(s => s.ParentId == item.Id)
                            .OrderByDescending(i => i.Order)
                            .Select(i => i.Order)
                            .FirstOrDefaultAsync();
                    }
                    return items;
                }
            }
            catch (Exception e)
            {
                // Exception when searching...
            }
            return null;
        }

        public AgileItemDto CreateServerSideAgileItem(CreateAgileItemDto agileItem)
        {
            // Create Dto in expected shape with some necessary server side generated properties such as createdOn
            return new AgileItemDto
            {
                AgileItemType = agileItem.ItemType,
                AssigneeId = agileItem.AssigneeId,
                AssigneeName = agileItem.AssigneeName,
                Description = agileItem.Description,
                BoardId = agileItem.Board,
                CreatedBy = agileItem.CreatedBy,
                CreatedOn = new DateTime(),
                DueBy = agileItem.DueBy,
                EstimatedTime = agileItem.EstimatedTime,
                IsActive = true,
                Id = Guid.NewGuid(),
                IsComplete = false,
                Order = agileItem.Order,
                ParentId = agileItem.ParentId,
                Priority = agileItem.Priority,
                Status = Models.Types.Status.Pending,
                StoryPoints = agileItem.StoryPoints,
                Title = agileItem.Title
            };
        }

        public async Task<bool> UpdateAgileItem(BoardTaskVm updatedTask)
        {
            try
            {
                var item = await _context.AgileItems.Where(a => a.Id == updatedTask.Id).FirstOrDefaultAsync();

                if (item != null)
                {
                    item.Title = updatedTask.Title;
                    item.Description = updatedTask.Description;
                    item.Order = updatedTask.Order;
                    item.Priority = updatedTask.Priority;
                    item.Status = updatedTask.Status;

                    await _context.SaveChangesAsync();
                    return true;
                }                
            }
            catch(Exception e)
            {
                // Error when updating agile item.
            }
            return false;
        }

        public async Task<AgileItemVm> GetFullAgileItem(Guid id)
        {
            try
            {
                var item = await _context.AgileItems.Where(a => a.Id == id && a.IsActive == true)
                    .Select(i => new AgileItemVm
                    {
                        AgileItemType = i.AgileItemType,
                        AssigneeName = i.AssigneeName,
                        BoardId = i.BoardId,
                        CreatedBy = i.CreatedBy,
                        CreatedOn = i.CreatedOn,
                        Description = i.Description,
                        DueBy = i.DueBy,
                        EstimatedTime = i.EstimatedTime,
                        Id = i.Id,
                        IsComplete = i.IsComplete,
                        LoggedTime = i.LoggedTime,
                        Order = i.Order,
                        ParentId = i.ParentId,
                        Priority = i.Priority,
                        Status = i.Status,
                        StoryPoints = i.StoryPoints,
                        Title = i.Title
                    }).FirstOrDefaultAsync();

                if (item != null)
                {
                    var parentTitle = await _context.AgileItems.Where(a => a.Id == item.ParentId)
                            .Select(x => x.Title).FirstOrDefaultAsync();

                    item.ParentTitle = parentTitle;

                    return item;
                }
            }
            catch (Exception e)
            {
                //exception when getting a full agile item...
            }
            return null;
        }
    }
}
