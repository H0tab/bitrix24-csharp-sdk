using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;

public class ListItems<TEntity> : IListItems<TEntity>
{
    [JsonProperty("items")]
    public List<TEntity> Items { get; set; }
}