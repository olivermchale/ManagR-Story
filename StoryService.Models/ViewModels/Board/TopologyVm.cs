using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels.Board
{
    public class TopologyVm
    {
        public List<SuperStoryVm> SuperStories { get; set; }

        public string BoardTitle { get; set; }
        public int SuperStoryCount { get; set; }
        public int StoryCount { get; set; }
        public int TaskCount { get; set; }
    }
}
