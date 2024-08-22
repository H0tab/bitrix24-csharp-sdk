﻿using Bitrix24RestApiClient.Api.Crm.Invoices.OldInvoices.Models;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitrix24RestApiClient.Core.Models.Response.BatchResponse;
using Bitrix24RestApiClient.Api.Crm.CrmDeal.ProductRows.Models;
using Bitrix24RestApiClient.Test.Utilities;

namespace Bitrix24RestApiClient.Test.Tests.IntegrationTests
{
    public class DealProductRowTests : AbstractTest
    {
        [Fact]
        public async Task SetTest()
        {
            int? dealId = (await Bitrix24.Crm.Deals.Add(x => x.SetField(x => x.Title, "test"))).Result;
            AllocatedDeals.Add(dealId.Value);

            await Bitrix24.Crm.Deals.ProductRows.Set(dealId.Value, new List<DealProductRow>
            {
                new DealProductRow
                {
                    ProductName = "Test",
                    Price = 1
                }
            });

            var actualProductRows = (await Bitrix24.Crm.Deals.ProductRows.Get(dealId.Value)).Result;
            Assert.Equal("Test", actualProductRows.First().ProductName);
        }

        [Fact]
        public async Task GetBatchTest()
        {
            int? dealId1 = (await Bitrix24.Crm.Deals.Add(x => x.SetField(x => x.Title, "test"))).Result;
            AllocatedDeals.Add(dealId1.Value);
            int? dealId2 = (await Bitrix24.Crm.Deals.Add(x => x.SetField(x => x.Title, "test"))).Result;
            AllocatedDeals.Add(dealId2.Value);

            await Bitrix24.Crm.Deals.ProductRows.Set(dealId1.Value, new List<DealProductRow>
            {
                new DealProductRow
                {
                    ProductName = "Test",
                    Price = 1
                }
            });

            await Bitrix24.Crm.Deals.ProductRows.Set(dealId2.Value, new List<DealProductRow>
            {
                new DealProductRow
                {
                    ProductName = "Test",
                    Price = 1
                }
            });

            IAsyncEnumerable<ByIdBatchResponseItem<List<DealProductRow>>> prodactRowsIterator = Bitrix24.Crm.Deals.ProductRows.GetByDealIds(new List<int> { dealId1.Value, dealId2.Value });
            var prodactRows = new List<ByIdBatchResponseItem<List<DealProductRow>>>();
            await foreach (var prodactRow in prodactRowsIterator)
                prodactRows.Add(prodactRow);
        }
    }
}
