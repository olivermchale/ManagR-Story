using StoryService.Models.ViewModels.Board;
using System.Collections.Generic;

namespace StoryService.Models.ViewModels
{
    public class BoardTaskListVm
    {
        public List<BoardTaskVm> Todo { get; set; }
        public List<BoardTaskVm> InProgress { get; set; }
        public List<BoardTaskVm> Done { get; set; }
    }
}
