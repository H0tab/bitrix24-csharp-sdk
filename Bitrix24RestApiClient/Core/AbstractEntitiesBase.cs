using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.BatchStrategies.BatchOperations;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Models.CrmTypes;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;

namespace Bitrix24RestApiClient.Core;

public class AbstractEntitiesBase<TEntity> where TEntity : class, IAbstractEntity
{
    protected readonly IBitrix24Client client;
    protected readonly EntryPointPrefix entityTypePrefix;
    protected readonly int? entityTypeId;

    protected AbstractEntitiesBase(IBitrix24Client client, EntryPointPrefix entityTypePrefix, int? entityTypeId = null)
    {
        this.client = client;
        this.entityTypePrefix = entityTypePrefix;
        this.entityTypeId = entityTypeId;
        BatchOperations = new BatchOperationsForListResponse(client, entityTypePrefix);
    }
    
    public BatchOperationsForListResponse BatchOperations { get; private set; }
    
    protected async Task<ListResponse<TCustomEntity>> List<TCustomEntity>()
        where TCustomEntity : IAbstractEntity
    {
        var builder = new ListRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        return await client.SendPostRequest<CrmEntityListRequestArgs, ListResponse<TCustomEntity>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }
    
    protected async Task<ListResponse<TCustomEntity>> List<TCustomEntity>(
        Action<IListRequestBuilder<TCustomEntity>> builderFunc) where TCustomEntity : IAbstractEntity
    {
        var builder = new ListRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityListRequestArgs, ListResponse<TCustomEntity>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }

    protected async Task<ListItemsResponse<TListItems, TItemsType>> List<TListItems, TItemsType>(Action<IListRequestBuilder<TEntity>> builderFunc)
        where TListItems : class, IListItems<TItemsType> =>
        await List<TEntity, TListItems, TItemsType>(builderFunc);

    protected async Task<ListItemsResponse<TListItems, TItemsType>> List<TCustomEntity, TListItems, TItemsType>(Action<IListRequestBuilder<TCustomEntity>> builderFunc)
        where TListItems : class, IListItems<TItemsType>
    {
        var builder = new ListRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityListRequestArgs, ListItemsResponse<TListItems, TItemsType>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }
    
    protected async Task<TCustomEntity> First<TCustomEntity>(Action<IListRequestBuilder<TCustomEntity>> builderFunc) where TCustomEntity : IAbstractEntity =>
        (await List(builderFunc)).Result.First();
    
    protected async Task<GetResponseBase<TGetResult>> Get<TGetResult>(int id, params Expression<Func<TEntity, object>>[] fieldsExpr) =>
        await Get<TGetResult, TEntity>(id, fieldsExpr);

    protected async Task<GetResponseBase<TGetResult>> Get<TGetResult, TCustomEntity>(int id, params Expression<Func<TCustomEntity, object>>[] fieldsExpr) where TCustomEntity : class =>
        await client.SendPostRequest<CrmEntityGetRequestArgs, GetResponseBase<TGetResult>>(entityTypePrefix,
            EntityMethod.Get, new()
            {
                Id = id,
                Fields = GetFields(fieldsExpr)
            });

    private static List<string> GetFields<T>(params Expression<Func<T, object>>[] fieldsExpr) =>
        fieldsExpr.Length != 0
            ? fieldsExpr.Select(x => ((MemberExpression)x.Body).Member.Name).ToList()
            : ["*"];
    
    protected async Task<UpdateResponse> Update<TCustomEntity>(int id, Action<IUpdateRequestBuilder<TCustomEntity, IUpdateArgs>> builderFunc)
    {
        var builder = new UpdateRequestBuilder<TCustomEntity, CrmEntityUpdateArgs>();
        builder.SetEntityTypeId(entityTypeId);
        builder.SetId(id);
        builderFunc(builder);
        return await client.SendPostRequest<object, UpdateResponse>(entityTypePrefix, EntityMethod.Update, builder.BuildArgs());
    }
    
    protected async Task<DeleteResponse<TDeleteResult>> Delete<TDeleteResult>(int id) =>
        await client.SendPostRequest<CrmEntityDeleteRequestArgs, DeleteResponse<TDeleteResult>>(entityTypePrefix,
            EntityMethod.Delete, new CrmEntityDeleteRequestArgs
            {
                EntityTypeId = entityTypeId,
                Id = id
            });
    
    protected async Task<AddResponse<TAddResult>> Add<TAddResult>(Action<IAddRequestBuilder<TEntity>> builderFunc)
        where TAddResult : class =>
        await Add<TEntity, TAddResult>(builderFunc);

    protected async Task<AddResponse<TAddResult>> Add<TCustomEntity, TAddResult>(
        Action<IAddRequestBuilder<TCustomEntity>> builderFunc)
    {
        var builder = new AddRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityAddArgs, AddResponse<TAddResult>>(entityTypePrefix, EntityMethod.Add,
            builder.BuildArgs());
    }
}