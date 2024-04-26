using Asp.Versioning;
using AutoMapper;
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

namespace LT.api.Controllers.V2
{
    [Authorize]
    [ApiController]    
    [ApiVersion(2)]
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
        [MapToApiVersion(2)]
        [HttpGet]
        public async Task<IEnumerable<EntityScoreDto>> Get(CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());

            var query = new GetScoreQuery();
            return await _mediator.Send(query, cancellationToken);
        }

        [MapToApiVersion(2)]
        [HttpGet("{id:int}")]
        public async Task<IEnumerable<EntityScoreDto>> GetById(int id, CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());

            var query = new GetScoreQuery();
            return await _mediator.Send(query, cancellationToken);
        }

        [MapToApiVersion(2)]
        [HttpPost]
        public async Task<int> Insert(EntityScoreDto entity, CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());
            var query = new InsertScoreQuery(entity);
            return await _mediator.Send(query, cancellationToken);
        }
        [MapToApiVersion(2)]
        [HttpPut]
        public async Task<int> Update(EntityScoreDto entity, CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());

            var query = new UpdateScoreQuery(entity);
            return await _mediator.Send(query, cancellationToken);
        }
        [MapToApiVersion(2)]
        [HttpDelete]
        public async Task<int> Delete(EntityScoreDto entity, CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());

            var query = new DeleteScoreQuery(entity);
            return await _mediator.Send(query, cancellationToken);
        }
        [MapToApiVersion(2)]
        [HttpPatch]
        public async Task<int> Patch(EntityScoreDto entity, CancellationToken cancellationToken)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());

            var query = new UpdateScoreQuery(entity);
            return await _mediator.Send(query, cancellationToken);
        }
    }
}