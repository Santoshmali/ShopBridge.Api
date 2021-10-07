using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using ShopBridge.Core.DataModels;
using ShopBridge.Core.DataModels.Users;
using ShopBridge.Data.DbModels.Users;
using ShopBridge.Data.Repositories.Users;
using ShopBridge.Services.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.Tests.ShopBridge.Services
{
    public class UserServiceTests
    {
        //Unit under test
        UserService _userService;

        // Dependencies
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IOptions<AppSettings>> _appSettingsMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _appSettingsMock = new Mock<IOptions<AppSettings>>();

            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object, _appSettingsMock.Object);
        }

        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void Authenticate_WithNullAuthenticateRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            AuthenticateRequest authenticateRequest = null;
            User userResponse = null;
            _userRepositoryMock.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(userResponse));

            // Act
            Func<Task> act = () => _userService.Authenticate(authenticateRequest, ""); 

            // Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(act);

        }

        [Fact]
        public async Task Authenticate_WithInvalidUsernamePassword_ShouldReturnNull()
        {
            // Arrange
            AuthenticateRequest authenticateRequest = new AuthenticateRequest();
            User userResponse = null;
            _userRepositoryMock.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(userResponse));

            // Act
            var result = await _userService.Authenticate(authenticateRequest, "");

            // Assert
            Assert.Null(result);
        }
    }
}
