namespace Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;

public interface IUpdateArgs
{
    int? EntityTypeId { get; set; }
    int Id { get; set; }
    Dictionary<string, object> Fields { get; set; }
}