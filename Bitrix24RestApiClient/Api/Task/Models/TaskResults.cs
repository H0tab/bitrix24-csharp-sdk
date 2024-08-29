using Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;
using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Api.Task.Models;

public class TasksResult : IListItems<TaskItem>
{
    [JsonProperty("tasks")] 
    public List<TaskItem> Items { get; set; }
}

public class TaskResult
{
    [JsonProperty("task")] 
    public TaskItem Task { get; set; }
}

public class TaskDeleteResult
{
    [JsonProperty("task")] 
    public bool Deleted { get; set; }
}

public class TaskItem
{
    [JsonProperty("id")] 
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("description")]
    public string Descritption { get; set; }
}