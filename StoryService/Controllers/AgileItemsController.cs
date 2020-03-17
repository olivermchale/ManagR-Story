using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoryService.Models.Dtos;
using StoryService.Repository.Interfaces;

namespace StoryService.Controllers
{
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
    }
}