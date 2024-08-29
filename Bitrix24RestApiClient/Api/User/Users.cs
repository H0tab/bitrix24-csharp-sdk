using Bitrix24RestApiClient.Core;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Builders.Interfaces;

namespace Bitrix24RestApiClient.Api.User;

//TODO Не проверено 
public class Users : AbstractEntities<Models.User>
{
    private readonly IBitrix24Client client;
    private readonly EntryPointPrefix entityTypePrefix = EntryPointPrefix.User;

    public Users(IBitrix24Client client) : base(client, EntryPointPrefix.User)
    {
        this.client = client;
    }

    public async Task<ListResponse<TEntity>> Search<TEntity>()
    {
        var builder = new SearchRequestBuilder<TEntity>();
        return await client.SendPostRequest<CrmSearchRequestArgs, ListResponse<TEntity>>(entityTypePrefix, EntityMethod.Search, builder.BuildArgs());
    }

    public async Task<ListResponse<TEntity>> Search<TEntity>(Action<ISearchRequestBuilder<TEntity>> builderFunc)
    {
        var builder = new SearchRequestBuilder<TEntity>();
        builderFunc(builder);
        return await client.SendPostRequest<CrmSearchRequestArgs, ListResponse<TEntity>>(entityTypePrefix, EntityMethod.Search, builder.BuildArgs());
    }
}