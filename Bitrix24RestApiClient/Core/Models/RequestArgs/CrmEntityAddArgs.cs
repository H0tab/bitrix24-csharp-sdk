﻿using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.RequestArgs;

public class CrmEntityAddArgs
{
    [JsonProperty("entityTypeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? EntityTypeId { get; set; }

    [JsonProperty("fields")]
    public Dictionary<string, object> Fields { get; set; }
}