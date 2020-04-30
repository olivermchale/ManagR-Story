using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StoryService.Data;
using StoryService.Models.Dtos;
using StoryService.Models.ViewModels.Board;
using StoryService.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace StoryService.Repository.Tests
{
    public class AgileItemRepositoryTests
    {
        private IAgileItemRepository _agileItemRepository;
        private Mock<ILogger<AgileItemRepository>> _mockLogger;
        private CreateAgileItemDto _stubCreateAgileItemDto;
        private AgileItemDto _stubSuperStoryDto;
        private AgileItemDto _stubStoryDto;
        private BoardDto _stubBoard;

        [SetUp]
        public void Setup()
        {
            _stubCreateAgileItemDto = new CreateAgileItemDto
            {
                AssigneeId = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                AssigneeName = "Oli",
                Board = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f43408a1"),
                CreatedBy = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                Description = "Stub create",
                DueBy = DateTime.Now.AddHours(1),
                EstimatedTime = 1,
                ItemType = Models.Types.AgileItemType.Story,
                Order = null,
                ParentId = Guid.Parse("5eb09b45-9c70-4465-b62d-535e28b16aed"),
                Priority = Models.Types.Priority.High,
                StoryPoints = 3,
                Title = "A stub creation of a story"
            };
            _stubSuperStoryDto = new AgileItemDto
            {
                Id = Guid.Parse("5eb09b45-9c70-4465-b62d-535e28b16aed"),
                AssigneeId = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                AssigneeName = "Oli",
                CreatedBy = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                Description = "Stub Super Story",
                DueBy = DateTime.Now.AddHours(1),
                EstimatedTime = 1,
                AgileItemType = Models.Types.AgileItemType.SuperStory,
                Order = null,
                Priority = Models.Types.Priority.High,
                Title = "A stub creation of a story",
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsComplete = false,
                Status = Models.Types.Status.InProgress,
                BoardId = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f43408a1"),
            };
            _stubStoryDto = new AgileItemDto
            {
                Id = Guid.Parse("5eb09b45-9c70-4465-b62d-535e28b16aee"),
                AssigneeId = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                AssigneeName = "Oli",
                CreatedBy = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                Description = "Stub Story",
                DueBy = DateTime.Now.AddHours(1),
                EstimatedTime = 1,
                AgileItemType = Models.Types.AgileItemType.Story,
                Order = null,
                Priority = Models.Types.Priority.High,
                Title = "A stub creation of a story",
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsComplete = false,
                Status = Models.Types.Status.InProgress,
                ParentId = Guid.Parse("5eb09b45-9c70-4465-b62d-535e28b16aed"),
                BoardId = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f43408a1"),
            };
            _stubBoard = new BoardDto
            {
                Id = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f43408a1"),
                BoardStart = DateTime.Now,
                BoardEnd = DateTime.Now.AddDays(7),
                BoardName = "Stub board",
                IsActive = true
            };
            _mockLogger = new Mock<ILogger<AgileItemRepository>>();
            _agileItemRepository = new AgileItemRepository(GetInMemoryContextWithSeedData(), _mockLogger.Object);
        }

        private StoryServiceDb GetInMemoryContextWithSeedData()
        {
            var options = new DbContextOptionsBuilder<StoryServiceDb>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;
            var context = new StoryServiceDb(options);

            context.Add(_stubBoard);
            context.Add(_stubSuperStoryDto);
            context.Add(_stubStoryDto);
            context.SaveChanges();

            return context;
        }

        [Test]
        public async Task CreateItem_Valid_Success()
        {
            // Arrange & Act
            var success = await _agileItemRepository.CreateAgileItem(_stubCreateAgileItemDto);

            // Assert
            Assert.IsNotNull(success);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task CreateItem_Invalid_Fails()
        {
            // Arrange
            var stubInvalidItem = _stubCreateAgileItemDto;
            _stubCreateAgileItemDto.ParentId = Guid.NewGuid();

            // Act
            var success = await _agileItemRepository.CreateAgileItem(stubInvalidItem);

            // Assert
            Assert.IsNotNull(success);
            Assert.IsFalse(success);
        }

        [Test]
        public async Task SearchForItem_Valid_Success()
        {
            // Arrange
            var search = new SearchAgileItemDto
            {
                BoardId = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f43408a1"),
                ItemType = Models.Types.AgileItemType.SuperStory,
                SearchQuery = "stub"
            };

            // Act
            var items = await _agileItemRepository.SearchForAgileItem(search);

            Assert.IsNotNull(items);
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("A stub creation of a story", items[0].Title);
        }

        [Test]
        public async Task SearchForItem_Invalid_Null()
        {
            // Arrange
            var search = new SearchAgileItemDto
            {
                BoardId = Guid.NewGuid(),
                ItemType = Models.Types.AgileItemType.SuperStory,
                SearchQuery = "z"
            };

            // Act
            var items = await _agileItemRepository.SearchForAgileItem(search);

            Assert.IsEmpty(items);
        }

        [Test]
        public async Task UpdateAgileItem_Valid_Success()
        {
            // Arrange
            var updatedItem = new BoardTaskVm() 
            {
                Id = Guid.Parse("5eb09b45-9c70-4465-b62d-535e28b16aed"),
                AssigneeId = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                AssigneeName = "Oli",
                Description = "Updated",
                Order = 3,
                Priority = Models.Types.Priority.Low,
                Status = Models.Types.Status.Complete,
                Title = "Updated title"
            };

            // Act
            var success = await _agileItemRepository.UpdateAgileItem(updatedItem);

            // Assert
            Assert.IsNotNull(success);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task UpdateAgileItem_Invalid_Failure()
        {
            // Arrange
            var updatedItem = new BoardTaskVm()
            {
                Id = Guid.NewGuid(),
                AssigneeId = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                AssigneeName = "Oli",
                Description = "Updated",
                Order = 3,
                Priority = Models.Types.Priority.Low,
                Status = Models.Types.Status.Complete,
                Title = "Updated title"
            };

            // Act
            var success = await _agileItemRepository.UpdateAgileItem(updatedItem);

            // Assert
            Assert.IsNotNull(success);
            Assert.IsFalse(success);
        }

        [Test]
        public async Task GetAgileItem_Valid_Success()
        {
            // Arrange
            // Act
            var item = await _agileItemRepository.GetFullAgileItem(_stubStoryDto.Id);

            // Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(_stubStoryDto.AgileItemType, item.AgileItemType);
            Assert.AreEqual(_stubStoryDto.AssigneeId, item.AssigneeId);
            Assert.AreEqual(_stubStoryDto.AssigneeName, item.AssigneeName);
            Assert.AreEqual(_stubStoryDto.BlockedReason, item.BlockedReason);
            Assert.AreEqual(_stubStoryDto.BoardId, item.BoardId);
            Assert.AreEqual(_stubStoryDto.CreatedBy, item.CreatedBy);
            Assert.AreEqual(_stubStoryDto.CreatedOn, item.CreatedOn);
            Assert.AreEqual(_stubStoryDto.Description, item.Description);
            Assert.AreEqual(_stubStoryDto.DueBy, item.DueBy);
            Assert.AreEqual(_stubStoryDto.EstimatedTime, item.EstimatedTime);
            Assert.AreEqual(_stubStoryDto.IsActive, item.IsActive);
            Assert.AreEqual(_stubStoryDto.IsComplete, item.IsComplete);
            Assert.AreEqual(_stubStoryDto.LoggedTime, item.LoggedTime);
            Assert.AreEqual(_stubStoryDto.Order, item.Order);
            Assert.AreEqual(_stubStoryDto.ParentId, item.ParentId);
            Assert.AreEqual(_stubStoryDto.Priority, item.Priority);
            Assert.AreEqual(_stubStoryDto.Status, item.Status);
            Assert.AreEqual(_stubStoryDto.StoryPoints, item.StoryPoints);
            Assert.AreEqual(_stubStoryDto.Title, item.Title);

        }

        [Test]
        public async Task GetAgileItem_Invalid_Fails()
        {
            // Arrange
            // Act
            var item = await _agileItemRepository.GetFullAgileItem(Guid.NewGuid());

            // Assert
            Assert.IsNull(item);
        }
    }
}