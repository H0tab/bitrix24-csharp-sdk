using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.CrmTypes.CrmFile;

public class CrmFile: IAbstractEntity
{
    /// <summary>
    /// Идентификатор			
    /// Тип: integer	
    /// Только для чтения
    /// </summary>
    [JsonProperty(AbstractEntityFields.Id)]
    public int? Id { get; set; }

    [JsonProperty(CrmFileFields.ShowUrl)]
    public string ShowUrl { get; set; }

    [JsonProperty(CrmFileFields.DownloadUrl)]
    public string DownloadUrl { get; set; } 
}