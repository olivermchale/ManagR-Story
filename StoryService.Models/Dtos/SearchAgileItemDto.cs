using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class SearchAgileItemDto
    {
        public AgileItemType ItemType { get; set; }
        public string SearchQuery { get; set; }
    }
}
