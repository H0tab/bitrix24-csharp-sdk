﻿using Newtonsoft.Json;
using Bitrix24RestApiClient.Core.Models.Response.Common;

namespace Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;

public class ListItemsResponse<TItems, TItemType> where TItems : class, IListItems<TItemType>
{
    [JsonProperty("result")]
    public TItems Result { get; set; }

    [JsonProperty("next")]
    public int? Next { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("time")]
    public Time Time { get; set; }
}