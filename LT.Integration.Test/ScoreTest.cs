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
using LT.core.Handlers.Score;
using AutoMapper;
using LT.api.Configure;
using Microsoft.EntityFrameworkCore;
using LT.core.Handlers;
using LT.core.RabbitMQSender;

namespace LT.Integration.Test
{
    [Collection(Constants.TEST_COLLECTION)]
    public class ScoreTest : IClassFixture<LTWebApplicationFactory>
    {
        private readonly DatabaseFixture _database;
        private readonly LTWebApplicationFactory _factory;
        private readonly Fixture _fixture;
        private readonly ScoreCore _scoreCore;
        private readonly IMapper _mapper;

        public ScoreTest(DatabaseFixture database, LTWebApplicationFactory factory)
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
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var _ = _factory.CreateClient(new WebApplicationFactoryClientOptions());

            var entity = _fixture.Build<EntityScore>().Without(x => x.Id)
                .With(x => x.Score, 9.1M)
                .With(x => x.Acronym, "RUB")
                .With(x => x.CreatedTimestamp, DateTime.UtcNow)
                .With(x => x.UpdatedTimestamp, DateTime.UtcNow)
                .Create();

            var id = await _database.EnsureAsync(entity);

            var ok = id > 0;

            ok.Should().BeTrue();
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