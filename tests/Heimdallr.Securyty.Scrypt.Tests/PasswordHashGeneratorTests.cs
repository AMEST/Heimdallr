using System;
using Heimdallr.Security;
using Heimdallr.Security.Scrypt;
using Xunit;

namespace Heimdallr.Securyty.Scrypt.Tests
{
    public class PasswordHashGeneratorTests
    {
        [Fact]
        public void ShouldGenerateIdempotentHash_WhenHashGenerateExecuteTwice()
        {
            var request = new SecurityRequest
                {ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster"};
            var hashGenerator = new PasswordHashGenerator();

            var firstResult = hashGenerator.GenerateHash(request).GetAwaiter().GetResult();
            var secondResult = hashGenerator.GenerateHash(request).GetAwaiter().GetResult();

            Assert.Equal(firstResult, secondResult);
        }

        [Fact]
        public void ShouldNotGenerateIdempotentHash_WhenDifferentMasterPassword()
        {
            var request = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster" };
            var request2 = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "notverysecreymaster" };
            var hashGenerator = new PasswordHashGenerator();

            var firstResult = hashGenerator.GenerateHash(request).GetAwaiter().GetResult();
            var secondResult = hashGenerator.GenerateHash(request2).GetAwaiter().GetResult();

            Assert.NotEqual(firstResult, secondResult);
        }


        [Fact]
        public void ShouldNotGenerateIdempotentHash_WhenDifferentVersion()
        {
            var request = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster", Version = 1};
            var request2 = new SecurityRequest
                { ServiceName = "test", CommonName = "user", MasterPassword = "verysecreymaster", Version = 2};
            var hashGenerator = new PasswordHashGenerator();

            var firstResult = hashGenerator.GenerateHash(request).GetAwaiter().GetResult();
            var secondResult = hashGenerator.GenerateHash(request2).GetAwaiter().GetResult();

            Assert.NotEqual(firstResult, secondResult);
        }
    }
}