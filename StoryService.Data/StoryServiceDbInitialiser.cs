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
                    Description = "As a space flight operator, I want to get to the planet Mars so that we can determine the viability of human cohabitation.",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("c516eba1-7eb0-49e1-861d-608d47d4b8c5"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.High,
                    Status = Models.Types.Status.Pending,
                    Title = "Spacetravel to the planet Mars",
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
                    Description = "As an engineer, I want to build a rocket so that we can provide the astronauts with a way of travelling to Mars.",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.Pending,
                    Title = "Build the rocket",
                    CustomLabel = "Phase one",
                    StoryPoints = 1,
                    SuperStoryId = Guid.Parse("c516eba1-7eb0-49e1-861d-608d47d4b8c5"),
                    SuperStory = superStories.First(),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new StoryDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Kasper",
                            Id = Guid.Parse("fcfc205f-b4d6-4b12-a8a2-b4180426b626")
                        }
                    },
                    CreatedBy = Guid.Parse("8e5f300c-56c8-4e83-9adf-7847975243e1"),
                    CreatedOn = new DateTime(),
                    Description = "As an astronaut, I want to grow potatos as a way of sustaining food for life on mars.",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("f8ffc1f3-c920-4ea4-8b5c-5748ece3e3c3"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.Pending,
                    Title = "Grow potatos",
                    CustomLabel = "Phase one",
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
                    Description = "As an engineer, I want to assemble the rocket parts so that I can build the rocket",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("9702098a-d6ef-4b10-88c9-10ea49315478"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.High,
                    Status = Models.Types.Status.Pending,
                    Title = "Assemble rocket",
                    Order = 1,
                    Story = stories.First(),
                    StoryId = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("0ed4dae2-0282-4701-9a6a-b57470b4e7a1")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "As a software engineer, I want to finish creating rocket pilot software so that the assembled rocket can function",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("50af17b1-3849-4877-91bf-1aad49db5d2a"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.InProgress,
                    Title = "Finish rocket pilot software",
                    Order = 2,
                    Story = stories.First(),
                    StoryId = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("7bb9bc9b-5a20-4e5b-b8d2-4888fac67b50")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "As a parts co-ordinator, I want to buy all necessary rocket parts so that the team can build the rocket",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("ae3d1f51-99c4-4812-a6e0-4078ec1e2695"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Low,
                    Status = Models.Types.Status.Complete,
                    Title = "Buy rocket parts",
                    Order = 2,
                    Story = stories.First(),
                    StoryId = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("32585822-95ad-4bf3-9ab8-82e6e7859cbc")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "As a geologist, I want to scout out the rocket landing zone to determine the viability of landing the rocket",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("ee4bdc96-b448-428d-9074-63bc3a6726b1"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Low,
                    Status = Models.Types.Status.Blocked,
                    Title = "Scout landing zone",
                    Order = 2,
                    Story = stories.First(),
                    StoryId = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("dfbbffc4-2656-44cd-b86a-2993579a1c95")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "As a project manager, I want to hire astronauts so that we have skilled pilots for our rocket.",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("062850cc-85d3-4df2-bbfb-dc92ef0ad361"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.High,
                    Status = Models.Types.Status.Pending,
                    Title = "Hire astronauts",
                    Order = 2,
                    Story = stories.First(),
                    StoryId = Guid.Parse("5cafcc21-6ee1-4a2a-bac4-a678936bc682"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("5858fe72-5c36-4dc4-9e13-30f5e26e2eba")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "As an astronaut, I want to acquire potato seeds to grow potatos",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("3018c819-33b2-42c7-8b21-e8ef7be31d57"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.Pending,
                    Title = "Acquire potato seeds",
                    Order = 2,
                    Story = stories.Last(),
                    StoryId = Guid.Parse("f8ffc1f3-c920-4ea4-8b5c-5748ece3e3c3"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("913f4186-48c1-49c8-b6af-c42345f3d487")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "As an astronaut, I want to water to grow poatoes",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("f8db7f37-73c7-4700-ad8e-c8b2adeba94f"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.InProgress,
                    Title = "Figure out how to get water",
                    Order = 2,
                    Story = stories.Last(),
                    StoryId = Guid.Parse("f8ffc1f3-c920-4ea4-8b5c-5748ece3e3c3"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },
                new TaskDto
                {
                    Assignees = new List<AssigneeDto>
                    {
                        new AssigneeDto
                        {
                            DisplayName = "Rena",
                            Id = Guid.Parse("489636f4-2f8a-44fe-8691-38b2cb2432f9")
                        }
                    },
                    CreatedBy = Guid.Parse("85c0f676-20e4-45b9-b52d-150a42569d3b"),
                    CreatedOn = new DateTime(),
                    Description = "As an astronaut, I want to re-harvest potato seeds for sustainability",
                    DueBy = new DateTime().AddDays(1),
                    Id = Guid.Parse("006b9aae-b419-4a26-9e9f-1935425b1791"),
                    IsActive = true,
                    IsComplete = false,
                    Prority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.InProgress,
                    Title = "Figure out how to re-harvest potato seeds",
                    Order = 2,
                    Story = stories.Last(),
                    StoryId = Guid.Parse("f8ffc1f3-c920-4ea4-8b5c-5748ece3e3c3"),
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                },

            };
            tasks.ForEach(task => context.Tasks.Add(task));
            await context.SaveChangesAsync();
        }
    }
}
