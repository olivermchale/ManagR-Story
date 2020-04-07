using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels.Charts
{
    public class AnalyticsVm
    {
        public int TotalStories { get; set; }
        public int CompleteStories { get; set; }
        public int TotalTasks { get; set; }
        public int CompleteTasks { get; set; }
        public int EstimatedHours { get; set; }
        public int LoggedHours { get; set; }
        public string BoardTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
