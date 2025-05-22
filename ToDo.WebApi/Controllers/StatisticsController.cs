using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Statistics.Queries;

namespace ToDo.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StatisticsController : BaseController
    {
        private readonly IMapper _mapper;
        public StatisticsController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<TaskStatisticsVm>> GetGloabalStatistic()
        {
            var query = new GetTaskStatisticsQuery
            {
                UserId = UserId
            };

            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
