﻿using Newtonsoft.Json;
using Bitrix24RestApiClient.Core.Attributes;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.CrmTypes;

namespace Bitrix24RestApiClient.Api.Crm.CrmDeal.Models;

public class Deal: IAbstractEntity
{
    /// <summary>
    /// Идентификатор			
    /// Тип: integer	
    /// Только для чтения
    /// </summary>
    [JsonProperty(AbstractEntityFields.Id)]
    public int? Id { get; set; }

    /// <summary>
    /// Дополнительная информация	
    /// Тип: string	
    /// </summary>
    [JsonProperty(DealFields.AdditinalInfo)]
    public string AdditinalInfo { get; set; }

    /// <summary>
    /// Связано с пользователем по ID (родной тип user)	
    /// Тип: user	
    /// </summary>
    [JsonProperty(DealFields.AssignedById)]
    public int? AssignedById { get; set; }

    /// <summary>
    /// ID банковского реквизита			
    /// Тип: integer	
    /// Принимается, но не возвращается. 
    /// Параметр подаётся на функцию crm.requisite.link.register автоматически при успешном добавлении/обновлении сделки с идентификатором этой сделки
    /// </summary>
    [JsonProperty(DealFields.BankDetailId)]
    public int? BankDetailId { get; set; }

    /// <summary>
    /// Дата начала			
    /// Тип: date	
    /// </summary>
    [JsonProperty(DealFields.BeginDate)]
    public DateTimeOffset? BeginDate { get; set; }

    /// <summary>
    /// Идентификатор направления			
    /// Тип: crm_category	
    /// Неизменяемое. Если не передавать это поле при создании сделки, то сделка создастся в общем направлении.
    /// </summary>
    [JsonProperty(DealFields.CategoryId)]
    public int? CategoryId { get; set; }

    /// <summary>
    /// Завершена	
    /// Тип: char	
    /// </summary>
    [JsonIgnore]
    [CrmField(DealFields.Closed, CrmFieldSubTypeEnum.Char_YesNo)]
    public bool Closed
    {
        get
        {
            return ClosedExt == YesNoEnum.Y.ToString("F");
        }
        set
        {
            ClosedExt = value
                ? YesNoEnum.Y.ToString("F")
                : YesNoEnum.N.ToString("F");
        }
    }

    /// <summary>
    /// Завершена.	
    /// Тип: char
    /// Скрытое свойство для передачи в битрикс
    /// </summary>
    [JsonProperty(DealFields.Closed)]
    public string ClosedExt { get; set; }

    /// <summary>
    /// Дата завершения			
    /// Тип: date	
    /// </summary>
    [JsonProperty(DealFields.CloseDate)]
    public DateTimeOffset? CloseDate { get; set; }

    /// <summary>
    /// Коментарии	
    /// Тип: string	
    /// </summary>
    [JsonProperty(DealFields.Comments)]
    public string Comments { get; set; }

    /// <summary>
    /// Идентификатор привязанной компании			
    /// Тип: crm_company	
    /// </summary>
    [JsonProperty(DealFields.CompanyId)]
    public int? CompanyId { get; set; }

    /// <summary>
    /// Идентификатор привязанного контакта			
    /// Тип: crm_contact	
    /// Устаревший. Сохраняется для совместимости
    /// </summary>
    [JsonProperty(DealFields.ContactId)]
    public int? ContactId { get; set; }

    /// <summary>
    /// Создано пользователем			
    /// Тип: user	
    /// Только для чтения
    /// </summary>
    [JsonProperty(DealFields.CreatedById)]
    public int? CreatedById { get; set; }

    /// <summary>
    /// Идентификатор валюты сделки			
    /// Тип: crm_currency	
    /// </summary>
    [JsonProperty(DealFields.CurrencyId)]
    public string CurrencyId { get; set; }

    /// <summary>
    /// Дата создания			
    /// Тип: datetime	
    /// Только для чтения
    /// </summary>
    [JsonProperty(DealFields.DateCreate)]
    public DateTimeOffset? DateCreate { get; set; }

