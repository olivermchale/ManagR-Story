using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class StoryDto : TaskBaseDto
    {
        public Guid SuperStoryId { get; set; }
        public SuperStoryDto SuperStory { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}
