using System.Threading.Tasks;

namespace Heimdallr.Security
{
    public interface IPasswordService
    {
        Task<SecurityResult> Generate(SecurityRequest passwordRequest);
    }
}