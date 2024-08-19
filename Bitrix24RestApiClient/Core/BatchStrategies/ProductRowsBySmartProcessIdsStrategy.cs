using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Models.Response.BatchResponse;
using Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;

namespace Bitrix24RestApiClient.Core.BatchStrategies;

public class ProductRowsBySmartProcessIdsStrategy
{
    private readonly IBitrix24Client client;

    public ProductRowsBySmartProcessIdsStrategy(IBitrix24Client client)
    {
        this.client = client;
    }

    public async IAsyncEnumerable<ByIdBatchResponseItem<ListProductRowsResponseResult>> Get(string smartProcessType, List<int> smartProcessIds)
    {
        const int batchSize = 50;

        for (var i = 0; i < smartProcessIds.Count; i += batchSize)
        {
            var partIds = smartProcessIds.GetRange(i, Math.Min(batchSize, smartProcessIds.Count - i));

            await foreach (var item in BatchGetItems(smartProcessType, partIds))
                yield return item;
        }
    } 

    private async IAsyncEnumerable<ByIdBatchResponseItem<ListProductRowsResponseResult>> BatchGetItems(string smartProcessType, List<int> ids)
    {
        var getItemsBatch = new CrmBatchRequestArgs() 
        {
            Halt = 0,
            Commands = ids
                .Select(id => new { Id = id, Cmd = $"{EntryPointPrefix.CrmProductRow.Value}.{EntityMethod.List.Value}?filter[%3DownerId]={id}&filter[%3DownerType]={smartProcessType}" })
                .ToDictionary(x => x.Id.ToString(), x => x.Cmd)
        }; 

        var batchResponse = await client.SendPostRequest<CrmBatchRequestArgs, BatchResponse<ListProductRowsResponseResult>>(EntryPointPrefix.Batch, EntityMethod.None, getItemsBatch);
        if (batchResponse.Result.Error.Count > 0)
            throw new Exception($"Ошибка при выполнении batch-запроса. Ответ: {JsonConvert.SerializeObject(batchResponse)}");

        foreach (var item in ids.Select(x => new ByIdBatchResponseItem<ListProductRowsResponseResult>
                 {
                     Id = x,
                     Result  = batchResponse.Result.Result[x.ToString()]
                 }))
            yield return item;
    }
}