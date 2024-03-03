using LT.core;
using LT.dal.Context;
using LT.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LT.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ILogger<ScoreController> _logger;
        private readonly LTContext _context;
        private readonly ScoreCore _core;

        public ScoreController(ILogger<ScoreController> logger, LTContext context)
        {
            _logger = logger;
            _context = context;
            _core = new ScoreCore(_context);
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