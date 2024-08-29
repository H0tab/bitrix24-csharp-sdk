using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.RequestArgs;

public class TaskGetRequestArgs
{
    // Идентификатор выбираемой сущности
    [JsonProperty("taskId")]
    public int TaskId { get; set; }

    // Список полей, значения которых надо вернуть
    [JsonProperty("select")]
    public List<string> Fields { get; set; } = new();
}