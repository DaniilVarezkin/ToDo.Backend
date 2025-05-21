using AutoMapper;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.Common.Mapping;
using ToDo.Application.TaskItems.Queries.GetTaskItemList;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.WebApi.Models.TaskItems
{
    /// <summary>
    /// DTO для получения списка элементов задач с параметрами фильтрации, сортировки и пагинации.
    /// </summary>
    public class GetTaskItemListQueryDto : IMapped
    {
        /// <summary>
        /// Номер страницы (начиная с 1).
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Размер страницы (количество элементов на странице).
        /// </summary>
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Фильтрация по статусу задачи.
        /// </summary>
        public UserTaskStatus? Status { get; set; }

        /// <summary>
        /// Фильтрация по приоритету задачи.
        /// </summary>
        public TaskPriority? Priority { get; set; }

        /// <summary>
        /// Фильтрация по флагу: задача на весь день.
        /// </summary>
        public bool? IsAllDay { get; set; }

        /// <summary>
        /// Фильтрация по дате начала (StartDate >= DateFrom).
        /// </summary>
        public DateTimeOffset? DateFrom { get; set; }

        /// <summary>
        /// Фильтрация по дате окончания (EndDate <= DateTo).
        /// </summary>
        public DateTimeOffset? DateTo { get; set; }

        /// <summary>
        /// Поиск по ключевым словам в заголовке или описании (не более 100 символов).
        /// </summary>
        [MaxLength(100)]
        public string? Search { get; set; }

        /// <summary>
        /// Поле для сортировки (например, "StartDate", "Priority").
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Порядок сортировки: true — по убыванию, false — по возрастанию.
        /// </summary>
        public bool SortDescending { get; set; }

        /// <summary>
        /// Настраивает маппинг из этого DTO в команду <see cref="GetTaskItemListQuery"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для конфигурации.</param>
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<GetTaskItemListQueryDto, GetTaskItemListQuery>()
                .ForMember(q => q.Page, opt => opt.MapFrom(dto => dto.Page))
                .ForMember(q => q.PageSize, opt => opt.MapFrom(dto => dto.PageSize))
                .ForMember(q => q.Status, opt => opt.MapFrom(dto => dto.Status))
                .ForMember(q => q.Priority, opt => opt.MapFrom(dto => dto.Priority))
                .ForMember(q => q.IsAllDay, opt => opt.MapFrom(dto => dto.IsAllDay))
                .ForMember(q => q.DateFrom, opt => opt.MapFrom(dto => dto.DateFrom))
                .ForMember(q => q.DateTo, opt => opt.MapFrom(dto => dto.DateTo))
                .ForMember(q => q.Search, opt => opt.MapFrom(dto => dto.Search))
                .ForMember(q => q.SortBy, opt => opt.MapFrom(dto => dto.SortBy))
                .ForMember(q => q.SortDescending, opt => opt.MapFrom(dto => dto.SortDescending));
        }
    }
}