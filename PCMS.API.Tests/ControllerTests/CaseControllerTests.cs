using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PCMS.API.Controllers;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;
using PCMS.API.Models.Enums;
using System.Security.Claims;

namespace PCMS.API.Tests.ControllerTests
{
    public class CaseControllerTests
    {
        [Fact]
        public async Task CreateCase_ValidRequest_ReturnsCreatedResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new ApplicationDbContext(options);

            var mapperMock = new Mock<IMapper>();

            var postCase = new POSTCase
            {
                Title = "Test Case",
                Description = "Test Description",
                Complexity = CaseComplexity.Complex,
                Priority = CasePriority.Low,
                Type = "Test Type"
            };

            var newCase = new Case
            {
                Id = Guid.NewGuid().ToString(),
                CaseNumber = "CA-2024-12345678",
                Title = postCase.Title,
                Description = postCase.Description,
                Status = CaseStatus.Open,
                DateOpened = DateTime.UtcNow,
                DateClosed = null,
                LastModifiedDate = null,
                Complexity = postCase.Complexity,
                Priority = postCase.Priority,
                Type = postCase.Type,
                CreatedById = "testUserId"
            };

            mapperMock.Setup(m => m.Map<Case>(It.IsAny<POSTCase>()))
                .Returns(newCase);

            var getCase = new GETCase
            {
                Id = newCase.Id,
                CaseNumber = newCase.CaseNumber,
                Title = newCase.Title,
                Description = newCase.Description,
                Status = newCase.Status,
                DateOpened = newCase.DateOpened,
                DateClosed = newCase.DateClosed,
                LastModifiedDate = newCase.LastModifiedDate,
                Complexity = newCase.Complexity,
                Priority = newCase.Priority,
                Type = newCase.Type,
                Creator = new GETApplicationUser
                {
                    Id = "testUserId",
                    UserName = "TestUser",
                    Email = "testuser@example.com"
                },
                LastEditor = null
            };

            mapperMock.Setup(m => m.Map<GETCase>(It.IsAny<Case>()))
                .Returns(getCase);

            var controller = new CaseController(context, mapperMock.Object);

            // Mock ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.NameIdentifier, "testUserId"),
            ]));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.CreateCase(postCase);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<GETCase>(createdAtActionResult.Value);
            Assert.Equal(newCase.Id, returnValue.Id);
            Assert.Equal(newCase.CaseNumber, returnValue.CaseNumber);
            Assert.Equal(newCase.Title, returnValue.Title);
            Assert.Equal(newCase.Description, returnValue.Description);
            Assert.Equal(newCase.Status, returnValue.Status);
            Assert.Equal(newCase.DateOpened, returnValue.DateOpened);
            Assert.Equal(newCase.DateClosed, returnValue.DateClosed);
            Assert.Equal(newCase.LastModifiedDate, returnValue.LastModifiedDate);
            Assert.Equal(newCase.Complexity, returnValue.Complexity);
            Assert.Equal(newCase.Priority, returnValue.Priority);
            Assert.Equal(newCase.Type, returnValue.Type);
            Assert.Equal("testUserId", returnValue.Creator.Id);

            // Verify that the case was added to the context
            var savedCase = await context.Cases.FirstOrDefaultAsync(c => c.Id == newCase.Id);
            Assert.NotNull(savedCase);
            Assert.Equal("testUserId", savedCase.CreatedById);
            Assert.Equal(postCase.Title, savedCase.Title);
            Assert.Equal(postCase.Description, savedCase.Description);
            Assert.Equal(postCase.Complexity, savedCase.Complexity);
            Assert.Equal(postCase.Priority, savedCase.Priority);
            Assert.Equal(postCase.Type, savedCase.Type);
        }

        [Fact]
        public async Task GetCase_ValidId_ReturnsOkResultWithCase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_GetCase")
                .Options;

            using var context = new ApplicationDbContext(options);

            var mapperMock = new Mock<IMapper>();

            var existingCase = new Case
            {
                Id = Guid.NewGuid().ToString(),
                CaseNumber = "CA-2024-12345678",
                Title = "Test Case",
                Description = "Test Description",
                Status = CaseStatus.Open,
                DateOpened = DateTime.UtcNow,
                DateClosed = null,
                LastModifiedDate = null,
                Complexity = CaseComplexity.Complex,
                Priority = CasePriority.Low,
                Type = "Test Type",
                CreatedById = "testUserId",
                Creator = new ApplicationUser
                {
                    Id = "testUserId",
                    UserName = "TestUser",
                    Email = "testuser@example.com"
                }
            };

            await context.Cases.AddAsync(existingCase);
            await context.SaveChangesAsync();

            var getCase = new GETCase
            {
                Id = existingCase.Id,
                CaseNumber = existingCase.CaseNumber,
                Title = existingCase.Title,
                Description = existingCase.Description,
                Status = existingCase.Status,
                DateOpened = existingCase.DateOpened,
                DateClosed = existingCase.DateClosed,
                LastModifiedDate = existingCase.LastModifiedDate,
                Complexity = existingCase.Complexity,
                Priority = existingCase.Priority,
                Type = existingCase.Type,
                Creator = new GETApplicationUser
                {
                    Id = existingCase.Creator.Id,
                    UserName = existingCase.Creator.UserName,
                    Email = existingCase.Creator.Email
                }
            };

            mapperMock.Setup(m => m.Map<GETCase>(It.IsAny<Case>()))
                .Returns(getCase);

            var controller = new CaseController(context, mapperMock.Object);

            // Act
            var result = await controller.GetCase(existingCase.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GETCase>(okResult.Value);

            Assert.Equal(existingCase.Id, returnValue.Id);
            Assert.Equal(existingCase.CaseNumber, returnValue.CaseNumber);
            Assert.Equal(existingCase.Title, returnValue.Title);
            Assert.Equal(existingCase.Description, returnValue.Description);
            Assert.Equal(existingCase.Status, returnValue.Status);
            Assert.Equal(existingCase.DateOpened, returnValue.DateOpened);
            Assert.Equal(existingCase.Complexity, returnValue.Complexity);
            Assert.Equal(existingCase.Priority, returnValue.Priority);
            Assert.Equal(existingCase.Type, returnValue.Type);
            Assert.Equal(existingCase.Creator.Id, returnValue.Creator.Id);
            Assert.Equal(existingCase.Creator.UserName, returnValue.Creator.UserName);
            Assert.Equal(existingCase.Creator.Email, returnValue.Creator.Email);
        }

        [Fact]
        public async Task GetCase_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_GetCase")
                .Options;

            using var context = new ApplicationDbContext(options);
            var mapperMock = new Mock<IMapper>();

            var controller = new CaseController(context, mapperMock.Object);

            // Act
            var result = await controller.GetCase("nonexistent-id");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Case not found.", notFoundResult.Value);
        }

    }
}
