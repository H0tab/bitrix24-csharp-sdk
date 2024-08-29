using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Models;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.Response.BatchResponse;
using Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;
using Bitrix24RestApiClient.Core.Utilities;
using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.BatchStrategies.ListGetStrategy;

public class ListGetStrategyForListItemsResponse
{
    private readonly IBitrix24Client client;
    private readonly EntryPointPrefix entityTypePrefix;
    private readonly int? entityTypeId;

    public ListGetStrategyForListItemsResponse(IBitrix24Client client, EntryPointPrefix entityTypePrefix, int? entityTypeId)
    {
        this.client = client;
        this.entityTypePrefix = entityTypePrefix;
        this.entityTypeId = entityTypeId;
    }

    /// <summary>
    /// Получить список сущностей используя особую стратегию выборки элементов.
    /// Ограничения: стратегия не поддерживает сортировку элементов. Элементы вернутся кучей.
    /// Плюсы: список сущностей будет выбран за минимальное кол-во обращений к API за счет использования batch-запросов и
    /// затратит меньшее кол-во времени, чем если бы элементы выбирались стандартным способом через list-запрос.
    /// </summary>
    /// <param name="builderFunc"></param>
    /// <returns></returns>
    public async IAsyncEnumerable<TCustomEntity> ListAll<TCustomEntity>(Expression<Func<TCustomEntity, object>> idNameExpr, Action<IListAllRequestBuilder<TCustomEntity>> builderFunc)
    {
        var builder = new ListRequestBuilder<TCustomEntity>(); 
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);

        var fetchMinIdBuilder = builder.Copy();
        fetchMinIdBuilder
            .ClearOrderBy()
            .ClearSelect()
            .AddSelect(idNameExpr)
            .AddOrderBy(idNameExpr); 

        var firstListResponse = await client.SendPostRequest<CrmEntityListRequestArgs, ListItemsResponse<ListItems<TCustomEntity>, TCustomEntity>>(entityTypePrefix, EntityMethod.List, fetchMinIdBuilder.BuildArgs());
        if (firstListResponse.Total == 0)
            yield break;

        var nextMinId = firstListResponse.Result.Items.Max(x => (int)ReflectionHelper.GetPropertyValue(idNameExpr, x));
        //Запросы уходят парами. Стартуем запрос на следующую страницу с айдишками и фечим сущности для предыдущей страницы
        var nextListResponseTask = FetchNextList(idNameExpr, fetchMinIdBuilder, nextMinId);

        await foreach (var item in BatchGetItems(idNameExpr, firstListResponse.Result.Items))
            yield return item;

        for (var i = 0; i < firstListResponse.Total; i += 50)
        {
            nextListResponseTask.Wait();
            var listResponse = nextListResponseTask.Result;

            if (listResponse.Result.Items.Count == 0)
                yield break;

            nextMinId = listResponse.Result.Items.Max(x => (int)ReflectionHelper.GetPropertyValue(idNameExpr, x));

            nextListResponseTask = FetchNextList(idNameExpr, fetchMinIdBuilder, nextMinId);

            await foreach (var item in BatchGetItems(idNameExpr, listResponse.Result.Items))
                yield return item;
        }
    }

    private async IAsyncEnumerable<TCustomEntity> BatchGetItems<TCustomEntity>(Expression<Func<TCustomEntity, object>> idNameExpr, List<TCustomEntity> items)
    {
        var getItemsBatch = new CrmBatchRequestArgs
        {
            Halt = 0,
            Commands = items
                .Select(x => new { 
                    Id = ((int)ReflectionHelper.GetPropertyValue(idNameExpr, x)).ToString(), 
                    Cmd = $"{entityTypePrefix.Value}.{EntityMethod.Get.Value}?{ExpressionExtensions.JsonPropertyNameByExpr(idNameExpr)}={(int)ReflectionHelper.GetPropertyValue(idNameExpr, x)}&entityTypeId={entityTypeId}" })
                .ToDictionary(x => x.Id, x => x.Cmd)
        };

        var batchResponse = await client.SendPostRequest<CrmBatchRequestArgs, BatchResponse<GetItemResponse<TCustomEntity>>>(EntryPointPrefix.Batch, EntityMethod.None, getItemsBatch);
        if (batchResponse.Result.Error.Count > 0)
            throw new Exception($"Ошибка при выполнении batch-запроса. Ответ: {JsonConvert.SerializeObject(batchResponse)}");

        foreach (var item in items.Select(x => batchResponse.Result.Result[((int)ReflectionHelper.GetPropertyValue(idNameExpr, x)).ToString()]))
            yield return item.Item;
    }

    private async Task<ListItemsResponse<ListItems<TCustomEntity>, TCustomEntity>> FetchNextList<TCustomEntity>(Expression<Func<TCustomEntity, object>> idNameExpr, ListRequestBuilder<TCustomEntity> fetchMinIdBuilder, int nextMinId)
    {
        var fetchNextBuilder = fetchMinIdBuilder.Copy();
        fetchNextBuilder
            .SetStart(-1)
            .AddFilter(idNameExpr, nextMinId, FilterOperator.GreateThan);

        var listResponse = await client.SendPostRequest<CrmEntityListRequestArgs, ListItemsResponse<ListItems<TCustomEntity>, TCustomEntity>>(entityTypePrefix, EntityMethod.List, fetchNextBuilder.BuildArgs());
        return listResponse;
    }
}