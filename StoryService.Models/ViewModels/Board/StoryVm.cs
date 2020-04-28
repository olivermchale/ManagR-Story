using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels.Board
{
    public class StoryVm
    {
        public Guid Id { get; set; }
        public List<TaskVm> Tasks { get; set; }
        public string Title { get; set; }
    }
}
