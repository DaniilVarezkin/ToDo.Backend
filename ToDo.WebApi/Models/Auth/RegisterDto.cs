using System.ComponentModel.DataAnnotations;

namespace ToDo.WebApi.Models.Auth
{
    /// <summary>
    /// DTO для регистрации пользователя.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Имя пользователя (логин).
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Пароль для аккаунта.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }
}
