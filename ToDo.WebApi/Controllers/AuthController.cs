using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDo.Persistance.Identity;
using ToDo.Persistance.Services;
using ToDo.WebApi.Models.Auth;

namespace ToDo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        /// <summary xml:lang="en">
        /// Registers a new user.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="registerDto" xml:lang="en">RegisterDto containing Username, Email and Password.</param>
        /// <param name="registerDto" xml:lang="ru">Данные для регистрации: имя пользователя, email и пароль.</param>
        /// <returns xml:lang="en">Returns a success message upon creation.</returns>
        /// <returns xml:lang="ru">Возвращает сообщение об успешном создании пользователя.</returns>
        /// <response code="200" xml:lang="en">User created successfully.</response>
        /// <response code="200" xml:lang="ru">Пользователь успешно создан.</response>
        /// <response code="400" xml:lang="en">Validation error or creation failed.</response>
        /// <response code="400" xml:lang="ru">Ошибка валидации или создание пользователя не удалось.</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            return Ok(new { message = "User created successfully." });
        }

        /// <summary xml:lang="en">
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Аутентифицирует пользователя и возвращает JWT-токен.
        /// </summary>
        /// <param name="loginDto" xml:lang="en">LoginDto containing Email and Password.</param>
        /// <param name="loginDto" xml:lang="ru">Данные для входа: email и пароль.</param>
        /// <returns xml:lang="en">Returns a JWT token string.</returns>
        /// <returns xml:lang="ru">Возвращает строку с JWT-токеном.</returns>
        /// <response code="200" xml:lang="en">Authentication successful.</response>
        /// <response code="200" xml:lang="ru">Аутентификация прошла успешно.</response>
        /// <response code="401" xml:lang="en">Invalid credentials.</response>
        /// <response code="401" xml:lang="ru">Неверные учетные данные.</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized(new { error = "Invalid username or password." });

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordValid)
                return Unauthorized(new { error = "Invalid username or password." });

            var token = _jwtService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
