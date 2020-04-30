using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using StoryService.Models.Dtos;
using StoryService.Models.ViewModels;
using StoryService.Models.ViewModels.Board;
using StoryService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryService.Controllers.Tests
{
    public class AgileItemsControllerTests
    {
        private AgileItemsController _agileItemsController;
        private Mock<IAgileItemRepository> _mockAgileItemsRepository;
        private CreateAgileItemDto _stubCreateAgileItemDto;
        private List<AgileItemShortVm> _stubShortVmList;
        private BoardTaskVm _stubUpdateItemVm;
        private AgileItemShortVm _stubShortVm;
        private AgileItemVm _stubFullAgileItemVm;
        private AgileItemOverviewVm _stubOverviewVm;
        private List<AgileItemOverviewVm> _stubOverviewVmList;

        [SetUp]
        public void Setup()
        {
            _mockAgileItemsRepository = new Mock<IAgileItemRepository>();
            _agileItemsController = new AgileItemsController(_mockAgileItemsRepository.Object);
            _stubShortVm = new AgileItemShortVm
            {
                Id = Guid.Parse("08a60a7f-6d09-438c-97f7-cef7426b1b83"),
                Order = 1,
                Title = "Stub Title"
            };
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
            _stubUpdateItemVm = new BoardTaskVm
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
            _stubFullAgileItemVm = new AgileItemVm
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
            _stubOverviewVm = new AgileItemOverviewVm
            {
                AssigneeName = "Oli",
                Description = "Stub Story",
                Order = null,
                Priority = Models.Types.Priority.High,
                Title = "A stub creation of a story",
                IsComplete = false,
                Status = Models.Types.Status.InProgress,
            };
            _stubShortVmList = new List<AgileItemShortVm> { _stubShortVm };
            _stubOverviewVmList = new List<AgileItemOverviewVm> { _stubOverviewVm };
        }

        [Test]
        public async Task SearchAgileItems_Valid_Success()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.SearchForAgileItem(It.IsAny<SearchAgileItemDto>()))
                   .ReturnsAsync(_stubShortVmList);

            var stubSearchVm = new SearchAgileItemDto
            {
                BoardId = Guid.NewGuid(),
                ItemType = Models.Types.AgileItemType.Story,
                SearchQuery = "Stub Search"
            };

            // Act
            var items = await _agileItemsController.SearchAgileItems(stubSearchVm) as OkObjectResult;

            // Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(items.StatusCode, 200);
            Assert.AreEqual(items.Value, _stubShortVmList);
            _mockAgileItemsRepository.Verify(m => m.SearchForAgileItem(It.IsAny<SearchAgileItemDto>()), Times.Once);
        }

        [Test]
        public async Task CreateAgileItem_Valid_Success()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.CreateAgileItem(It.IsAny<CreateAgileItemDto>()))
                .ReturnsAsync(true);

            // Act
            var result = await _agileItemsController.CreateAgileItem(_stubCreateAgileItemDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, true);
        }

        [Test]
        public async Task CreateAgileItem_CreationFails_Failure()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.CreateAgileItem(It.IsAny<CreateAgileItemDto>()))
                .ReturnsAsync(false);

            // Act
            var result = await _agileItemsController.CreateAgileItem(_stubCreateAgileItemDto) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task UpdateItem_Valid_Success()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.UpdateAgileItem(It.IsAny<BoardTaskVm>()))
                .ReturnsAsync(true);

            // Act
            var result = await _agileItemsController.UpdateAgileItem(_stubUpdateItemVm) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, true);
        }

        [Test]
        public async Task UpdateItem_UpdateFails_Failure()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.UpdateAgileItem(It.IsAny<BoardTaskVm>()))
                .ReturnsAsync(false);

            // Act
            var result = await _agileItemsController.UpdateAgileItem(_stubUpdateItemVm) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task GetAgileItem_Valid_Success()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.GetFullAgileItem(It.IsAny<Guid>()))
                .ReturnsAsync(_stubFullAgileItemVm);

            // Act
            var result = await _agileItemsController.GetFullAgileItem(Guid.NewGuid()) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, _stubFullAgileItemVm);
        }

        [Test]
        public async Task GetAgileItem_NoItemFound_Failure()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.GetFullAgileItem(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _agileItemsController.GetFullAgileItem(Guid.NewGuid()) as OkObjectResult;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetRelatedItems_Valid_Success()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.GetRelatedItems(It.IsAny<Guid>()))
                .ReturnsAsync(_stubOverviewVmList);

            // Act
            var result = await _agileItemsController.GetRelatedAgileItems(Guid.NewGuid()) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, _stubOverviewVmList);
        }

        [Test]
        public async Task GetRelatedItems_NoItems_Failure()
        {
            // Arrange
            _mockAgileItemsRepository.Setup(m => m.GetRelatedItems(It.IsAny<Guid>()))
                .ReturnsAsync(() =>  null);

            // Act
            var result = await _agileItemsController.GetRelatedAgileItems(Guid.NewGuid()) as StatusCodeResult;

            // Assert
            Assert.IsNull(result);
        }
    }
}