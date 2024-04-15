using Asp.Versioning;
using AutoMapper;
using LT.api.Metrics;
using LT.core;
using LT.dal;
using LT.dal.Context;
using LT.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection;

namespace LT.api.Controllers.V1
{
    //[Authorize]
    [ApiController]
    [ApiVersion(1, Deprecated = true)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ILogger<ScoreController> _logger;
        private readonly AppSettings _appSettings;
        private readonly ScoreCore _core;
        private readonly GetMetrics _metrics;
        private readonly IMapper _mapper;

        public ScoreController(ILogger<ScoreController> logger, AppSettings appSettings, ScoreCore core, GetMetrics metrics, IMapper mapper)
        {
            _logger = logger;
            _appSettings = appSettings;
            _core = core;
            _metrics = metrics;
            _mapper = mapper;
        }

        //Get
        [MapToApiVersion(1)]
        [HttpGet]
        public IEnumerable<EntityScoreDto> Get()
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());
            return _core.Get().Select(e => _mapper.Map<EntityScoreDto>(e));
        }

        [HttpPost]
        [MapToApiVersion(1)]
        public int Insert(EntityScoreDto test)
        {
            var testCoreEntity = _mapper.Map<EntityScore>(test);
            return _core.Insert(testCoreEntity);
        }
        [HttpPut]
        [MapToApiVersion(1)]
        public int Update(EntityScoreDto test)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());
            return 0;
        }
        [HttpDelete]
        [MapToApiVersion(1)]
        public int Delete(EntityScoreDto test)
        {
            return 0;
        }
        [HttpPatch]
        [MapToApiVersion(1)]
        public int Patch(EntityScoreDto test)
        {
            return 0;
        }
    }
}