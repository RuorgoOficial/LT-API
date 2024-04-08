using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices.Marshalling;

namespace LT.model
{
    public class ConnectionStrings
    {
        protected readonly string? _LTContext;
        protected readonly string? _IdentityDbContext;
        public ConnectionStrings(IConfiguration configuration)
        {
            _LTContext = configuration.GetConnectionString("LTContext");
            _IdentityDbContext = configuration.GetConnectionString("IdentityDbContext");
        }

        public string? GetLTContext()
        {
            return _LTContext;
        }
        public string? GetIdentityDbContext()
        {
            return _IdentityDbContext;
        }


    }
}