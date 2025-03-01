using AutoMapper;
using Azure.Core;
using LT.api.Application.Handlers.Base;
using LT.core.RabbitMQSender;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.messageBus;
using LT.model;
using LT.model.Commands.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LT.api.Application.Handlers
{
    public class ScoreQueryHandler(
            ILTRepository<EntityScore> dal,
            IMapper mapper,
            ILTUnitOfWork unitOfWork,
            IHttpRepository<EntityScoreDto> httpRepository,
            IRabbitMQMessageSender rabbitMQScoreMessageSender,
            IConfiguration configuration,
            ILogger<ScoreQueryHandler> logger
        ) :
        BaseHandler<EntityScore, EntityScoreDto>(dal, mapper, unitOfWork, logger),
        IRequestHandler<InsertServiceBusCommand<EntityScoreDto>>,
        IRequestHandler<GetHttpQuery<EntityScoreDto>, EntityScoreDto>,
        IRequestHandler<GetScoreQuery, Result<IEnumerable<EntityScoreDto>, EntityErrorResponseDto<string>>>
    {
        private readonly ILTRepository<EntityScore> _dal = dal;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpRepository<EntityScoreDto> _httpRepository = httpRepository;
        private readonly IRabbitMQMessageSender _rabbitMQScoreMessageSender = rabbitMQScoreMessageSender;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<ScoreQueryHandler> _logger = logger;

        public async Task Handle(InsertServiceBusCommand<EntityScoreDto> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(InsertServiceBusCommand<EntityScoreDto>)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var queueName = _configuration.GetSection("RabbitMQSettings:ScoreQueueName").Value;
            _rabbitMQScoreMessageSender.SendMessage(request.GetEntity(), queueName!);
            await Task.CompletedTask;
        }
        public async Task<Result<IEnumerable<EntityScoreDto>, EntityErrorResponseDto<string>>> Handle(GetScoreQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetScoreQuery)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var entities = await _dal.GetAllAsync(cancellationToken);
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (entities.Count() == 0)
                return new EntityErrorResponseDto<string>($"Entity not found");
            var returnEntities = entities.Select(e => _mapper.Map<EntityScoreDto>(e)).ToList();
            return returnEntities;
        }
        public async Task<EntityScoreDto> Handle(GetHttpQuery<EntityScoreDto> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GetHttpQuery<EntityScoreDto>)} - {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var entity = await _httpRepository.GetAsync(request.GetId());
            return entity;
        }
    }
}
