using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class CreateStoryWithTasksDto
    {
        public AgileItemDto Story { get; set; }
        public List<AgileItemDto> Tasks { get; set; }
    }
}
