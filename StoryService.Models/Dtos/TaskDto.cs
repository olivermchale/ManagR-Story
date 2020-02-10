using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class TaskDto : TaskBaseDto
    {
        public int Order { get; set; }
        public Guid StoryId { get; set; }
        public StoryDto Story { get; set; }
    }
}
