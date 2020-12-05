using System;
using System.Text;
using System.Threading.Tasks;

namespace Heimdallr.Security.Scrypt
{
    internal class PasswordService : IPasswordService
    {
        private readonly PasswordHashGenerator _hashGenerator;
        private const string letterDict = "NmgOsVKtUYqoLflSraqRQBPpXTkhIGHACcnFzDvjdeZixyuEMbJQ";
        private const string numericDict = "9425086317";
        private const string symbolicDict = "!@#$%^&*()_+={}[]";

        public PasswordService(PasswordHashGenerator hashGenerator)
        {
            _hashGenerator = hashGenerator;
        }

        public async Task<SecurityResult> Generate(SecurityRequest passwordRequest)
        {
            var passwordHash = await _hashGenerator.GenerateHash(passwordRequest);
            var passwordDict = GetDict(passwordRequest);

            var generatePassword = GeneratePasswordFromHash(passwordHash, passwordDict);

            return new SecurityResult
            {
                GeneratedPassword = generatePassword.Substring(0,passwordRequest.Length)
            };
        }

        private static string GeneratePasswordFromHash(string hash, char[] dict)
        {
            var hashBytes = Convert.FromBase64String(hash);
            var passwordBuilder = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                var fixedByte = hashByte % dict.Length;
                passwordBuilder.Append(dict[fixedByte]);
            }

            return passwordBuilder.ToString();
        }

        private static char[] GetDict(SecurityRequest passwordRequest)
        {
            var builder = new StringBuilder();
            if (passwordRequest.HasNumeric)
                builder.Append(numericDict);
            if (passwordRequest.HasLetters)
                builder.Append(letterDict);
            if (passwordRequest.HasSpecialSymbols)
                builder.Append(symbolicDict);
            return builder.ToString().ToCharArray();
        }
    }
}