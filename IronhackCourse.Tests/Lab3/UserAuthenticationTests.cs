using System;
using Lab3.Interfaces;
using Lab3.Managers;
using Moq;

namespace IronhackCourse.Tests.Lab3
{
	public class UserAuthenticationTests
	{
		[Fact]
		public void Authenticate_ValidUser_ReturnsTrue()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			mockUserRepository
				.Setup(r => r.ValidateCredentials("validUser", "validPass"))
				.Returns(true);

			var authentication = new UserAuthenticationManager(mockUserRepository.Object);

			var result = authentication.Authenticate("validUser", "validPass");

			Assert.True(result, "Esperamos una autenticación exitosa");
		}

        [Fact]
        public void Authenticate_InvalidPassword_ReturnsFalse()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(r => r.ValidateCredentials("validUser", "invalidPass"))
                .Returns(false);

            var authentication = new UserAuthenticationManager(mockUserRepository.Object);

            var result = authentication.Authenticate("validUser", "invalidPass");

            Assert.False(result, "Esperamos una falla porque la contraseña es invalida");
        }

        [Fact]
        public void Authenticate_InvalidUser_ReturnsFalse()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(r => r.ValidateCredentials("invalidUser", "validPass"))
                .Returns(false);

            var authentication = new UserAuthenticationManager(mockUserRepository.Object);

            var result = authentication.Authenticate("invalidUser", "validPass");

            Assert.False(result, "Esperamos una falla porque la contraseña es invalida");
        }

        [Fact]
        public void Authenticate_NullCredentials_ReturnsFalse()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            var authentication = new UserAuthenticationManager(mockUserRepository.Object);

            var result = authentication.Authenticate(null, null);

            Assert.False(result, "Esperamos una falla porque las credenciales son nulas");
        }
    }
}

