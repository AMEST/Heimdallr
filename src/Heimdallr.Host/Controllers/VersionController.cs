using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Heimdallr.Host.Controllers
{
    /// <summary>
    /// Version API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        /// <summary>
        /// Get version
        /// </summary>
        /// <response code="200">Version</response>
        [HttpGet]
        public IActionResult Get()
        {
            var version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            return new ObjectResult(new { Version = version }) { StatusCode = 200 };
        }
    }
}