using Microsoft.EntityFrameworkCore;
using StoryService.Data;
using StoryService.Models.Dtos;
using StoryService.Models.ViewModels;
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
        public async Task<bool> CreateAgileItem(AgileItemDto agileItem)
        {
            try
            {
                await _context.AgileItems.AddAsync(agileItem);
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
                     .Where(a => a.AgileItemType == search.ItemType)
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
                     .Where(a => a.AgileItemType == search.ItemType)
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
    }
}
