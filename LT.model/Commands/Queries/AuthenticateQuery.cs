using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.model.Commands.Queries
{
    public class AuthenticateQuery : IRequest<Result<JwtSecurityToken, EntityErrorDto>>
    {
        private readonly LoginModel _entityDto;
        public AuthenticateQuery(LoginModel entityDto)
        {
            _entityDto = entityDto;
        }

        public LoginModel GetEntity() => _entityDto;
    }
}
