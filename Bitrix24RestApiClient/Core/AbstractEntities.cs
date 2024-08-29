using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.BatchStrategies.BatchOperations;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.CrmTypes;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;

namespace Bitrix24RestApiClient.Core;

public abstract class AbstractEntities<TEntity> where TEntity : class, IAbstractEntity
{
    private readonly IBitrix24Client client;
    private readonly EntryPointPrefix entityTypePrefix;
    private readonly int? entityTypeId;

    protected AbstractEntities(IBitrix24Client client, EntryPointPrefix entityTypePrefix, int? entityTypeId = null)
    {
        this.client = client;
        this.entityTypePrefix = entityTypePrefix;
        this.entityTypeId = entityTypeId;
        BatchOperations = new BatchOperationsForListResponse(client, entityTypePrefix);
    }

    public BatchOperationsForListResponse BatchOperations { get; private set; }

    public virtual async Task<FieldsResponse> Fields() =>
        await client.SendPostRequest<object, FieldsResponse>(entityTypePrefix, EntityMethod.Fields, new { });

    public virtual async Task<ListResponse<TEntity>> List() =>
        await List<TEntity>();

    protected virtual async Task<ListResponse<TCustomEntity>> List<TCustomEntity>()
        where TCustomEntity : IAbstractEntity
    {
        var builder = new ListRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        return await client.SendPostRequest<CrmEntityListRequestArgs, ListResponse<TCustomEntity>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }

    public virtual async Task<ListResponse<TEntity>> List(Action<IListRequestBuilder<TEntity>> builderFunc) =>
        await List<TEntity>(builderFunc);

    protected virtual async Task<ListResponse<TCustomEntity>> List<TCustomEntity>(
        Action<IListRequestBuilder<TCustomEntity>> builderFunc) where TCustomEntity : IAbstractEntity
    {
        var builder = new ListRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityListRequestArgs, ListResponse<TCustomEntity>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }

    protected virtual async Task<ListItemsResponse<TListItems, TItemsType>> List<TListItems, TItemsType>(Action<IListRequestBuilder<TEntity>> builderFunc)
        where TListItems : class, IListItems<TItemsType> =>
        await List<TEntity, TListItems, TItemsType>(builderFunc);

    protected virtual async Task<ListItemsResponse<TListItems, TItemsType>> List<TCustomEntity, TListItems, TItemsType>(Action<IListRequestBuilder<TCustomEntity>> builderFunc)
        where TListItems : class, IListItems<TItemsType>
    {
        var builder = new ListRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityListRequestArgs, ListItemsResponse<TListItems, TItemsType>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }

    public virtual async Task<TEntity> First(Action<IListRequestBuilder<TEntity>> builderFunc) =>
        await First<TEntity>(builderFunc);

    protected virtual async Task<TCustomEntity> First<TCustomEntity>(Action<IListRequestBuilder<TCustomEntity>> builderFunc) where TCustomEntity : IAbstractEntity =>
        (await List(builderFunc)).Result.First();

    public virtual async Task<GetResponseBase<TEntity>> Get(int id, params Expression<Func<TEntity, object>>[] fieldsExpr) =>
        await Get<TEntity>(id, fieldsExpr);

    protected virtual async Task<GetResponseBase<TGetResult>> Get<TGetResult>(int id, params Expression<Func<TEntity, object>>[] fieldsExpr) =>
        await Get<TGetResult, TEntity>(id, fieldsExpr);

    protected virtual async Task<GetResponseBase<TGetResult>> Get<TGetResult, TCustomEntity>(int id, params Expression<Func<TCustomEntity, object>>[] fieldsExpr) where TCustomEntity : class =>
        await client.SendPostRequest<TaskGetRequestArgs, GetResponseBase<TGetResult>>(entityTypePrefix,
            EntityMethod.Get, new()
            {
                TaskId = id,
                Fields = GetFields(fieldsExpr)
            });

    private static List<string> GetFields<T>(params Expression<Func<T, object>>[] fieldsExpr) =>
        fieldsExpr.Length != 0
            ? fieldsExpr.Select(x => ((MemberExpression)x.Body).Member.Name).ToList()
            : ["*"];

    public virtual async Task<DeleteResponse> Delete(int id)
    {
        var response = await Delete<bool?>(id);
        return new DeleteResponse
        {
            Result = response.Result,
            Time = response.Time
        };
    }

    protected virtual async Task<DeleteResponse<TDeleteResult>> Delete<TDeleteResult>(int id) =>
        await client.SendPostRequest<CrmEntityDeleteRequestArgs, DeleteResponse<TDeleteResult>>(entityTypePrefix,
            EntityMethod.Delete, new CrmEntityDeleteRequestArgs
            {
                EntityTypeId = entityTypeId,
                Id = id
            });

    public virtual async Task<UpdateResponse> Update(int id, Action<IUpdateRequestBuilder<TEntity, IUpdateArgs>> builderFunc) =>
        await Update<TEntity>(id, builderFunc);


    protected virtual async Task<UpdateResponse> Update<TCustomEntity>(int id, Action<IUpdateRequestBuilder<TCustomEntity, IUpdateArgs>> builderFunc)
    {
        var builder = new UpdateRequestBuilder<TCustomEntity, CrmEntityUpdateArgs>();
        builder.SetEntityTypeId(entityTypeId);
        builder.SetId(id);
        builderFunc(builder);
        return await client.SendPostRequest<object, UpdateResponse>(entityTypePrefix, EntityMethod.Update, builder.BuildArgs());
    }

    public virtual async Task<AddResponse> Add()
    {
        var builder = new AddRequestBuilder<TEntity>();
        builder.SetEntityTypeId(entityTypeId);
        return await client.SendPostRequest<CrmEntityAddArgs, AddResponse>(entityTypePrefix, EntityMethod.Add,
            builder.BuildArgs());
    }

    public virtual async Task<AddResponse> Add(Action<IAddRequestBuilder<TEntity>> builderFunc)
    {
        var response = await Add<TEntity, int>(builderFunc);
        return new AddResponse
        {
            Result = response.Result,
            Time = response.Time
        };
    }

    protected virtual async Task<AddResponse<TAddResult>> Add<TAddResult>(Action<IAddRequestBuilder<TEntity>> builderFunc)
        where TAddResult : class =>
        await Add<TEntity, TAddResult>(builderFunc);

    protected virtual async Task<AddResponse<TAddResult>> Add<TCustomEntity, TAddResult>(
        Action<IAddRequestBuilder<TCustomEntity>> builderFunc)
    {
        var builder = new AddRequestBuilder<TCustomEntity>();
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityAddArgs, AddResponse<TAddResult>>(entityTypePrefix, EntityMethod.Add,
            builder.BuildArgs());
    }
}