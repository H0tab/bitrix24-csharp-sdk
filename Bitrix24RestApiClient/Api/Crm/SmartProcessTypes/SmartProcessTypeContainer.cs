﻿using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Utilities;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Api.Crm.SmartProcessTypes.Models;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Bitrix24RestApiClient.Core.Models.Response.SmartProcessResponse;

namespace Bitrix24RestApiClient.Api.Crm.SmartProcessTypes;

public class SmartProcessTypeContainer
{
    private EntryPointPrefix entityTypePrefix = EntryPointPrefix.SmartProcessType;
    private IBitrix24Client client;

    public SmartProcessTypeContainer(IBitrix24Client client)
    {
        this.client = client;
    }

    public async Task<FieldsResponse> Fields()
    {
        return await client.SendPostRequest<object, FieldsResponse>(entityTypePrefix, EntityMethod.Fields, new { });
    }

    public async Task<SmartProcessTypeListResponse> List()
    {
        var builder = new ListRequestBuilder<SmartProcessType>();
        return await client.SendPostRequest<CrmEntityListRequestArgs, SmartProcessTypeListResponse>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }

    public async Task<SmartProcessTypeListResponse> List(Action<IListRequestBuilder<SmartProcessType>> builderFunc)
    {
        var builder = new ListRequestBuilder<SmartProcessType>();
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityListRequestArgs, SmartProcessTypeListResponse>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
    }

    public async Task<GetResponse<SmartProcessType>> Get(int id, params Expression<Func<SmartProcessType, object>>[] fieldsExpr)
    {
        return await client.SendPostRequest<CrmEntityGetRequestArgs, GetResponse<SmartProcessType>>(entityTypePrefix, EntityMethod.Get, new CrmEntityGetRequestArgs
        {
            Id = id,
            Fields = fieldsExpr.Select(x => x.JsonPropertyName()).ToList()
        });
    }

    public async Task<DeleteResponse> Delete(int id)
    {
        return await client.SendPostRequest<CrmEntityDeleteRequestArgs, DeleteResponse>(entityTypePrefix, EntityMethod.Delete, new CrmEntityDeleteRequestArgs
        {
            Id = id
        });
    }

    public async Task<UpdateResponse> Update(int id, Action<IUpdateRequestBuilder<SmartProcessType, CrmEntityUpdateArgs>> builderFunc)
    {
        var builder = new UpdateRequestBuilder<SmartProcessType, CrmEntityUpdateArgs>();
        builder.SetId(id);
        builderFunc(builder);
        return await client.SendPostRequest<object, UpdateResponse>(entityTypePrefix, EntityMethod.Update, builder.BuildArgs());
    }

    public async Task<AddResponse> Add()
    {
        var builder = new AddRequestBuilder<SmartProcessType>();
        return await client.SendPostRequest<CrmEntityAddArgs, AddResponse>(entityTypePrefix, EntityMethod.Add, builder.BuildArgs());
    }

    public async Task<AddResponse> Add(Action<IAddRequestBuilder<SmartProcessType>> builderFunc)
    {
        var builder = new AddRequestBuilder<SmartProcessType>();
        builderFunc(builder);
        return await client.SendPostRequest<CrmEntityAddArgs, AddResponse>(entityTypePrefix, EntityMethod.Add, builder.BuildArgs());
    }
}