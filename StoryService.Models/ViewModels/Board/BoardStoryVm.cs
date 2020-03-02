﻿using StoryService.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryService.Models.ViewModels.Board
{
    public class BoardStoryVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double StoryPoints { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public List<BoardTaskVm> Tasks { get; set; }

    }
}
