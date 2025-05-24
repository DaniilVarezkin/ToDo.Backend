using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Statistics.Queries.DailyTaskStatistics;
using ToDo.Application.Statistics.Queries.GlobalTaskStatistics;

namespace ToDo.WebApi.Controllers
{
    /// <summary xml:lang="ru">
    /// Предоставляет методы для получения статистики по выполненным задачам.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/statistics/[action]")]
    public class StatisticsController : BaseController
    {
        private readonly IMapper _mapper;

        /// <summary xml:lang="ru">
        /// Конструктор <see cref="StatisticsController"/>.
        /// </summary>
        /// <param name="mapper" xml:lang="ru">Экземпляр AutoMapper.</param>
        public StatisticsController(IMapper mapper) => _mapper = mapper;

        /// <summary xml:lang="ru">
        /// Получает общую статистику по задачам пользователя.
        /// </summary>
        /// <remarks xml:lang="en">
        /// Пример запроса:
        /// GET api/statistics/getglobal
        /// </remarks>
        /// <returns xml:lang="ru">Возвращает <see cref="TaskStatisticsVm"/>.</returns>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet]
        [ProducesResponseType(typeof(TaskStatisticsVm),StatusCodes.Status200OK)]
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

        /// <summary xml:lang="ru">
        /// Получает ежедневную статистику завершения задач за указанный период.
        /// </summary>
        /// <remarks xml:lang="en">
        /// Пример запроса:
        /// GET api/statistics/getdaily?days=7
        /// </remarks>
        /// <param name="Days" xml:lang="ru">Количество дней для статистики (по умолчанию 7).</param>
        /// <returns xml:lang="ru">Возвращает <see cref="DailyTaskStatisticsVm"/>.</returns>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DailyTaskStatisticsVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DailyTaskStatisticsVm>> GetDaily(
            [FromQuery] int Days = 7)
        {
            var query = new GetDailyTaskStatisticsQuery
            {
                UserId = UserId,
                Days = Days
            };

            var results = await Mediator.Send(query);
            return Ok(results);
        }
    }
}
