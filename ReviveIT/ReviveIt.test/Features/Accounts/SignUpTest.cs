using Application.DTO;
using Application.Features.Accounts;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ReviveIt.test.Feature
{
    public class SignupTest
    {
        private readonly Mock<UserManager<Users>> _userManagerMock;
        private readonly Mock<IEmailSender> _emailSenderMock;
        private readonly TokenHelper _tokenHelper;
        private readonly RegisterFeature _registerFeature;

        public SignupTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _userManagerMock = SetupUserManagerMock();
            _emailSenderMock = new Mock<IEmailSender>();
            _tokenHelper = new TokenHelper(configuration, new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>().Object);

            _registerFeature = new RegisterFeature(_userManagerMock.Object, _tokenHelper, _emailSenderMock.Object);
        }

        private Mock<UserManager<Users>> SetupUserManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<Users>>();
            return new Mock<UserManager<Users>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenEmailAlreadyExists()
        {
            var dto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd",
                Role = UserRole.Customer,
                Name = "test"
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(dto.Email))
                            .ReturnsAsync(new Users { Email = dto.Email });

            var result = await _registerFeature.RegisterUserAsync(dto);

            Assert.False(result.Success);
            Assert.Equal("An account with this email already exists.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenPasswordsDoNotMatch()
        {
            var dto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd123",
                Role = UserRole.Customer
            };

            var result = await _registerFeature.RegisterUserAsync(dto);

            Assert.False(result.Success);
            Assert.Equal("Passwords do not match.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenFullNameIsRequiredForCustomerRole()
        {
            var dto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd",
                Role = UserRole.Customer
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(dto.Email))
                            .ReturnsAsync((Users)null);

            var result = await _registerFeature.RegisterUserAsync(dto);

            Assert.False(result.Success);
            Assert.Equal("Full name is required for Customer role.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenPasswordIsTooWeak()
        {
            var dto = new RegisterDto
            {
                Email = "newuser@example.com",
                Password = "123",
                ConfirmPassword = "123",
                Role = UserRole.Customer
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(dto.Email))
                            .ReturnsAsync((Users)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<Users>(), dto.Password))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError
                            {
                                Code = "PasswordTooWeak",
                                Description = "Password must contain at least 8 characters, including uppercase, lowercase, at least one number and an non alphanumeric character."
                            }));

            var result = await _registerFeature.RegisterUserAsync(dto);

            Assert.False(result.Success);
            Assert.Equal("Password must contain at least 8 characters, including uppercase, lowercase, at least one number and an non alphanumeric character.", result.Message); // Updated message
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenRoleAssignmentFails()
        {
            var dto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd",
                Role = UserRole.Customer,
                Name = "test"
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(dto.Email))
                            .ReturnsAsync((Users)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<Users>(), dto.Password))
                            .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<Users>(), dto.Role.ToString()))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError
                            {
                                Code = "RoleAssignmentFailed",
                                Description = "Failed to assign role."
                            }));

            var result = await _registerFeature.RegisterUserAsync(dto);

            Assert.False(result.Success);
            Assert.Equal("Failed to assign role: Failed to assign role.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenInvalidRoleSelected()
        {
            var dto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd",
                Role = (UserRole)999,
                Name = "test"
            };

            var result = await _registerFeature.RegisterUserAsync(dto);

            Assert.False(result.Success);
            Assert.Equal("Invalid role selected.", result.Message);
        }
    }
}
