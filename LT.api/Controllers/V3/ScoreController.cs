using Asp.Versioning;
using AutoMapper;
using LT.api.Contracts;
using LT.api.Metrics;
using LT.core;
using LT.core.Handlers.Score;
using LT.dal;
using LT.dal.Context;
using LT.model;
using LT.model.Commands.Queries;
using LT.model.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection;
using System.Threading;

namespace LT.api.Controllers.V3
{
    [ApiController]    
    [ApiVersion(3)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly GetMetrics _metrics;
        private readonly IMediator _mediator;

        public ScoreController(
            GetMetrics metrics, 
            IMediator mediator)
        {
            _metrics = metrics;
            _mediator = mediator;
        }

        //Get
        [MapToApiVersion(3)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());

            var query = new GetScoreQuery();
            var response = await _mediator.Send(query, cancellationToken);

            return response.Match<IActionResult>(
                m => CreatedAtAction(nameof(Get), null, ResponseBuilder.Build(m)),
                failed => BadRequest(ResponseBuilder.Build(failed))
                );
        }

        [MapToApiVersion(3)]
        [HttpPost]
        public async Task<bool> Insert(EntityScoreDto entity, CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());
            var query = new InsertServiceBusCommand<EntityScoreDto>(entity);
            await _mediator.Send(query, cancellationToken);
            return true;
        }
    }
}