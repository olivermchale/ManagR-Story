using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using StoryService.Models.ViewModels.Board;
using StoryService.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoryService.Controllers.Tests
{
    public class BoardControllerTests
    {
        private BoardController _boardController;
        private Mock<IBoardRepository> _mockBoardRepository;
        private BoardVm _mockBoardVm;
        [SetUp]
        public void Setup()
        {
            _mockBoardRepository = new Mock<IBoardRepository>();
            _boardController = new BoardController(_mockBoardRepository.Object);
            _mockBoardVm = new BoardVm
            {
                Stories = new List<BoardStoryVm>
                {
                    new BoardStoryVm
                    {
                        AssigneeId = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                        AssigneeName = "Oli",
                        Priority = Models.Types.Priority.High,
                        StoryPoints = 3,
                        Title = "A stub creation of a story",
                        Status = Models.Types.Status.Complete,
                        Tasks = new Models.ViewModels.BoardTaskListVm
                        {
                            Todo = new List<BoardTaskVm>
                            {
                                new BoardTaskVm
                                {
                                    AssigneeId = Guid.Parse("0f294219-704d-40a5-afe9-a74fffa7003f"),
                                    AssigneeName = "Oli",
                                    Priority = Models.Types.Priority.High,
                                    Title = "A stub creation of a task",
                                    Status = Models.Types.Status.Complete,
                                }
                            }

                        }
                    }
                }
            };
        }

        [Test]
        public async Task GetBoard_Valid_Success()
        {
            // Arrange
            _mockBoardRepository.Setup(m => m.GetBoard(It.IsAny<Guid>()))
                .ReturnsAsync(_mockBoardVm);

            // Act
            var result = await _boardController.GetBoard(Guid.NewGuid()) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            var value = result.Value as BoardVm;
            Assert.IsNotNull(value);
            Assert.IsNotNull(value.Stories);
            Assert.AreEqual(value.Stories.Count, 1);
            Assert.AreSame(value.Stories[0], _mockBoardVm.Stories[0]);
            Assert.AreSame(value.Stories[0].Tasks.Todo, _mockBoardVm.Stories[0].Tasks.Todo);
        }

        [Test]
        public async Task GetBoardNames_Valid_Success()
        {
            //Arrange
            _mockBoardRepository.Setup(m => m.GetBoardNames())
                .ReturnsAsync(new BoardNameListVm
                {
                    boardNames = new List<Models.ViewModels.BoardNameVm>
                    {
                        new Models.ViewModels.BoardNameVm
                        {
                            BoardName = "Stub board name",
                            Id = Guid.Parse("aa5f3a3c-b80e-486b-a4b5-4ca91c3ae496")
                        }
                    }
                });

            // Act
            var result = await _boardController.GetBoardNames() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            var value = result.Value as BoardNameListVm;
            Assert.AreSame(value.boardNames[0].BoardName, "Stub board name");
        }
    }
}
