using System.Threading.Tasks;
using Heimdallr.Security;
using Heimdallr.Security.Scrypt;
using Xunit;

namespace Heimdallr.Securyty.Scrypt.Tests
{
    public class PasswordServiceTests
    {
        [Theory]
        [InlineData("google", "user", "masterpassword1")]
        [InlineData("ldap", "ldapuser", "masterpassword")]
        [InlineData("github", "developer", "masterpassword")]
        public async Task ShouldGenerateIdempotentPass_WhenPassGenerateExecuteTwice(string service, string name,
            string masterpwd)
        {
            var request = new SecurityRequest {ServiceName = service, CommonName = name, MasterPassword = masterpwd};
            var passwordService = new PasswordService(new PasswordHashGenerator());

            var firstResult = await passwordService.Generate(request);
            var secondResult = await passwordService.Generate(request);

            Assert.Equal(firstResult.GeneratedPassword, secondResult.GeneratedPassword);
        }
    }
}