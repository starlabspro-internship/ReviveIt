using Application.DTO;
using Application.Features.Accounts;
using Application.Helpers;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using System.Threading.Tasks;
using ReviveIt.test.Helpers;
using Application.Interfaces;

namespace ReviveIt.test.Feature
{
    public class LoginTest
    {
        private readonly TokenHelper _tokenHelper;
        private readonly Mock<UserManager<Users>> _userManagerMock;
        private readonly Mock<SignInManager<Users>> _signInManagerMock;
        private readonly Mock<ITokenHelper> _tokenHelperMock;
        private readonly Mock<RefreshTokenRepository> _refreshTokenRepositoryMock;
        private readonly Mock<ConfigurationConstant> _constantMock;
        private readonly LoginFeature _loginFeature;
        private readonly IConfiguration _configuration;

        public LoginTest()
        {
            // Configure mock dependencies
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new DefaultHttpContext();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock);

            _tokenHelper = new TokenHelper(_configuration, httpContextAccessorMock.Object);

            _userManagerMock = SetupUserManagerMock();
            _signInManagerMock = SetupSignInManagerMock();
            _tokenHelperMock = new Mock<ITokenHelper>();

            // Mocking IApplicationDbContext to pass into RefreshTokenRepository
            var dbContextMock = new Mock<IApplicationDbContext>();
            _refreshTokenRepositoryMock = new Mock<RefreshTokenRepository>(dbContextMock.Object);

            _constantMock = new Mock<ConfigurationConstant>();

            _loginFeature = new LoginFeature(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _tokenHelper,
                _refreshTokenRepositoryMock.Object,
                _constantMock.Object
            );
        }

        private Mock<UserManager<Users>> SetupUserManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<Users>>();
            return new Mock<UserManager<Users>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        private Mock<SignInManager<Users>> SetupSignInManagerMock()
        {
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<Users>>();
            return new Mock<SignInManager<Users>>(_userManagerMock.Object, contextAccessorMock.Object, claimsFactoryMock.Object, null, null, null, null);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnFailure_WhenUserNotFound()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password123" };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync((Users)null);

            var result = await _loginFeature.AuthenticateUser(loginDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid credentials.", result.ErrorMessage);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnFailure_WhenEmailNotConfirmed()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password123" };
            var user = new Users
            {
                Email = loginDto.Email,
                EmailConfirmed = false,
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);

            var result = await _loginFeature.AuthenticateUser(loginDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Email not confirmed!", result.ErrorMessage);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnFailure_WhenPasswordIsIncorrect()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "wrongpassword" };
            var user = new Users
            {
                Email = loginDto.Email,
                EmailConfirmed = true,
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, loginDto.Password, false))
                              .ReturnsAsync(SignInResult.Failed);

            var result = await _loginFeature.AuthenticateUser(loginDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid credentials.", result.ErrorMessage);
        }

        //[Fact]
        //public async Task AuthenticateUser_ShouldReturnSuccess_WithToken_WhenCredentialsAreValid()
        //{
        //    // Arrange
        //    var loginDto = new LoginDto { Email = "gosalci38@gmail.com", Password = "P@ssw0rd" };
        //    var user = new Users
        //    {
        //        Email = loginDto.Email,
        //        EmailConfirmed = true,
        //        Id = "123",
        //        UserName = "TestUser",
        //        Role = UserRole.Admin
        //    };

        //    var refreshToken = new UserRefreshToken
        //    {
        //        UserId = user.Id,  // Ensure this matches your refresh token logic
        //        Token = "valid-refresh-token",
        //        ExpiresOn = DateTime.UtcNow.AddDays(1)  // Set an expiration if necessary
        //    };

        //    // Mock UserManager to return a user by email
        //    _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);

        //    // Mock SignInManager to return success for the credentials
        //    _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, loginDto.Password, false))
        //                      .ReturnsAsync(SignInResult.Success);

        //    // Mock ITokenHelper to generate a token
        //    _tokenHelperMock.Setup(th => th.GenerateToken(user))
        //                    .Returns("valid-jwt-token");

        //    // Mock IRefreshTokenRepository to add or update a refresh token
        //    _refreshTokenRepositoryMock.Setup(rfr => rfr.AddOrUpdateRefreshTokenAsync(It.IsAny<UserRefreshToken>()))
        //                    .Returns(Task.CompletedTask); // Ensure the method returns a completed task
        //                                                  // Mock the IRefreshTokenRepository
        //    _refreshTokenRepositoryMock.Setup(rfr => rfr.AddOrUpdateRefreshTokenAsync(It.IsAny<UserRefreshToken>()))
        //        .Returns(Task.CompletedTask);


        //    // Act
        //    var result = await _loginFeature.AuthenticateUser(loginDto);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.True(result.IsSuccess);
        //    Assert.NotEmpty(result.Token);
        //    Assert.Equal("valid-jwt-token", result.Token);  // Check if correct token is returned
        //}
    }
}
