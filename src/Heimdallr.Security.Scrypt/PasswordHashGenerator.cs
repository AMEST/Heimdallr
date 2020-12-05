using Scrypt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heimdallr.Security.Scrypt
{
    internal class PasswordHashGenerator
    {
        private const int DefaultIterationCount = 16384;
        private const int DefaultBlockSize = 8;
        private const int DefaultThreadCount = 1;

        public async Task<string> GenerateHash(SecurityRequest request)
        {
            var masterPasswordNumberGenerator = new MasterPasswordNumberGenerator(request.MasterPassword);
            var encoder = new ScryptEncoder(
                iterationCount: DefaultIterationCount, 
                blockSize: DefaultBlockSize,
                threadCount: DefaultThreadCount, 
                saltGenerator: masterPasswordNumberGenerator);
            var passwordBase = CreatePasswordBase(request);

            var passwordHash = await Task.Run(() => encoder.Encode(passwordBase));

            return passwordHash.Split('$').Last();
        }

        private static string CreatePasswordBase(SecurityRequest request)
        {
            var builder = new StringBuilder();
            builder.Append(request.ServiceName);
            builder.Append(":");
            builder.Append(request.CommonName);
            builder.Append(":");
            builder.Append("v").Append(request.Version);
            return builder.ToString();
        }
    }
}