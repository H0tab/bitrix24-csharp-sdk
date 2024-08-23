﻿using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.CrmTypes.CrmMultiField;

public class CrmMultiField: IAbstractEntity
{
    /// <summary>
    /// Идентификатор			
    /// Тип: integer	
    /// Только для чтения
    /// </summary>
    [JsonProperty(AbstractEntityFields.Id)]
    public int? Id { get; set; }

    [JsonProperty(CrmMultiFieldFields.ValueType)]
    public string ValueType { get; set; }

    [JsonProperty(CrmMultiFieldFields.Value)]
    public string Value { get; set; }

    [JsonProperty(CrmMultiFieldFields.TypeId)]
    public string TypeId { get; set; }
}