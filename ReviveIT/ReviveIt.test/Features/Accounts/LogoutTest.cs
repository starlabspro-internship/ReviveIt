using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ReviveIt.test.Feature
{
    public class LogoutTest
    {
        private readonly Mock<UserManager<Users>> _userManagerMock;
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
        private readonly AccountsController _accountsController;

        public LogoutTest()
        {
            _userManagerMock = new Mock<UserManager<Users>>(
                Mock.Of<IUserStore<Users>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );

            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();

            _accountsController = new AccountsController(
                null,
                null,
                null,
                _userManagerMock.Object, _refreshTokenRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Logout_ShouldReturnOk_WhenLogoutIsSuccessful()
        {
            var testUserId = "12345";
            var testEmail = "test@example.com";

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testEmail)
            }, "TestAuthType");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _accountsController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(testEmail))
                .ReturnsAsync(new Users { Id = testUserId, Email = testEmail });

            _refreshTokenRepositoryMock.Setup(repo => repo.RemoveRefreshTokenAsync(testUserId))
                .ReturnsAsync(true);

            var result = await _accountsController.Logout();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully logged out", okResult.Value.GetType().GetProperty("message").GetValue(okResult.Value));
        }

        [Fact]
        public async Task Logout_ShouldReturnNotFound_WhenRefreshTokenDoesNotExist()
        {
            var testUserId = "12345";
            var testEmail = "test@example.com";

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, testEmail)
            }, "TestAuthType");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _accountsController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(testEmail))
                .ReturnsAsync(new Users { Id = testUserId, Email = testEmail });

            _refreshTokenRepositoryMock.Setup(repo => repo.RemoveRefreshTokenAsync(testUserId))
                .ReturnsAsync(false);

            var result = await _accountsController.Logout();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Refresh token not found.", notFoundResult.Value.GetType().GetProperty("message").GetValue(notFoundResult.Value));
        }
    }
}
