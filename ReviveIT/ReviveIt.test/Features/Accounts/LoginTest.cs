using Application.DTO;
using Application.Features.Accounts;
using Application.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ReviveIt.test.Feature
{
    public class LoginTest
    {
        private readonly TokenHelper _tokenHelper;
        private readonly Mock<UserManager<Users>> _userManagerMock;
        private readonly Mock<SignInManager<Users>> _signInManagerMock;
        private readonly LoginFeature _loginFeature;
        private readonly IConfiguration _configuration;

        public LoginTest()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _tokenHelper = new TokenHelper(_configuration);

            _userManagerMock = SetupUserManagerMock();
            _signInManagerMock = SetupSignInManagerMock();

            _loginFeature = new LoginFeature(_userManagerMock.Object, _signInManagerMock.Object, _tokenHelper);
        }

        private Mock<UserManager<Users>> SetupUserManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<Users>>();
            var userManagerMock = new Mock<UserManager<Users>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            return userManagerMock;
        }

        private Mock<SignInManager<Users>> SetupSignInManagerMock()
        {
            var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
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
            Assert.Equal("User not found.", result.ErrorMessage);
        }


        [Fact]
        public async Task AuthenticateUser_ShouldReturnFailure_WhenEmailNotConfirmed()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password123" };
            var user = new Users
            {
                Email = loginDto.Email,
                EmailConfirmed = false,
                Role = UserRole.Customer 
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);

            var result = await _loginFeature.AuthenticateUser(loginDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Email not verified.", result.ErrorMessage);
        }


        [Fact]
        public async Task AuthenticateUser_ShouldReturnFailure_WhenPasswordIsIncorrect()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "wrongpassword" };
            var user = new Users
            {
                Email = loginDto.Email,
                EmailConfirmed = true,
                Role = UserRole.Customer 
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, loginDto.Password, false))
                              .ReturnsAsync(SignInResult.Failed);

            var result = await _loginFeature.AuthenticateUser(loginDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Password incorrect.", result.ErrorMessage);
        }


        [Fact]
        public async Task AuthenticateUser_ShouldReturnSuccess_WithToken_WhenCredentialsAreValid()
        {
            var loginDto = new LoginDto { Email = "gosalci38@gmail.com", Password = "P@ssw0rd" };
            var user = new Users
            {
                Email = loginDto.Email,
                EmailConfirmed = true, 
                Id = "123",
                UserName = "TestUser",
                Role = UserRole.Admin 
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, loginDto.Password, false)).ReturnsAsync(SignInResult.Success);

            var result = await _loginFeature.AuthenticateUser(loginDto);

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Token);
        }
    }
}
