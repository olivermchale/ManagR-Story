using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StoryService.Models.Dtos;
using StoryService.Repository.Interfaces;

namespace StoryService.Controllers
{
    [EnableCors("ManagRAppServices")]
    public class AgileItemsController : Controller
    {
        private IAgileItemRepository _agileItemRepository;
        public AgileItemsController(IAgileItemRepository agileItemRepository)
        {
            _agileItemRepository = agileItemRepository;
        }
        public async Task<IActionResult> SearchAgileItems(SearchAgileItemDto search)
        {
            var items = await _agileItemRepository.SearchForAgileItem(search);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAgileItem([FromBody] CreateAgileItemDto agileItem)
        {
            var success = await _agileItemRepository.CreateAgileItem(agileItem);
            if(success)
            {
                return Ok(success);
            }
            return new StatusCodeResult(500); 
        }
    }
}