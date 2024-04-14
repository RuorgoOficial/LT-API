using Asp.Versioning;
using LT.api.Metrics;
using LT.core;
using LT.dal;
using LT.dal.Context;
using LT.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection;

namespace LT.api.Controllers.V2
{
    //[Authorize]
    [ApiController]    
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ILogger<ScoreController> _logger;
        private readonly AppSettings _appSettings;
        private readonly ScoreCore _core;
        private readonly GetMetrics _metrics;

        public ScoreController(ILogger<ScoreController> logger, AppSettings appSettings, ScoreCore core, GetMetrics metrics)
        {
            _logger = logger;
            _appSettings = appSettings;
            _core = core;
            _metrics = metrics;
        }

        //Get
        [MapToApiVersion(2)]
        [HttpGet]
        public IEnumerable<EntityScore> Get()
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());
            return _core.Get();
        }

        [MapToApiVersion(2)]
        [HttpPost]
        public int Insert(EntityScore test)
        {
            return _core.Insert(test);
        }
        [MapToApiVersion(2)]
        [HttpPut]
        public int Update(EntityScore test)
        {
            _metrics.GetCount(nameof(ScoreController), MethodBase.GetCurrentMethod());
            return 0;
        }
        [MapToApiVersion(2)]
        [HttpDelete]
        public int Delete(EntityScore test)
        {
            return 0;
        }
        [MapToApiVersion(2)]
        [HttpPatch]
        public int Patch(EntityScore test)
        {
            return 0;
        }
    }
}