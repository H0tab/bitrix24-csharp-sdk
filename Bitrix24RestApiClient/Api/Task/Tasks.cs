using System.Linq.Expressions;
using Bitrix24RestApiClient.Core;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.Response.Common;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Bitrix24RestApiClient.Core.Utilities;
using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Api.Task;

/// <summary>
/// Task
/// </summary>
public class Tasks : AbstractEntities<Models.Task>
{
    private readonly IBitrix24Client client;
    private readonly EntryPointPrefix entityTypePrefix = EntryPointPrefix.Task;
    private readonly int? entityTypeId;

    public Tasks(IBitrix24Client client) : base(client, EntryPointPrefix.Task)
    {
        this.client = client;
    }

    public new async Task<ExtFieldsResponse> Fields() =>
        await client.SendPostRequest<object, ExtFieldsResponse>(entityTypePrefix, EntityMethod.Fields, new { });

    public new async Task<TEntity> Get<TEntity>(int id, params Expression<Func<TEntity, object>>[] fieldsExpr)
        where TEntity : class
    {
        var response = await client.SendPostRequest<CrmEntityGetRequestArgs, ListResponse<TEntity>>(entityTypePrefix,
            EntityMethod.Get, new CrmEntityGetRequestArgs
            {
                Id = id,
                Fields = fieldsExpr.Select(x => x.JsonPropertyName()).ToList()
            });
        return response.Result.FirstOrDefault();
    }

    public new async Task<TaskAddResponse> Add(Action<IAddRequestBuilder<Models.Task>> builderFunc)
    {
        var builder = new AddRequestBuilder<Models.Task>();
        builder.SetEntityTypeId(entityTypeId);
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityAddArgs, TaskAddResponse>(entityTypePrefix, EntityMethod.Add,
            builder.BuildArgs());
    }
}

public class TaskAddResponse
{
    [JsonProperty("result")] public TaskAddResult Result { get; set; }

    [JsonProperty("time")] public Time Time { get; set; }
}

public class TaskAddResult
{
    [JsonProperty("task")] public TaskResult Task { get; set; }
}

public class TaskResult
{
    [JsonProperty("id")] public int Id { get; set; }
}