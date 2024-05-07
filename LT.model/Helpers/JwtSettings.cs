using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices.Marshalling;

namespace LT.model
{
    public class JwtSettings
    {
        protected readonly string? _Issuer;
        protected readonly string? _Audience;
        protected readonly string? _Key;
        public JwtSettings(IConfiguration configuration)
        {
            _Issuer = configuration["JwtSettings:Issuer"];
            _Audience = configuration["JwtSettings:Audience"];
            _Key = configuration["JwtSettings:Key"];
        }

        public string? GetIssuer()
        {
            return _Issuer;
        }
        public string? GetAudience()
        {
            return _Audience;
        }
        public string? GetKey()
        {
            return _Key;
        }


    }
}