using System.Threading.Tasks;

namespace Heimdallr.Security;

public interface IPasswordService
{
    /// <summary>
    /// Generate password from request
    /// </summary>
    Task<SecurityResult> Generate(SecurityRequest passwordRequest);
}