using System.Linq.Expressions;
using Bitrix24RestApiClient.Api.Task.Models;
using Bitrix24RestApiClient.Core;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;

namespace Bitrix24RestApiClient.Api.Task;

/// <summary>
/// Task
/// </summary>
public class Tasks : AbstractEntitiesBase<Models.Task>
{
    private readonly IBitrix24Client client;
    private readonly EntryPointPrefix entityTypePrefix = EntryPointPrefix.Task;
    private readonly int? entityTypeId;

    public Tasks(IBitrix24Client client) : base(client, EntryPointPrefix.Task)
    {
        this.client = client;
    }

    public async Task<ExtFieldsResponse> Fields() =>
        await client.SendPostRequest<object, ExtFieldsResponse>(entityTypePrefix, EntityMethod.Fields, new { });

    public async Task<GetResponseBase<TaskResult>> Get(int id, params Expression<Func<Models.Task, object>>[] fieldsExpr) =>
        await base.Get<TaskResult>(id, fieldsExpr);

    public async Task<AddResponse<TaskResult>> Add(Action<IAddRequestBuilder<Models.Task>> builderFunc) => 
        await base.Add<TaskResult>(builderFunc);

    public async Task<UpdateResponse> Update(int id, Action<IUpdateRequestBuilder<Models.Task, IUpdateArgs>> builderFunc)
    {
        var builder = new UpdateRequestBuilder<Models.Task, TaskUpdateArgs>();
        builder.SetEntityTypeId(entityTypeId);
        builder.SetId(id);
        builderFunc(builder);
        return await client.SendPostRequest<object, UpdateResponse>(entityTypePrefix, EntityMethod.Update, builder.BuildArgs());
    }

    public async Task<ListItemsResponse<TasksResult, TaskItem>> List(Action<IListRequestBuilder<Models.Task>> builderFunc) =>
        await base.List<TasksResult, TaskItem>(builderFunc);

    public async Task<DeleteResponse<TaskDeleteResult>> Delete(int id) =>
        await base.Delete<TaskDeleteResult>(id);
}