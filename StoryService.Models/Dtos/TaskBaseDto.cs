using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class TaskBaseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public List<AssigneeDto> Assignees { get; set; }
        public DateTime DueBy { get; set; }
        public Priority Prority { get; set; }
        public Status Status { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public Guid BoardId { get; set; }
    }
}
