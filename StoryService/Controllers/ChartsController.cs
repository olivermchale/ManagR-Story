using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryService.Repository.Interfaces;

namespace StoryService.Controllers
{
    [EnableCors("ManagRAppServices")]
    public class ChartsController : ControllerBase
    {
        private IChartsRepository _chartsRepository;
        public ChartsController(IChartsRepository chartsRepository)
        {
            _chartsRepository = chartsRepository;
        }

        [HttpGet] 
        public async Task<IActionResult> GetBurndownChart(Guid id)
        {
            return Ok(await _chartsRepository.GetBurndownChart(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProgressChart(Guid id)
        {
            return Ok(await _chartsRepository.GetCompleteTasksChart(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalytics(Guid id)
        {
            return Ok(await _chartsRepository.GetAnalytics(id));
        }
    }
}