    /// <summary>
    /// Дата изменения			
    /// Тип: datetime	
    /// Только для чтения
    /// </summary>
    [JsonProperty(DealFields.DateModify)]
    public DateTimeOffset? DateModify { get; set; }

    /// <summary>
    /// Флаг новой сделки (т. е. сделка в первой стадии)				
    /// Тип: char	
    /// </summary>
    [JsonIgnore]
    [CrmField(DealFields.IsNew, CrmFieldSubTypeEnum.Char_YesNo)]
    public bool IsNew
    {
        get
        {
            return IsNewExt == YesNoEnum.Y.ToString("F");
        }
        set
        {
            IsNewExt = value
                ? YesNoEnum.Y.ToString("F")
                : YesNoEnum.N.ToString("F");
        }
    }

    /// <summary>
    /// Флаг новой сделки (т. е. сделка в первой стадии)			
    /// Тип: char	
    /// </summary>
    [JsonProperty(DealFields.IsNew)]
    public string IsNewExt { get; set; }

    /// <summary>
    /// Флаг шаблона регулярной сделки (если стоит Y, то это не сделка, а шаблон)		
    /// Тип: char	
    /// </summary>
    [JsonIgnore]
    [CrmField(DealFields.IsRecurring, CrmFieldSubTypeEnum.Char_YesNo)]
    public bool IsRecurring
    {
        get
        {
            return IsRecurringExt == YesNoEnum.Y.ToString("F");
        }
        set
        {
            IsRecurringExt = value
                ? YesNoEnum.Y.ToString("F")
                : YesNoEnum.N.ToString("F");
        }
    }

    /// <summary>
    /// Флаг шаблона регулярной сделки (если стоит Y, то это не сделка, а шаблон)			
    /// Тип: char	
    /// </summary>
    [JsonProperty(DealFields.IsRecurring)]
    public string IsRecurringExt { get; set; }

    /// <summary>
    /// Признак повторного лида			
    /// Тип: char	
    /// </summary>
    [JsonIgnore]
    [CrmField(DealFields.IsReturnCustomer, CrmFieldSubTypeEnum.Char_YesNo)]
    public bool IsReturnCustomer
    {
        get
        {
            return IsReturnCustomerExt == YesNoEnum.Y.ToString("F");
        }
        set
        {
            IsReturnCustomerExt = value
                ? YesNoEnum.Y.ToString("F")
                : YesNoEnum.N.ToString("F");
        }
    }

    /// <summary>
    /// Признак повторного лида			
    /// Тип: char	
    /// </summary>
    [JsonProperty(DealFields.IsReturnCustomer)]
    public string IsReturnCustomerExt { get; set; }

    /// <summary>
    /// Идентификатор привязанного лида			
    /// Тип: crm_lead	
    /// Только для чтения
    /// </summary>
    [JsonProperty(DealFields.LeadId)]
    public int? LeadId { get; set; }

    /// <summary>
    /// Местоположение клиента			
    /// Тип: location	
    /// Служебный, не рекомендуется к использованию.
    /// </summary>
    [JsonProperty(DealFields.LocationId)]
    public string LocationId { get; set; }

    /// <summary>
    /// Идентификатор автора последнего изменения			
    /// Тип: user	
    /// Только для чтения
    /// </summary>
    [JsonProperty(DealFields.ModifyById)]
    public int? ModifyById { get; set; }

    /// <summary>
    /// Доступен для всех		
    /// Тип: char	
    /// </summary>
    [JsonIgnore]
    [CrmField(DealFields.Opened, CrmFieldSubTypeEnum.Char_YesNo)]
    public bool Opened
    {
        get
        {
            return OpenedExt == YesNoEnum.Y.ToString("F");
        }
        set
        {
            OpenedExt = value
                ? YesNoEnum.Y.ToString("F")
                : YesNoEnum.N.ToString("F");
        }
    }

    /// <summary>
    /// Доступен для всех			
    /// Тип: char	
    /// </summary>
    [JsonProperty(DealFields.Opened)]
    public string OpenedExt { get; set; }

    /// <summary>
    /// Сумма			
    /// Тип: double	
    /// </summary>
    [JsonProperty(DealFields.Opportunity)]
    public decimal? Opportunity { get; set; }

