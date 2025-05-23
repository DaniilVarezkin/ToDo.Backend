using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Statistics.Queries.DailyTaskStatistics;
using ToDo.Application.Statistics.Queries.GlobalTaskStatistics;

namespace ToDo.WebApi.Controllers
{
    /// <summary xml:lang="en">
    /// Provides endpoints for retrieving task completion statistics.
    /// </summary>
    /// <summary xml:lang="ru">
    /// Предоставляет методы для получения статистики по выполненным задачам.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StatisticsController : BaseController
    {
        private readonly IMapper _mapper;

        /// <summary xml:lang="en">
        /// Initializes a new instance of <see cref="StatisticsController"/>.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Конструктор <see cref="StatisticsController"/>.
        /// </summary>
        /// <param name="mapper" xml:lang="en">AutoMapper instance.</param>
        /// <param name="mapper" xml:lang="ru">Экземпляр AutoMapper.</param>
        public StatisticsController(IMapper mapper) => _mapper = mapper;

        /// <summary xml:lang="en">
        /// Retrieves overall statistics for the user's tasks.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получает общую статистику по задачам пользователя.
        /// </summary>
        /// <remarks xml:lang="en">
        /// Sample request:
        /// GET api/statistics/getgloabal
        /// </remarks>
        /// <returns xml:lang="en">Returns <see cref="TaskStatisticsVm"/>.</returns>
        /// <returns xml:lang="ru">Возвращает <see cref="TaskStatisticsVm"/>.</returns>
        /// <response code="200" xml:lang="en">Success.</response>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="en">If the user is unauthorized.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TaskStatisticsVm>> GetGlobal()
        {
            var query = new GetTaskStatisticsQuery
            {
                UserId = UserId
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary xml:lang="en">
        /// Retrieves daily task completion statistics for the past given number of days.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получает ежедневную статистику завершения задач за указанный период.
        /// </summary>
        /// <remarks xml:lang="en">
        /// Sample request:
        /// GET api/statistics/getdaily?days=7
        /// </remarks>
        /// <param name="days" xml:lang="en">Number of days to include (default is 7).</param>
        /// <param name="days" xml:lang="ru">Количество дней для статистики (по умолчанию 7).</param>
        /// <returns xml:lang="en">Returns <see cref="DailyTaskStatisticsVm"/>.</returns>
        /// <returns xml:lang="ru">Возвращает <see cref="DailyTaskStatisticsVm"/>.</returns>
        /// <response code="200" xml:lang="en">Success.</response>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="en">If the user is unauthorized.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DailyTaskStatisticsVm>> GetDaily(
            [FromQuery] int days = 7)
        {
            var query = new GetDailyTaskStatisticsQuery
            {
                UserId = UserId,
                Days = days
            };

            var results = await Mediator.Send(query);
            return Ok(results);
        }
    }
}