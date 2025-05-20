using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ToDo.WebApi.Controllers
{
    /// <summary>
    /// Базовый контроллер для всех API-контроллеров приложения.
    /// Предоставляет доступ к MediatR и текущему идентификатору пользователя.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Ленивая инициализация MediatR через DI-контейнер.
        /// </summary>
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        /// <summary>
        /// Идентификатор текущего авторизованного пользователя.
        /// </summary>
        /// <remarks>
        /// Возвращает <see cref="Guid.Empty"/>, если пользователь не аутентифицирован.
        /// </remarks>
        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
    }
}
