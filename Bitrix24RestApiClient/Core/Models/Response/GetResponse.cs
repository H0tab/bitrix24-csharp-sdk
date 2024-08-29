using Newtonsoft.Json;
using Bitrix24RestApiClient.Core.Models.Response.Common;

namespace Bitrix24RestApiClient.Core.Models.Response;

public class GetResponseBase<TEntity>
{
    [JsonProperty("result")] 
    public TEntity Result { get; set; }

    [JsonProperty("time")] 
    public Time Time { get; set; }
}

public class GetResponse<TEntity> : GetResponseBase<TEntity>
{
    [JsonProperty("next")]
    public int? Next { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }
}