    /// <summary>
    /// Идентификатор источника данных			
    /// Тип: string	
    /// Используется только для привязки к внешнему источнику.
    /// </summary>
    [JsonProperty(DealFields.OriginatorId)]
    public string OriginatorId { get; set; }

    /// <summary>
    /// Идентификатор элемента в источнике данных		
    /// Тип: string	
    /// Используется только для привязки к внешнему источнику.
    /// </summary>
    [JsonProperty(DealFields.OriginId)]
    public string OriginId { get; set; }

    /// <summary>
    /// Вероятность	
    /// Тип: integer	
    /// </summary>
    [JsonProperty(DealFields.Probability)]
    public int? Probability { get; set; }

    /// <summary>
    /// Идентификатор квоты		
    /// Тип: crm_quote	
    /// Только для чтения. 
    /// Устаревший, использовать метод crm.quote.list с фильром по сделке
    /// </summary>
    [JsonProperty(DealFields.QuoteId)]
    public int? QuoteId { get; set; }

    /// <summary>
    /// Идентификатор реквизита		Принимается, но не возвращается.
    /// Параметр подаётся на функцию crm.requisite.link.register автоматически при успешном добавлении/обновлении сделки с идентификатором этой сделки.
    /// </summary>
    [JsonProperty(DealFields.RequisiteId)]
    public int? RequisiteId { get; set; }

    /// <summary>
    /// Идентификатор стадии		
    /// Тип: crm_status	
    /// </summary>
    [JsonProperty(DealFields.StageId)]
    public string StageId { get; set; }

    /// <summary>
    /// Имя	
    /// Тип: string	
    /// </summary>
    [JsonProperty(DealFields.StageSemanticId)]
    public string StageSemanticId { get; set; }

    /// <summary>
    /// Идентификатор источника. Определяет источник сделки (обратный звонок, реклама, электронная почта итд). 	
    /// Тип: string	
    /// Список возможных идентификаторов можно вытащить рест методом crm.status.list с фильтром filter[ENTITY_ID]=SOURCE
    /// </summary>
    [JsonProperty(DealFields.SourceId)]
    public string SourceId { get; set; }

    /// <summary>
    /// Дополнительно об источнике.	
    /// Тип: string	
    /// Текстовое поле
    /// </summary>
    [JsonProperty(DealFields.SourceDescription)]
    public string SourceDescription { get; set; }

    /// <summary>
    /// Ставка налога
    /// Тип: double	
    /// </summary>
    [JsonProperty(DealFields.TaxValue)]
    public decimal? TaxValue { get; set; }

    /// <summary>
    /// Название	
    /// Тип: string	
    /// Обязательное.
    /// </summary>
    [JsonProperty(DealFields.Title)]
    public string Title { get; set; }

    /// <summary>
    /// Тип сделки		
    /// Тип: crm_status	
    /// Используется только для привязки к внешнему источнику.
    /// </summary>
    [JsonProperty(DealFields.TypeId)]
    public string TypeId { get; set; }

    /// <summary>
    /// Обозначение рекламной кампании		
    /// Тип: string	
    /// </summary>
    [JsonProperty(DealFields.UtmCampaign)]
    public string UtmCampaign { get; set; }

    /// <summary>
    /// Содержание кампании		
    /// Тип: string	
    /// Например, для контекстных объявлений.
    /// </summary>
    [JsonProperty(DealFields.UtmContent)]
    public string UtmContent { get; set; }

    /// <summary>
    /// Тип трафика		
    /// Тип: string	
    /// CPC (объявления), CPM (баннеры)
    /// </summary>
    [JsonProperty(DealFields.UtmMedium)]
    public string UtmMedium { get; set; }

    /// <summary>
    /// Рекламная система	
    /// Тип: string	
    /// Yandex-Direct, Google-Adwords и другие.
    /// </summary>
    [JsonProperty(DealFields.UtmSource)]
    public string UtmSource { get; set; }

    /// <summary>
    /// Условие поиска кампании	
    /// Тип: string	
    /// Например, ключевые слова контекстной рекламы.
    /// </summary>
    [JsonProperty(DealFields.UtmTerm)]
    public string UtmTerm { get; set; }


}