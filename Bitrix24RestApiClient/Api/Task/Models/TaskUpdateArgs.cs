using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;
using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Api.Task.Models;

public class TaskUpdateArgs : IUpdateArgs
{
    [JsonProperty("entityTypeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? EntityTypeId { get; set; }
    
    [JsonProperty("taskId")] 
    public int Id { get; set; }
    
    [JsonProperty("fields")] 
    public Dictionary<string, object> Fields { get; set; }
}