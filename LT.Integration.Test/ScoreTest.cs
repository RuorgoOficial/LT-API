using AutoFixture;
using FluentAssertions;
using LT.model.Commands.Queries;
using LT.model;
using Microsoft.AspNetCore.Mvc.Testing;
using LT.core;
using LT.dal.Context;
using MediatR;
using Moq;
using System.Threading;
using Microsoft.AspNetCore.Components.Forms;

using AutoMapper;
using LT.api.Configure;
using Microsoft.EntityFrameworkCore;
using LT.core.RabbitMQSender;
using System.Net.Http.Json;
using Newtonsoft.Json;
using static Dapper.SqlMapper;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions.Common;

namespace LT.Integration.Test
{
    [Obsolete]
    [Collection(Constants.TEST_COLLECTION)]
    public class ScoreTest : IClassFixture<LTWebApplicationFactory>
    {
        private readonly DatabaseFixture _database;
        private readonly LTWebApplicationFactory _factory;
        private readonly Fixture _fixture;
        [Obsolete]
        private readonly ScoreCore _scoreCore;
        private readonly IMapper _mapper;

        [Obsolete]
        public ScoreTest(DatabaseFixture database,
            LTWebApplicationFactory factory)
        {
            _database = database;
            _factory = factory;
            _fixture = new Fixture();

            var dbContext = _database.CreateDBContext();
            _scoreCore = new ScoreCore(new dal.Access.BaseDal<EntityScore>(dbContext), new LTUnitOfWork(dbContext));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

        }
        [Fact]
        public async Task TestInsert_Score()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));

                var entityDto = _fixture.Build<EntityScoreDto>().Without(x => x.Id)
                    .With(x => x.Score, 9.1M)
                    .With(x => x.Acronym, "RUB")
                    .With(x => x.CreatedTimestamp, DateTime.UtcNow)
                    .With(x => x.UpdatedTimestamp, DateTime.UtcNow)
                    .Create();


                var insertCommand = new InsertCommand<EntityScoreDto>(entityDto);
                var insertResponseId = await _mediator.Send(insertCommand, timeout.Token);

                var ok = insertResponseId > 0;
                ok.Should().BeTrue();
            }
        }
        [Fact]
        public async Task TestInsertAndGet_Score()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));

                var entityDto = _fixture.Build<EntityScoreDto>().Without(x => x.Id)
                    .With(x => x.Score, 9.1M)
                    .With(x => x.Acronym, "RUB")
                    .With(x => x.CreatedTimestamp, DateTime.UtcNow)
                    .With(x => x.UpdatedTimestamp, DateTime.UtcNow)
                .Create();

                var insertCommand = new InsertCommand<EntityScoreDto>(entityDto);
                var insertResponseId = await _mediator.Send(insertCommand, timeout.Token);

                var getQuery = new GetQueryById<EntityScoreDto>(insertResponseId);
                var getResponse = await _mediator.Send(getQuery, timeout.Token);

                getResponse.Id.Should().BeGreaterThan(0);
            }
        }
        [Fact]
        public void TestInsert_Score_Through_Core()
        {
            var entity = _fixture.Build<EntityScore>().Without(x => x.Id)
                .With(x => x.Score, 9.1M)
                .With(x => x.Acronym, "RUB")
                .With(x => x.CreatedTimestamp, DateTime.UtcNow)
                .With(x => x.UpdatedTimestamp, DateTime.UtcNow)
                .Create();

            var id = _scoreCore.Insert(entity);

            var ok = id > 0;

            ok.Should().BeTrue();
        }
    }
}