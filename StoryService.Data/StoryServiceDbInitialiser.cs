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
            if (context.AgileItems.Any())
            {
                // db is seeded
                return;
            }
            var boards = new List<BoardDto>
            {
                new BoardDto
                {
                    Id = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                    BoardName = "Hailzys Board",
                    IsActive = true,
                },
                new BoardDto
                {
                    Id = Guid.Parse("1f7f8649-8fd5-4346-ad7c-51c30fa96607"),
                    BoardName = "Rena's Board",
                    IsActive = true
                },
            };
            boards.ForEach(board =>
            {
                context.Boards.Add(board);
            });
            await context.SaveChangesAsync();

            var agileItems = new List<AgileItemDto>
            {
                // Super stories
                new AgileItemDto
                {
                    Id = Guid.Parse("44305f57-ff1a-4269-8348-ebb79326aa26"),
                    AssigneeId = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    AssigneeName = "Oliver M",
                    AgileItemType = Models.Types.AgileItemType.SuperStory,
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                    CreatedBy = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    CreatedOn = new DateTime(),
                    CustomLabel = "Moon",
                    Description = "Get to the moon",
                    DueBy = new DateTime().AddDays(1),
                    IsActive = true,
                    IsComplete = false,
                    Order = 1,
                    Priority = Models.Types.Priority.Low,
                    Status = Models.Types.Status.Pending,
                    Title = "NASA Mission 1"
                },
                // Stories
                new AgileItemDto
                {
                    Id = Guid.Parse("67d8260f-5920-4988-93f4-7c2e9f3546cb"),
                    ParentId = Guid.Parse("44305f57-ff1a-4269-8348-ebb79326aa26"),
                    AssigneeId = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    AssigneeName = "Oliver M",
                    AgileItemType = Models.Types.AgileItemType.Story,
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                    CreatedBy = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    CreatedOn = new DateTime(),
                    CustomLabel = "Moon",
                    Description = "Build a rocket ship",
                    DueBy = new DateTime().AddDays(1),
                    IsActive = true,
                    IsComplete = false,
                    Order = 2,
                    Priority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.InProgress,
                    Title = "Build rocket"
                },
                new AgileItemDto
                {
                    Id = Guid.Parse("ade18239-6cad-4d6d-b51a-d5a5cd84b0b0"),
                    ParentId = Guid.Parse("44305f57-ff1a-4269-8348-ebb79326aa26"),
                    AssigneeId = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    AssigneeName = "Oliver M",
                    AgileItemType = Models.Types.AgileItemType.Story,
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                    CreatedBy = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    CreatedOn = new DateTime(),
                    CustomLabel = "Moon",
                    Description = "Gather the supplies for the rocket ship",
                    DueBy = new DateTime().AddDays(1),
                    IsActive = true,
                    IsComplete = false,
                    Order = 2,
                    Priority = Models.Types.Priority.High,
                    Status = Models.Types.Status.Pending,
                    Title = "Gather supplies"
                },
                new AgileItemDto
                {
                    Id = Guid.Parse("0bf37bb9-ccdf-49e2-88b3-0207178ce927"),
                    ParentId = Guid.Parse("67d8260f-5920-4988-93f4-7c2e9f3546cb"),
                    AssigneeId = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    AssigneeName = "Oliver M",
                    AgileItemType = Models.Types.AgileItemType.Task,
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                    CreatedBy = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    CreatedOn = new DateTime(),
                    CustomLabel = "Moon",
                    Description = "Buy all rocket parts",
                    DueBy = new DateTime().AddDays(1),
                    EstimatedTime = 2,
                    LoggedTime = 1,
                    IsActive = true,
                    IsComplete = false,
                    Order = 2,
                    Priority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.InProgress,
                    Title = "Buy rocket parts"
                },
                new AgileItemDto
                {
                    Id = Guid.Parse("f2c170e1-8128-4446-9e8a-1ecfbff520fa"),
                    ParentId = Guid.Parse("67d8260f-5920-4988-93f4-7c2e9f3546cb"),
                    AssigneeId = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    AssigneeName = "Oliver M",
                    AgileItemType = Models.Types.AgileItemType.Task,
                    BoardId = Guid.Parse("dbb831c6-67da-4e92-bdc4-d2f748efad20"),
                    CreatedBy = Guid.Parse("aa6b576b-93f1-485e-87bc-2710e694a33a"),
                    CreatedOn = new DateTime(),
                    CustomLabel = "Moon",
                    Description = "Put together rocket parts",
                    DueBy = new DateTime().AddDays(1),
                    EstimatedTime = 5,
                    LoggedTime = 0,
                    IsActive = true,
                    IsComplete = false,
                    Order = 2,
                    Priority = Models.Types.Priority.Medium,
                    Status = Models.Types.Status.Pending,
                    Title = "Put parts together"
                },
            };
            agileItems.ForEach(agileItem =>
            {
                context.AgileItems.Add(agileItem);
            });
            await context.SaveChangesAsync();
        }
    }
}
