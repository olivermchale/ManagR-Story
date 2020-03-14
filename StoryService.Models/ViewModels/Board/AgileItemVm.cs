using StoryService.Models.Dtos;
using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels.Board
{
    public class AgileItemVm
    {
        public AgileItemVm(AgileItemDto item)
        {
            this.Id = item.Id;
            this.ParentId = item.ParentId;
            this.Title = item.Title;
            this.StoryPoints = item.StoryPoints;
            this.Priority = item.Priority;
            this.Status = item.Status;
            this.Order = item.Order;
        }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Title { get; set; }
        public double? StoryPoints { get; set; }
        public Priority Priority { get; set; }
        public int? Order { get; set; }
        public Status Status { get; set; }
        public List<AgileItemVm> AgileItems = new List<AgileItemVm>();

    }
}
