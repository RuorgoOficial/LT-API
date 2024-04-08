using LT.core;
using LT.dal.Context;
using LT.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LT.api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ILogger<ScoreController> _logger;
        private readonly AppSettings _appSettings;
        private readonly ScoreCore _core;

        public ScoreController(ILogger<ScoreController> logger, AppSettings appSettings, ScoreCore core)
        {
            _logger = logger;
            _appSettings = appSettings;
            _core = core;
        }

        //Get
        [HttpGet]
        public IEnumerable<EntityScore> Get()
        {
            return _core.Get();
        }
        [HttpPost]
        public int Insert(EntityScore test)
        {
            return _core.Insert(test);
        }
    }
}