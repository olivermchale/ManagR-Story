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
        private Mapper _mapper;
        public BoardRepository(StoryServiceDb context)
        {
            _context = context;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskDto, BoardTaskVm>());
            _mapper = new Mapper(config);
        }
        public async Task<BoardVm> GetBoard(Guid boardId)
        {
            var stories = await _context.Stories
                .Where(s => s.BoardId == boardId && s.IsActive == true)
                .Include(x => x.Tasks)
                .Select(b => new BoardStoryVm
                {
                    Id = b.Id,
                    Priority = b.Prority,
                    Status = b.Status,
                    StoryPoints = b.StoryPoints,
                    Tasks = OrderTasksByStatus(_mapper.Map<List<BoardTaskVm>>(b.Tasks)),
                    Title = b.Title
                }).ToListAsync();

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
                Done = tasks.Where(s => s.Status == Models.Types.Status.Complete).ToList()
            };
        }
    }
}
