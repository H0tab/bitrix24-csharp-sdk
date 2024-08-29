namespace Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;

public interface IListItems<TItem>
{
    public List<TItem> Items { get; set; }
}