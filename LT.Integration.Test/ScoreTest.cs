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
        //[Fact]
        //public async Task TestInsert_Score()
        //{
        //    using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        //    var client = _factory.CreateClient(new WebApplicationFactoryClientOptions());

        //    var entityDto = _fixture.Build<EntityScoreDto>().Without(x => x.Id)
        //        .With(x => x.Score, 9.1M)
        //        .With(x => x.Acronym, "RUB")
        //        .With(x => x.CreatedTimestamp, DateTime.UtcNow)
        //        .With(x => x.UpdatedTimestamp, DateTime.UtcNow)
        //        .Create();

        //    var response = await client.PostAsJsonAsync("api/v2/score", entityDto);
        //    response.EnsureSuccessStatusCode();

        //    string idString = (await response.Content.ReadAsStringAsync()) ?? string.Empty;
        //    var id = 0;

        //    var success = int.TryParse(idString, out id);
        //    success.Should().BeTrue();

        //    var ok = id > 0;
        //    ok.Should().BeTrue();
        //}
        //[Fact]
        //public async Task TestInsertAndGet_Score()
        //{
        //    using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        //    var client = _factory.CreateClient(new WebApplicationFactoryClientOptions());

        //    var entityDto = _fixture.Build<EntityScoreDto>().Without(x => x.Id)
        //        .With(x => x.Score, 9.1M)
        //        .With(x => x.Acronym, "RUB")
        //        .With(x => x.CreatedTimestamp, DateTime.UtcNow)
        //        .With(x => x.UpdatedTimestamp, DateTime.UtcNow)
        //        .Create();

        //    var insertResponse = await client.PostAsJsonAsync("api/v2/score", entityDto);
        //    insertResponse.EnsureSuccessStatusCode();

        //    var response = await client.GetAsync("api/v2/score");
        //    response.EnsureSuccessStatusCode();

        //    var responseString = (await response.Content.ReadAsStringAsync());
        //    var scores = JsonConvert.DeserializeObject<IEnumerable<EntityScoreDto>>(responseString);
        //    scores.Should().HaveCountGreaterThan(0);
        //}

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