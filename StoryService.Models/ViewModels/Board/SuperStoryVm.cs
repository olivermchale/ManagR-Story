using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels.Board
{
    public class SuperStoryVm
    {
        public Guid Id { get; set; }
        public List<StoryVm> Stories { get; set; }
        public string Title { get; set; }
    }
}
