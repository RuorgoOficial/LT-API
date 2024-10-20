using AutoFixture;
using LT.dal.Abstractions;
using LT.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.Integration.Test.Mocks
{
    public class ScoreHttpRepositoryMock(HttpClient httpClient) : IHttpRepository<EntityScoreDto>
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<EntityScoreDto> GetAsync(int id)
        {
            Fixture fixture = new Fixture();
            var resp = fixture.Build<EntityScoreDto>()
                .With(x => x.Id, id)
                .With(x => x.Score, 9.1M)
                .With(x => x.Acronym, "RUB")
                .With(x => x.CreatedTimestamp, DateTime.UtcNow)
                .With(x => x.UpdatedTimestamp, DateTime.UtcNow)
                .Create();

            await Task.CompletedTask;

            return resp;
        }
    }
}
