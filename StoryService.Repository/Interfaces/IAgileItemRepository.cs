using StoryService.Models.Dtos;
using StoryService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Repository.Interfaces
{
    public interface IAgileItemRepository
    {
        public Task<bool> CreateAgileItem(AgileItemDto agileItem);

        Task<bool> CreateStoryWithTasks(CreateStoryWithTasksDto storyAndTasks);

        Task<List<AgileItemShortVm>> SearchForAgileItem(SearchAgileItemDto search);
    }
}
