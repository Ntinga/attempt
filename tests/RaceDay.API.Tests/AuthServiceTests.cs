using RaceDay.API.Services;
using Xunit;

namespace RaceDay.API.Tests
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _authService = new AuthService();
        }

        [Fact]
        public void HashPassword_ShouldGenerateHash_AndVerifyCorrectly()
        {
            // Arrange
            string rawPassword = "TestPassword123!";

            // Act
            string hashedPassword = _authService.HashPassword(rawPassword);
            bool isValid = _authService.VerifyPassword(rawPassword, hashedPassword);

            // Assert
            Assert.NotNull(hashedPassword); // xUnit null-check assertion (xUnit.net, 2025)
            Assert.NotEqual(rawPassword, hashedPassword); // xUnit inequality assertion ensuring hashing works
            Assert.True(isValid); // xUnit boolean condition assertion (xUnit.net, 2025)
        }

        [Fact]
        public void VerifyPassword_WithIncorrectPassword_ShouldReturnFalse()
        {
            // Arrange
            string rawPassword = "CorrectPassword123!";
            string wrongPassword = "WrongPassword123!";

            // Act
            string hashedPassword = _authService.HashPassword(rawPassword);
            bool isValid = _authService.VerifyPassword(wrongPassword, hashedPassword);

            // Assert
            Assert.False(isValid);
        }
    }
}