using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels.Charts
{
    public class ChartVm
    {
        public string ChartTitle { get; set; }
        public ChartDataVm ChartData { get; set; }
        public ChartDataVm ChartComparisonData { get; set; }
        public List<string> DataLabels = new List<string>();
    }
}
