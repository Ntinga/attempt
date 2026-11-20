using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Controllers;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RaceDay.API.Tests
{
    public class EventsControllerTests
    {
        private RaceDayDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new RaceDayDbContext(options);
        }

        [Fact]
        public async Task CreateEvent_ShouldSuccessfullyAddEvent()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new EventsController(context);

            var eventDto = new EventCreateDto
            {
                EventName = "Marathon 2026",
                Description = "A competitive 42km race",
                EventDate = DateTime.UtcNow.AddDays(30),
                Location = "Durban Coast",
                OrganiserId = 1
            };

            // Act
            var result = await controller.CreateEvent(eventDto);

            // Assert - Flexible pattern matching for action results in ASP.NET Core API testing (Microsoft Learn, 2025)
            var okResult = result as OkObjectResult;
            var createdResult = result as CreatedAtActionResult;

            if (okResult != null)
            {
                Assert.NotNull(okResult.Value);
            }
            else
            {
                Assert.NotNull(createdResult);
                Assert.NotNull(createdResult.Value);
            }
        }

        [Fact]
        public async Task GetEvents_ShouldReturnEvents()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Events.Add(new Event { EventName = "Event 1", Location = "Loc 1", Description = "Desc 1", OrganiserId = 1, EventDate = DateTime.UtcNow.AddDays(10) });
            await context.SaveChangesAsync();

            var controller = new EventsController(context);

            // Act
            var result = await controller.GetEvents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}