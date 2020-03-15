using StoryService.Data;
using StoryService.Models.Dtos;
using StoryService.Repository.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}
