using System.Security.Cryptography;
using System.Text;

namespace Heimdallr.Security
{
    public class MasterPasswordNumberGenerator: RandomNumberGenerator
    {
        private readonly string _masterPassword;
        public MasterPasswordNumberGenerator(string masterPassword)
        {
            _masterPassword = masterPassword;
        }

        public override void GetBytes(byte[] data)
        {
            var fixedMasterPassword = Expand(_masterPassword, data.Length);
            var bytes = Encoding.UTF8.GetBytes(fixedMasterPassword);
            for (var i = 0; i < data.Length; i++)
                data[i] = bytes[i];
        }

        private static string Expand(string value, int length)
        {
            var chars = new char[length];

            for (var index = 0; (index < length); index++)
            {
                chars[index] = value[(index % value.Length)];
            }

            return new string(chars);
        }
    }
}