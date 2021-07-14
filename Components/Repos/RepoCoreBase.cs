/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LundbeckConsulting.Components.Core.Components.Repos
{
    public interface IRepoCoreBase : IRepoBase
    {
        /// <summary>
        /// The IP address based on current HTTP context
        /// </summary>
        IIPAddressCustom CurrentIP { get; }
        
        /// <summary>
        /// Accessor to current HTTP context
        /// </summary>
        IHttpContextAccessor HttpContext { get; }
                
        /// <summary>
        /// Current configuration
        /// </summary>
        IConfiguration Configuration { get; }
    }

    /// <summary>
    /// Base class for repo's used in Core components
    /// </summary>
    public abstract class RepoCoreBase : RepoBase, IRepoCoreBase
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _config;

        protected RepoCoreBase(IHttpContextAccessor httpContext, IConfiguration config)
        {
            _httpContext = httpContext;
            _config = config;
        }

        public IIPAddressCustom CurrentIP => new IPAddressCustom();

        public IHttpContextAccessor HttpContext => _httpContext;

        public IConfiguration Configuration => _config;
    }
}
