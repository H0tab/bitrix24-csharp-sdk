using Newtonsoft.Json;
using System.Collections.Generic;
using Bitrix24RestApiClient.Core.Models.Response.Common;

namespace Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;

public class FieldsResponse
{
    [JsonProperty("result")]
    public Dictionary<string, FieldInfo> Result { get; set; } = new();

    [JsonProperty("time")]
    public Time Time { get; set; }
}