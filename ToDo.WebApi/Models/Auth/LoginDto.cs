using System.ComponentModel.DataAnnotations;

namespace ToDo.WebApi.Models.Auth
{
    /// <summary>
    /// DTO для входа пользователя.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; } = null!;
    }
}
