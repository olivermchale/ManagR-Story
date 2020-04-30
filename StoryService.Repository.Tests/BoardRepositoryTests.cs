using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StoryService.Data;
using StoryService.Models.Dtos;
using StoryService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Repository.Tests
{
    public class BoardRepositoryTests
    {
        private IBoardRepository _boardRepository;
        private Mock<ILogger<BoardRepository>> _mockLogger;
        private AgileItemDto _stubSuperStoryDto;
        private AgileItemDto _stubStoryDto;
        private BoardDto _stubBoard;

        [SetUp]
        public void Setup()
        {
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
            _mockLogger = new Mock<ILogger<BoardRepository>>();
            _boardRepository = new BoardRepository(GetInMemoryContextWithSeedData(), _mockLogger.Object);
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
        public async Task GetBoard_Valid_Success()
        {
            // Arrange
            var boardId = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f43408a1");

            // Act
            var board = await _boardRepository.GetBoard(boardId);

            // Assert
            Assert.IsNotNull(board);
            Assert.IsNotNull(board.Stories);
            Assert.AreEqual(board.Stories.Count, 1);
        }

        [Test]
        public async Task GetBoard_Invalid_Failure()
        {
            // Arrange
            var fakeBoardId = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f4340821");

            // Act
            var board = await _boardRepository.GetBoard(fakeBoardId);

            // Assert
            // assert object structure still exists for elegant handling in front end
            Assert.IsNotNull(board);
            Assert.IsNotNull(board.Stories);
            Assert.AreEqual(board.Stories.Count, 0);
        }

        [Test]
        public async Task GetBoardTopology_Valid_Success()
        {
            // Arrange
            var boardId = Guid.Parse("aada6f2a-8854-4a5e-a907-2244f43408a1");

            // Act
            var topology = await _boardRepository.GetBoardTopology(boardId);

            // Assert
            Assert.IsNotNull(topology);
            Assert.AreEqual(1, topology.SuperStoryCount);
            Assert.AreEqual(1, topology.StoryCount);
            Assert.AreEqual(0, topology.TaskCount);
        }

        [Test]
        public async Task GetBoardNames_Valid_Success()
        {
            // Arrange
            // Act
            var names = await _boardRepository.GetBoardNames();

            // Assert 
            Assert.IsNotNull(names);
            Assert.AreEqual(1, names.boardNames.Count);
            Assert.AreEqual(names.boardNames[0].BoardName, _stubBoard.BoardName);
        }
    }
}
