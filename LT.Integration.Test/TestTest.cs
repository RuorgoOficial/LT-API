using AutoFixture;
using FluentAssertions;
using LT.model.Commands.Queries;
using LT.model;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LT.Integration.Test
{
    [Collection(Constants.TEST_COLLECTION)]
    public class TestTest : IClassFixture<LTWebApplicationFactory>
    {
        private readonly DatabaseFixture _database;
        private readonly LTWebApplicationFactory _factory;
        private readonly Fixture _fixture;

        public TestTest(DatabaseFixture database, LTWebApplicationFactory factory)
        {
            _database = database;
            _factory = factory;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task TestInsert_Test()
        {
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var entity = _fixture.Build<EntityTest>().Without(x => x.Id)
                .With(x => x.Description, "Description from test")
                .Create();

            var id = await _database.EnsureAsync<EntityTest>(entity);

            var ok = id > 0;

            ok.Should().BeTrue();
        }
    }
}