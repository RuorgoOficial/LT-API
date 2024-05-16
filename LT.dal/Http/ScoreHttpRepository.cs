using LT.dal.Abstractions;
using LT.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal
{
    public class ScoreHttpRepository(HttpClient httpClient) : IHttpRepository<EntityScoreDto>
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<EntityScoreDto> GetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/score/{id}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<EntityScoreDto>(apiContent);
            if(resp is null)
                return new EntityScoreDto();
            return resp;
        }
    }
}
