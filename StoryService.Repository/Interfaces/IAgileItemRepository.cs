using StoryService.Models.Dtos;
using StoryService.Models.ViewModels;
using StoryService.Models.ViewModels.Board;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Repository.Interfaces
{
    public interface IAgileItemRepository
    {
        public Task<bool> CreateAgileItem(CreateAgileItemDto agileItem);

        public Task<bool> CreateStoryWithTasks(CreateStoryWithTasksDto storyAndTasks);

        public Task<List<AgileItemShortVm>> SearchForAgileItem(SearchAgileItemDto search);

        public Task<bool> UpdateAgileItem(BoardTaskVm updatedTask);

        public Task<AgileItemVm> GetFullAgileItem(Guid id);
    }
}
