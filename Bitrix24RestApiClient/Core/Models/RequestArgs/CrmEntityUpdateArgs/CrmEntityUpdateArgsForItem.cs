using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;

public class CrmEntityUpdateArgsForItem
{
    [JsonProperty("entityTypeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? EntityTypeId { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("fields")]
    public Dictionary<string, object> Fields { get; set; }
}