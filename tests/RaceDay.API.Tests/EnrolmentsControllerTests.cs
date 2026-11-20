using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Controllers;
using RaceDay.API.Data;
using RaceDay.API.Models;
using System;
using System.Threading.Tasks;
using Xunit; // xUnit.net testing framework core namespace (xUnit.net, 2025)

namespace RaceDay.API.Tests
{
    public class EnrolmentsControllerTests
    {
        // Using EF Core InMemory database for isolated unit testing (Microsoft Learn, 2025)
        private RaceDayDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new RaceDayDbContext(options);
        }

        [Fact] // xUnit [Fact] attribute defining a test method (xUnit.net, 2025)
        public async Task Enrol_Participant_ShouldRecordEnrolment()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new EnrolmentsController(context);

            var enrolmentModel = new Enrolment
            {
                CategoryId = 1,
                ParticipantId = 2,
                EnrolmentDate = DateTime.UtcNow
            };

            // Act
            context.Enrolments.Add(enrolmentModel);
            await context.SaveChangesAsync();

            var result = await controller.GetEnrolments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}