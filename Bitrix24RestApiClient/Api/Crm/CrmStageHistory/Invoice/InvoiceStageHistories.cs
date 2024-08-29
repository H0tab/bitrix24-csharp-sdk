using System;
using System.Threading.Tasks;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Builders;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.BatchStrategies;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.Response.ListItemsResponse;
using Bitrix24RestApiClient.Api.Crm.CrmStageHistory.Invoice.Models;
using Bitrix24RestApiClient.Core.BatchStrategies.BatchOperations;

namespace Bitrix24RestApiClient.Api.Crm.CrmStageHistory.Invoice
{
    public class InvoiceStageHistories
    {
        private EntryPointPrefix entityTypePrefix = EntryPointPrefix.StageHistory;
        private IBitrix24Client client;

        public InvoiceStageHistories(IBitrix24Client client)
        {
            this.client = client;
            this.BatchOperations = new BatchOperationsForListResponse(client, entityTypePrefix);
        }

        public BatchOperationsForListResponse BatchOperations { get; private set; }

        public async Task<ListItemsResponse<ListItems<InvoiceStageHistory>, InvoiceStageHistory>> List()
        {
            var builder = new ListRequestBuilder<InvoiceStageHistory>();
            builder.SetEntityTypeId(EntityTypeIdEnum.Invoice);
            return await client.SendPostRequest<CrmEntityListRequestArgs, ListItemsResponse<ListItems<InvoiceStageHistory>, InvoiceStageHistory>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
        }

        public async Task<ListItemsResponse<ListItems<InvoiceStageHistory>, InvoiceStageHistory>> List(Action<IStageHistoriesListRequestBuilder<InvoiceStageHistory>> builderFunc)
        {
            var builder = new ListRequestBuilder<InvoiceStageHistory>();
            builder.SetEntityTypeId(EntityTypeIdEnum.Invoice);
            builderFunc(builder);
            return await client.SendPostRequest<CrmEntityListRequestArgs, ListItemsResponse<ListItems<InvoiceStageHistory>, InvoiceStageHistory>>(entityTypePrefix, EntityMethod.List, builder.BuildArgs());
        }
    }
}
