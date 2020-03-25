using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class CreateAgileItemDto
    {
        public AgileItemType ItemType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueBy { get; set; }
        public Priority Priority { get; set; }
        public Guid Board { get; set; }
        public Guid ParentId { get; set; }
        public Guid AssigneeId { get; set; }
        public Guid CreatedBy { get; set; }
        public string AssigneeName { get; set; }
        public int? StoryPoints { get; set; }
        public int? Order { get; set; }
        public int? EstimatedTime { get; set; }

    }
}
