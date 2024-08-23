using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.CrmTypes.CrmUserFieldList;

public class UserFieldList: IAbstractEntity
{
    /// <summary>
    /// Идентификатор			
    /// Тип: integer	
    /// Только для чтения
    /// </summary>
    [JsonProperty(AbstractEntityFields.Id)]
    public int? Id { get; set; }

    [JsonProperty(UserFieldListFields.Value)]
    public string Value { get; set; }
}