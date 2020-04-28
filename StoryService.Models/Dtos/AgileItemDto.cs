using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class AgileItemDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime DueBy { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public AgileItemType AgileItemType { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
        public double? StoryPoints { get; set; }
        public double? EstimatedTime { get; set; }
        public double? LoggedTime { get; set; }
        public string? BlockedReason { get; set; }
        public int? Order { get; set; }
        public Guid AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public Guid BoardId { get; set; }
    }
}
