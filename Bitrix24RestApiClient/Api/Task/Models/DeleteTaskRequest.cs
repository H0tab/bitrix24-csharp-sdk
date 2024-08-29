using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Api.Task.Models;

public class DeleteTaskRequest
{
    [JsonProperty("taskId")]
    public int TaskId { get; set; }
}