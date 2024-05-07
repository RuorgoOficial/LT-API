using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LT.model;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LT.model.Commands.Queries;
using System.Threading;
using MediatR;
using Newtonsoft.Json.Linq;

namespace Identity.api.Controllers
{
    [ApiController]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
        {
            var query = new AuthenticateQuery(model);
            var response = await _mediator.Send(query, cancellationToken);

            return response.Match<IActionResult>(
                m => CreatedAtAction(
                    nameof(Login),
                    model,
                    Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(m),
                        expiration = m.ValidTo
                    })
                ),
                failed => BadRequest(failed)
                );

        }

    }
}
