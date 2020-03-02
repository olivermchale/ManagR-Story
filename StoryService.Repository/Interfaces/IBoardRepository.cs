using StoryService.Models.ViewModels.Board;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Repository.Interfaces
{
    public interface IBoardRepository
    {
        public Task<BoardVm> GetBoard(Guid boardId);
    }
}
