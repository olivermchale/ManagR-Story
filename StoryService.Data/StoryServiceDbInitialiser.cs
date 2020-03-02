using StoryService.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Data
{
    public class StoryServiceDbInitialiser
    {
        public static async Task SeedStubData(StoryServiceDb context, IServiceProvider services)
        {
            if (context.Stories.Any() && context.Tasks.Any() && context.SuperStories.Any())
            {
                // db is seeded
                return;
            }
            var superStories = new List<SuperStoryDto>
            {
                new SuperStoryDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Oliver",
                            Id = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "Complete this super story",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("c516eba1-7eb0-49e1-861d-608d47d4b8c5"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.High,
                    Status = Models.Types.Status.Pending,
                    Title = "Stub Super Story 1",
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                }
            };
            superStories.ForEach(superStory => context.SuperStories.Add(superStory));

            var stories = new List<StoryDto>
            {
                new StoryDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Kasper",
                            Id = Guid.Parse("e79769db-5e13-4532-94ca-b1efabc35ac8")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "Complete this story",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.Pending,
                    Title = "Stub Story 1",
                    CustomLabel = "Release 1",
                    StoryPoints = 1,
                    SuperStoryId = Guid.Parse("c516eba1-7eb0-49e1-861d-608d47d4b8c5"),
                    SuperStory = superStories.First(),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                }
            };
            stories.ForEach(story => context.Stories.Add(story));

            var tasks = new List<TaskDto>
            {
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("c83780a4-4151-43a1-989f-487b5394166b")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "Complete this task",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("9702098a-d6ef-4b10-88c9-10ea49315478"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.High,
                    Status = Models.Types.Status.Pending,
                    Title = "Stub Task 1",
                    Order = 1,
                    Story = stories.First(),
                    StoryId = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                }
            };
            tasks.ForEach(task => context.Tasks.Add(task));
            await context.SaveChangesAsync();
        }
    }
}
