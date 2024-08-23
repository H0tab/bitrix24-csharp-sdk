using Bitrix24RestApiClient.Core.Models.CrmTypes;
using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Api.Crm.CrmActivity.Models;

/// <summary>
/// Activity
/// </summary>
public class Activity : IAbstractEntity
{
	/// <summary>
	/// ID
	/// Тип: integer
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.Id)]
	public int? Id { get; set; }

	/// <summary>
	/// Название
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.Title)]
	public string? Title { get; set; }

	/// <summary>
	/// Тип
	/// Тип: crm_status
	/// </summary>
	[JsonProperty(ActivityFields.TypeId)]
	public string? TypeId { get; set; }

	/// <summary>
	/// Воронка
	/// Тип: crm_category
	/// </summary>
	[JsonProperty(ActivityFields.CategoryId)]
	public string? CategoryId { get; set; }

	/// <summary>
	/// Стадия сделки
	/// Тип: crm_status
	/// </summary>
	[JsonProperty(ActivityFields.StageId)]
	public string? StageId { get; set; }

	/// <summary>
	/// Группа стадии
	/// Тип: string
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.StageSemanticId)]
	public string? StageSemanticId { get; set; }

	/// <summary>
	/// Новая сделка
	/// Тип: char
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.IsNew)]
	public string? IsNew { get; set; }

	/// <summary>
	/// Регулярная сделка
	/// Тип: char
	/// </summary>
	[JsonProperty(ActivityFields.IsRecurring)]
	public string? IsRecurring { get; set; }

	/// <summary>
	/// Повторная сделка
	/// Тип: char
	/// </summary>
	[JsonProperty(ActivityFields.IsReturnCustomer)]
	public string? IsReturnCustomer { get; set; }

	/// <summary>
	/// Повторное обращение
	/// Тип: char
	/// </summary>
	[JsonProperty(ActivityFields.IsRepeatedApproach)]
	public string? IsRepeatedApproach { get; set; }

	/// <summary>
	/// Вероятность
	/// Тип: integer
	/// </summary>
	[JsonProperty(ActivityFields.Probability)]
	public int? Probability { get; set; }

	/// <summary>
	/// Валюта
	/// Тип: crm_currency
	/// </summary>
	[JsonProperty(ActivityFields.CurrencyId)]
	public string? CurrencyId { get; set; }

	/// <summary>
	/// Сумма
	/// Тип: double
	/// </summary>
	[JsonProperty(ActivityFields.Opportunity)]
	public decimal? Opportunity { get; set; }

	/// <summary>
	/// IS_MANUAL_OPPORTUNITY
	/// Тип: char
	/// </summary>
	[JsonProperty(ActivityFields.IsManualOpportunity)]
	public string? IsManualOpportunity { get; set; }

	/// <summary>
	/// Ставка налога
	/// Тип: double
	/// </summary>
	[JsonProperty(ActivityFields.TaxValue)]
	public decimal? TaxValue { get; set; }

	/// <summary>
	/// Компания
	/// Тип: crm_company
	/// </summary>
	[JsonProperty(ActivityFields.CompanyId)]
	public int? CompanyId { get; set; }

	/// <summary>
	/// Контакт
	/// Тип: crm_contact
	/// </summary>
	[JsonProperty(ActivityFields.ContactId)]
	public int? ContactId { get; set; }

	/// <summary>
	/// Контакты
	/// Тип: crm_contact
	/// </summary>
	[JsonProperty(ActivityFields.ContactIds)]
	public int? ContactIds { get; set; }

	/// <summary>
	/// Предложение
	/// Тип: crm_quote
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.QuoteId)]
	public string? QuoteId { get; set; }

	/// <summary>
	/// Дата начала
	/// Тип: date
	/// </summary>
	[JsonProperty(ActivityFields.Begindate)]
	public DateTimeOffset? Begindate { get; set; }

	/// <summary>
	/// Дата завершения
	/// Тип: date
	/// </summary>
	[JsonProperty(ActivityFields.Closedate)]
	public DateTimeOffset? Closedate { get; set; }

	/// <summary>
	/// Доступна для всех
	/// Тип: char
	/// </summary>
	[JsonProperty(ActivityFields.Opened)]
	public string? Opened { get; set; }

	/// <summary>
	/// Закрыта
	/// Тип: char
	/// </summary>
	[JsonProperty(ActivityFields.Closed)]
	public string? Closed { get; set; }

	/// <summary>
	/// Комментарий
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.Comments)]
	public string? Comments { get; set; }

	/// <summary>
	/// Ответственный
	/// Тип: user
	/// </summary>
	[JsonProperty(ActivityFields.AssignedById)]
	public int? AssignedById { get; set; }

	/// <summary>
	/// Кем создана
	/// Тип: user
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.CreatedById)]
	public int? CreatedById { get; set; }

	/// <summary>
	/// Кем изменена
	/// Тип: user
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.ModifyById)]
	public int? ModifyById { get; set; }

	/// <summary>
	/// MOVED_BY_ID
	/// Тип: user
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.MovedById)]
	public int? MovedById { get; set; }

	/// <summary>
	/// Дата создания
	/// Тип: datetime
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.DateCreate)]
	public DateTimeOffset? DateCreate { get; set; }

	/// <summary>
	/// Дата изменения
	/// Тип: datetime
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.DateModify)]
	public DateTimeOffset? DateModify { get; set; }

	/// <summary>
	/// MOVED_TIME
	/// Тип: datetime
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.MovedTime)]
	public DateTimeOffset? MovedTime { get; set; }

	/// <summary>
	/// Источник
	/// Тип: crm_status
	/// </summary>
	[JsonProperty(ActivityFields.SourceId)]
	public string? SourceId { get; set; }

	/// <summary>
	/// Дополнительно об источнике
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.SourceDescription)]
	public string? SourceDescription { get; set; }

	/// <summary>
	/// Лид
	/// Тип: crm_lead
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.LeadId)]
	public int? LeadId { get; set; }

	/// <summary>
	/// Дополнительная информация
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.AdditionalInfo)]
	public string? AdditionalInfo { get; set; }

	/// <summary>
	/// Местоположение
	/// Тип: location
	/// </summary>
	[JsonProperty(ActivityFields.LocationId)]
	public string? LocationId { get; set; }

	/// <summary>
	/// Внешний источник
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.OriginatorId)]
	public string? OriginatorId { get; set; }

	/// <summary>
	/// Идентификатор элемента во внешнем источнике
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.OriginId)]
	public string? OriginId { get; set; }

	/// <summary>
	/// Рекламная система
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.UtmSource)]
	public string? UtmSource { get; set; }

	/// <summary>
	/// Тип трафика
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.UtmMedium)]
	public string? UtmMedium { get; set; }

	/// <summary>
	/// Обозначение рекламной кампании
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.UtmCampaign)]
	public string? UtmCampaign { get; set; }

	/// <summary>
	/// Содержание кампании
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.UtmContent)]
	public string? UtmContent { get; set; }

	/// <summary>
	/// Условие поиска кампании
	/// Тип: string
	/// </summary>
	[JsonProperty(ActivityFields.UtmTerm)]
	public string? UtmTerm { get; set; }

	/// <summary>
	/// LAST_ACTIVITY_TIME
	/// Тип: datetime
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.LastActivityTime)]
	public DateTimeOffset? LastActivityTime { get; set; }

	/// <summary>
	/// LAST_ACTIVITY_BY
	/// Тип: user
	/// Только для чтения
	/// </summary>
	[JsonProperty(ActivityFields.LastActivityBy)]
	public int? LastActivityBy { get; set; }

}