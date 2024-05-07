using AutoMapper;
using Azure.Core;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.model;
using LT.model.Commands.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LT.core.Handlers.Score
{
    public class AuthenticateQueryHandler(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppSettings appSettings) : 
        IRequestHandler<AuthenticateQuery, Result<JwtSecurityToken, EntityErrorDto>>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly AppSettings _appSettings = appSettings;

        

        public async Task<Result<JwtSecurityToken, EntityErrorDto>> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.GetEntity().Username!);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.GetEntity().Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = CreateToken(authClaims);

                return token;
            }
            var threadId = Thread.CurrentThread.ManagedThreadId;
            return new EntityErrorDto(LogLevel.Information.ToString(), threadId, 0, nameof(Handle), $"Unauthorized", null, null);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.GetKey()!));

            var token = new JwtSecurityToken(
                issuer: _appSettings.GetIssuer(),
                audience: _appSettings.GetAudience(),
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
