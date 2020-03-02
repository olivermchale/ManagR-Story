using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryService.Data;
using StoryService.Models.Dtos;
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
                    Tasks = _mapper.Map<List<BoardTaskVm>>(b.Tasks),
                    Title = b.Title
                }).ToListAsync();

            return new BoardVm
            {
                Stories = stories
            };
        }
    }
}
