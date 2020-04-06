using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.Dtos
{
    public class BoardDto
    {
        public Guid Id { get; set; }
        public string BoardName { get; set; }
        public DateTime BoardStart { get; set; }
        public DateTime BoardEnd { get; set; }
        public bool IsActive { get; set; }
    }
}
