using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StoryService.Repository;
using StoryService.Repository.Interfaces;

namespace StoryService.Controllers
{
    [EnableCors("ManagRAppServices")]
    public class BoardController : Controller
    {
        public IBoardRepository _boardRepository;
        public BoardController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoard(Guid id)
        {
            var board = await _boardRepository.GetBoard(id);
            return Ok(board);
        }

        [HttpGet]
        public async Task<IActionResult> GetBoardNames()
        {
            var boards = await _boardRepository.GetBoardNames();
            return Ok(boards);
        }
    }
}