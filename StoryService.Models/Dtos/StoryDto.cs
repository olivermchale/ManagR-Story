using StoryService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class StoryDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public List<Guid> Assignees { get; set; }
        public DateTime DueBy { get; set; }
        public Guid SuperStoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
