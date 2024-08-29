using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.CrmTypes;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;

namespace Bitrix24RestApiClient.Core;

public class AbstractEntities<TEntity> : AbstractEntitiesBase<TEntity> where TEntity : class, IAbstractEntity
{
    protected AbstractEntities(IBitrix24Client client, EntryPointPrefix entityTypePrefix, int? entityTypeId = null) : base(client, entityTypePrefix, entityTypeId)
    { }

    public async Task<FieldsResponse> Fields() =>
        await client.SendPostRequest<object, FieldsResponse>(entityTypePrefix, EntityMethod.Fields, new { });

    public async Task<ListResponse<TEntity>> List() =>
        await List<TEntity>();

    public async Task<ListResponse<TEntity>> List(Action<IListRequestBuilder<TEntity>> builderFunc) =>
        await List<TEntity>(builderFunc);

    public async Task<TEntity> First(Action<IListRequestBuilder<TEntity>> builderFunc) =>
        await First<TEntity>(builderFunc);

    public async Task<GetResponseBase<TEntity>> Get(int id, params Expression<Func<TEntity, object>>[] fieldsExpr) =>
        await Get<TEntity>(id, fieldsExpr);

    public async Task<DeleteResponse> Delete(int id)
    {
        var response = await Delete<bool?>(id);
        return new DeleteResponse
        {
            Result = response.Result,
            Time = response.Time
        };
    }

    public async Task<UpdateResponse> Update(int id, Action<IUpdateRequestBuilder<TEntity, IUpdateArgs>> builderFunc) =>
        await Update<TEntity>(id, builderFunc);

    public async Task<AddResponse> Add()
    {
        var builder = new AddRequestBuilder<TEntity>();
        builder.SetEntityTypeId(entityTypeId);
        return await client.SendPostRequest<CrmEntityAddArgs, AddResponse>(entityTypePrefix, EntityMethod.Add,
            builder.BuildArgs());
    }

    public async Task<AddResponse> Add(Action<IAddRequestBuilder<TEntity>> builderFunc)
    {
        var response = await Add<TEntity, int>(builderFunc);
        return new AddResponse
        {
            Result = response.Result,
            Time = response.Time
        };
    }
}