using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels
{
    public class AgileItemShortVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? Order { get; set; }
    }
}
