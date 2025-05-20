namespace ToDo.WebApi.Middleware
{
    /// <summary>
    /// Расширения для подключения настраиваемого обработчика исключений в конвейер обработки запросов.
    /// </summary>
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Включает в конвейер middleware для глобальной обработки исключений.
        /// </summary>
        /// <param name="app">Интерфейс для настройки конвейера обработки HTTP-запросов.</param>
        /// <returns>Тот же <see cref="IApplicationBuilder"/>, расширенный middleware-обработчиком исключений.</returns>
        public static IApplicationBuilder UseCustomExceptionHandler(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
