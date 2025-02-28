using AutoFixture;
using AutoMapper;
using Castle.Core.Logging;
using LT.api.Application.Handlers;
using LT.core.RabbitMQSender;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.messageBus;
using LT.model;
using LT.model.Commands.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace LT.Application.Unit.Test
{
    public class ScoreQueryHandlerTest
    {
        private readonly ScoreQueryHandler _scoreQueryHandler;
        private readonly Mock<ILTRepository<EntityScore>> _baseDal;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILTUnitOfWork> _unitOfWork;
        private readonly Mock<IMessageBus> _messageBus;
        private readonly Mock<IHttpRepository<EntityScoreDto>> _httpRepository;
        private readonly Mock<IRabbitMQMessageSender> _rabbitMQScoreMessageSender;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<ILogger<ScoreQueryHandler>> _logger;

        private readonly Fixture _fixture;

        public ScoreQueryHandlerTest()
        {
            _fixture = new Fixture();

            _baseDal = new Mock<ILTRepository<EntityScore>>();
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<ILTUnitOfWork>();
            _messageBus = new Mock<IMessageBus>();
            _httpRepository = new Mock<IHttpRepository<EntityScoreDto>>();
            _rabbitMQScoreMessageSender = new Mock<IRabbitMQMessageSender>();
            _configuration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<ScoreQueryHandler>>();
            

            _scoreQueryHandler = new ScoreQueryHandler(
                _baseDal.Object, 
                _mapper.Object, 
                _unitOfWork.Object, 
                _messageBus.Object,
                _httpRepository.Object,
                _rabbitMQScoreMessageSender.Object,
                _configuration.Object,
                _logger.Object
            );
        }

        [Fact]
        public async Task Handle_OnInsertCommand_ShouldBeInsertedAsync()
        {
            //Arrange
            var cancellationToken = new CancellationToken();
            var entityScoreDto = _fixture
                .Build<EntityScoreDto>()
                .Create();
            var command = new InsertCommand<EntityScoreDto>(entityScoreDto);

            //Act
            await _scoreQueryHandler.Handle(command, cancellationToken);

            //Assert
            _baseDal.Verify(x => x.Add(It.IsAny<EntityScore>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
