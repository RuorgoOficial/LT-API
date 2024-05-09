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
                return await CreateToken(user);
            }
            return CreateErrorResponse();
        }
        private async Task<JwtSecurityToken> CreateToken(IdentityUser user)
        {
            var authClaims = GetInitialClaims(user.UserName!);

            authClaims.AddRange(await GetClaimsByRole(user));

            var token = CreateToken(authClaims);

            return token;
        }
        private List<Claim> GetInitialClaims(string userName)
        {
            return new List<Claim>
                {
                    new(ClaimTypes.Name, userName),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
        }
        private async Task<List<Claim>> GetClaimsByRole(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>();
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            return claims;
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
        private EntityErrorDto CreateErrorResponse()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            return new EntityErrorDto(LogLevel.Information.ToString(), threadId, 0, nameof(Handle), $"Unauthorized", null, null);
        }
    }
}
