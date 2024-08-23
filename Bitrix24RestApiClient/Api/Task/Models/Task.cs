#nullable enable
using Bitrix24RestApiClient.Core.Models.CrmTypes;
using Bitrix24RestApiClient.Core.Models.CrmTypes.CrmFile;
using G_Task;
using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Api.Task.Models;

/// <summary>
/// Task
/// </summary>
public class Task : IAbstractEntity
{
    /// <summary>
    /// ID
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.Id)]
    public int? Id {get; set;}
    /// <summary>
    /// ID базовой задачи
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ParentId)]
    public int? ParentId {get; set;}
    /// <summary>
    /// Название
    /// Тип: string
    /// </summary>
    [JsonProperty(TaskFields.Title)]
    public string? Title {get; set;}
    /// <summary>
    /// Описание
    /// Тип: string
    /// </summary>
    [JsonProperty(TaskFields.Description)]
    public string? Description {get; set;}
    /// <summary>
    /// Оценка
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.Mark)]
    public int? Mark {get; set;}
    /// <summary>
    /// Приоритет
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.Priority)]
    public int? Priority {get; set;}
    /// <summary>
    /// Статус
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.Status)]
    public int? Status {get; set;}
    /// <summary>
    /// Множественная задача
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.Multitask)]
    public int? Multitask {get; set;}
    /// <summary>
    /// 
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.NotViewed)]
    public int? NotViewed {get; set;}
    /// <summary>
    /// Повторяемая задача
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.Replicate)]
    public int? Replicate {get; set;}
    /// <summary>
    /// Проект
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.GroupId)]
    public int? GroupId {get; set;}
    /// <summary>
    /// Стадия
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.StageId)]
    public int? StageId {get; set;}
    /// <summary>
    /// Постановщик
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.CreatedBy)]
    public int? CreatedBy {get; set;}
    /// <summary>
    /// 
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.CreatedDate)]
    public DateTimeOffset? CreatedDate {get; set;}
    /// <summary>
    /// Исполнитель
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ResponsibleId)]
    public int? ResponsibleId {get; set;}
    /// <summary>
    /// 
    /// Тип: array
    /// </summary>
    [JsonProperty(TaskFields.Accomplices)]
    public List<object> Accomplices {get; set;}
    /// <summary>
    /// 
    /// Тип: array
    /// </summary>
    [JsonProperty(TaskFields.Auditors)]
    public List<object> Auditors {get; set;}
    /// <summary>
    /// Изменил
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ChangedBy)]
    public int? ChangedBy {get; set;}
    /// <summary>
    /// Дата изменения
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.ChangedDate)]
    public DateTimeOffset? ChangedDate {get; set;}
    /// <summary>
    /// Изменил статус
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.StatusChangedBy)]
    public int? StatusChangedBy {get; set;}
    /// <summary>
    /// Дата изменения статуса
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.StatusChangedDate)]
    public DateTimeOffset? StatusChangedDate {get; set;}
    /// <summary>
    /// Закрыл задачу
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ClosedBy)]
    public int? ClosedBy {get; set;}
    /// <summary>
    /// Дата закрытия
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.ClosedDate)]
    public DateTimeOffset? ClosedDate {get; set;}
    /// <summary>
    /// 
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.ActivityDate)]
    public DateTimeOffset? ActivityDate {get; set;}
    /// <summary>
    /// Дата начала
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.DateStart)]
    public DateTimeOffset? DateStart {get; set;}
    /// <summary>
    /// Крайний срок
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.Deadline)]
    public DateTimeOffset? Deadline {get; set;}
    /// <summary>
    /// Плановое начало
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.StartDatePlan)]
    public DateTimeOffset? StartDatePlan {get; set;}
    /// <summary>
    /// Плановое завершение
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.EndDatePlan)]
    public DateTimeOffset? EndDatePlan {get; set;}
    /// <summary>
    /// GUID
    /// Тип: string
    /// </summary>
    [JsonProperty(TaskFields.Guid)]
    public string? Guid {get; set;}
    /// <summary>
    /// XML_ID
    /// Тип: string
    /// </summary>
    [JsonProperty(TaskFields.XmlId)]
    public string? XmlId {get; set;}
    /// <summary>
    /// Кол-во комментариев
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.CommentsCount)]
    public int? CommentsCount {get; set;}
    /// <summary>
    /// 
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ServiceCommentsCount)]
    public int? ServiceCommentsCount {get; set;}
    /// <summary>
    /// 
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.NewCommentsCount)]
    public int? NewCommentsCount {get; set;}
    /// <summary>
    /// 
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.AllowChangeDeadline)]
    public int? AllowChangeDeadline {get; set;}
    /// <summary>
    /// 
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.AllowTimeTracking)]
    public int? AllowTimeTracking {get; set;}
    /// <summary>
    /// Принять работу
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.TaskControl)]
    public int? TaskControl {get; set;}
    /// <summary>
    /// Добавить в отчет
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.AddInReport)]
    public int? AddInReport {get; set;}
    /// <summary>
    /// Создано из шаблона
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.ForkedByTemplateId)]
    public int? ForkedByTemplateId {get; set;}
    /// <summary>
    /// Затраченое время
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.TimeEstimate)]
    public int? TimeEstimate {get; set;}
    /// <summary>
    /// Затраченое время из истории изменений
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.TimeSpentInLogs)]
    public int? TimeSpentInLogs {get; set;}
    /// <summary>
    /// Пропустить выходные дни
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.MatchWorkTime)]
    public int? MatchWorkTime {get; set;}
    /// <summary>
    /// FORUM_TOPIC_ID
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ForumTopicId)]
    public int? ForumTopicId {get; set;}
    /// <summary>
    /// FORUM_ID
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ForumId)]
    public int? ForumId {get; set;}
    /// <summary>
    /// SITE_ID
    /// Тип: string
    /// </summary>
    [JsonProperty(TaskFields.SiteId)]
    public string? SiteId {get; set;}
    /// <summary>
    /// Задача подчиненного
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.Subordinate)]
    public int? Subordinate {get; set;}
    /// <summary>
    /// 
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.Favorite)]
    public int? Favorite {get; set;}
    /// <summary>
    /// EXCHANGE_MODIFIED
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.ExchangeModified)]
    public DateTimeOffset? ExchangeModified {get; set;}
    /// <summary>
    /// EXCHANGE_ID
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.ExchangeId)]
    public int? ExchangeId {get; set;}
    /// <summary>
    /// OUTLOOK_VERSION
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.OutlookVersion)]
    public int? OutlookVersion {get; set;}
    /// <summary>
    /// Дата последнего просмотра
    /// Тип: datetime
    /// </summary>
    [JsonProperty(TaskFields.ViewedDate)]
    public DateTimeOffset? ViewedDate {get; set;}
    /// <summary>
    /// Индекс сортировки
    /// Тип: double
    /// </summary>
    [JsonProperty(TaskFields.Sorting)]
    public decimal? Sorting {get; set;}
    /// <summary>
    /// Затрачено (план)
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.DurationPlan)]
    public int? DurationPlan {get; set;}
    /// <summary>
    /// Затрачено (фактически)
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.DurationFact)]
    public int? DurationFact {get; set;}
    /// <summary>
    /// 
    /// Тип: array
    /// </summary>
    [JsonProperty(TaskFields.Checklist)]
    public List<object> Checklist {get; set;}
    /// <summary>
    /// DURATION_TYPE
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.DurationType)]
    public int? DurationType {get; set;}
    /// <summary>
    /// 
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.IsMuted)]
    public int? IsMuted {get; set;}
    /// <summary>
    /// 
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.IsPinned)]
    public int? IsPinned {get; set;}
    /// <summary>
    /// 
    /// Тип: enum
    /// </summary>
    [JsonProperty(TaskFields.IsPinnedInGroup)]
    public int? IsPinnedInGroup {get; set;}
    /// <summary>
    /// 
    /// Тип: integer
    /// </summary>
    [JsonProperty(TaskFields.FlowId)]
    public int? FlowId {get; set;}
    /// <summary>
    /// Элементы CRM
    /// Тип: crm
    /// </summary>
    [JsonProperty(TaskFields.UfCrmTask)]
    public object? UfCrmTask {get; set;}
    /// <summary>
    /// Load files
    /// Тип: disk_file
    /// </summary>
    [JsonProperty(TaskFields.UfTaskWebdavFiles)]
    public CrmFile? UfTaskWebdavFiles {get; set;}
    /// <summary>
    /// 
    /// Тип: mail_message
    /// </summary>
    [JsonProperty(TaskFields.UfMailMessage)]
    public string? UfMailMessage {get; set;}
}