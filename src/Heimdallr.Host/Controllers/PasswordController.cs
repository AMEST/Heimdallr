using Heimdallr.Security;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Heimdallr.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        /// <summary>
        /// Generate password form request
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">PasswordRequest validation failed</response>
        [HttpPost]
        public async Task<SecurityResult> Post([FromBody] SecurityRequest passwordRequest)
        {
            return await _passwordService.Generate(passwordRequest);
        }

    }
}
