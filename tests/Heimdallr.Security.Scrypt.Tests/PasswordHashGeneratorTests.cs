using System.Threading.Tasks;
using Heimdallr.Security;
using Heimdallr.Security.Scrypt;
using Xunit;

namespace Heimdallr.Securyty.Scrypt.Tests
{
    public class PasswordHashGeneratorTests
    {
        [Fact]
        public async Task ShouldGenerateIdempotentHash_WhenHashGenerateExecuteTwice()
        {
            var request = new SecurityRequest
                {ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster"};
            var hashGenerator = new PasswordHashGenerator();

            var firstResult = await hashGenerator.GenerateHash(request);
            var secondResult = await hashGenerator.GenerateHash(request);

            Assert.Equal(firstResult, secondResult);
        }

        [Fact]
        public async Task ShouldNotGenerateIdempotentHash_WhenDifferentMasterPassword()
        {
            var request = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster" };
            var request2 = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "notverysecreymaster" };
            var hashGenerator = new PasswordHashGenerator();

            var firstResult = await hashGenerator.GenerateHash(request);
            var secondResult = await hashGenerator.GenerateHash(request2);

            Assert.NotEqual(firstResult, secondResult);
        }


        [Fact]
        public async Task ShouldNotGenerateIdempotentHash_WhenDifferentVersion()
        {
            var request = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster", Version = 1};
            var request2 = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster", Version = 2};
            var hashGenerator = new PasswordHashGenerator();

            var firstResult = await hashGenerator.GenerateHash(request);
            var secondResult = await hashGenerator.GenerateHash(request2);

            Assert.NotEqual(firstResult, secondResult);
        }
    }
}