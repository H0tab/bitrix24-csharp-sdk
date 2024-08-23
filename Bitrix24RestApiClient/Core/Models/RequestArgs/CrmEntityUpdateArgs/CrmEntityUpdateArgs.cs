using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;

public class CrmEntityUpdateArgs
{
    [JsonProperty("entityTypeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? EntityTypeId { get; set; }

    [JsonProperty("ID")]
    public int Id { get; set; }

    [JsonProperty("fields")]
    public Dictionary<string, object> Fields { get; set; }
}