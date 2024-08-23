using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.RequestArgs;

public class CrmBatchRequestArgs
{
    [JsonProperty("halt")]
    public int Halt { get; set; }

    [JsonProperty("cmd")]
    public Dictionary<string, string> Commands { get; set; } = new();
}