using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class DailyDataDto
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public DateTime Date { get; set; }
        public int LoggedHours { get; set; }
        public int TasksComplete { get; set; }
    }
}
