using System;
using System.Linq;
using Xunit;
using System.Threading.Tasks;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Api.Crm.Requisite.Models;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Bitrix24RestApiClient.Test.Utilities;

namespace Bitrix24RestApiClient.Test.Tests.IntegrationTests
{
    public class RequisitesTests : AbstractTest
    {
        [Fact]
        public async Task AddTest()
        {
            int? companyId = (await Bitrix24.Crm.Companies.Add()).Result;
            AllocatedCompanies.Add(companyId.Value);

            int? requisiteId = (await Bitrix24.Crm.Requisites.Add(x => x
                .SetField(x => x.EntityTypeId, EntityTypeIdEnum.Company.EntityTypeId)
                .SetField(x => x.EntityId, companyId)
                .SetField(x => x.PresetId, 1)
                .SetField(x => x.Name, "test")
                .SetField(x => x.RqEmail, "t@t.ru"))).Result;
            AllocatedRequisites.Add(requisiteId.Value);

            CrmRequisite requisite = (await Bitrix24.Crm.Requisites.Get(requisiteId.Value)).Result;
            Assert.Equal(requisiteId.Value, requisite.Id);
        }

        [Fact]
        public async Task ListTest()
        {
            int? companyId = (await Bitrix24.Crm.Companies.Add()).Result;
            AllocatedCompanies.Add(companyId.Value);

            int? requisiteId = (await Bitrix24.Crm.Requisites.Add(x => x
                .SetField(x => x.EntityTypeId, EntityTypeIdEnum.Company.EntityTypeId)
                .SetField(x => x.EntityId, companyId)
                .SetField(x => x.PresetId, 1)
                .SetField(x => x.Name, "test")
                .SetField(x => x.RqEmail, "t@t.ru"))).Result;
            AllocatedRequisites.Add(requisiteId.Value);

            ListResponse<CrmRequisite> response = await Bitrix24.Crm.Requisites.List(x=>x
                .AddFilter(x=>x.Id, requisiteId.Value)
                .AddSelect(x=>x.RqEmail));

            Assert.Equal("t@t.ru", response.Result.First().RqEmail);
        }

        [Fact]
        public async Task FirstTest()
        {
            int? companyId = (await Bitrix24.Crm.Companies.Add()).Result;
            AllocatedCompanies.Add(companyId.Value);

            int? requisiteId = (await Bitrix24.Crm.Requisites.Add(x => x
                 .SetField(x => x.EntityTypeId, EntityTypeIdEnum.Company.EntityTypeId)
                 .SetField(x => x.EntityId, companyId)
                .SetField(x => x.PresetId, 1)
                .SetField(x => x.Name, "test")
                 .SetField(x => x.RqEmail, "t@t.ru"))).Result;
            AllocatedRequisites.Add(requisiteId.Value);

            CrmRequisite requisite = await Bitrix24.Crm.Requisites.First(x => x
                .AddFilter(x => x.Id, requisiteId.Value)
                .AddSelect(x => x.RqEmail));

            Assert.Equal("t@t.ru", requisite.RqEmail);
        }

        [Fact]
        public async Task UpdateTest()
        {
            int? companyId = (await Bitrix24.Crm.Companies.Add()).Result;
            AllocatedCompanies.Add(companyId.Value);

            int? requisiteId = (await Bitrix24.Crm.Requisites.Add(x => x
                .SetField(x => x.EntityTypeId, EntityTypeIdEnum.Company.EntityTypeId)
                .SetField(x => x.EntityId, companyId)
                .SetField(x => x.PresetId, 1)
                .SetField(x => x.Name, "test")
                .SetField(x => x.RqEmail, "t@t.ru"))).Result;
            AllocatedRequisites.Add(requisiteId.Value);

            await Bitrix24.Crm.Requisites.Update(requisiteId.Value, x => x.SetField(x => x.RqEmail, "f@f.ru"));

            CrmRequisite requisite = (await Bitrix24.Crm.Requisites.Get(requisiteId.Value, x=>x.RqEmail)).Result;
            Assert.Equal("f@f.ru", requisite.RqEmail);
        }

        [Fact]
        public async Task FieldsTest()
        {
            FieldsResponse fields = (await Bitrix24.Crm.Requisites.Fields());
        }

        [Fact]
        public async Task DeleteTest()
        {
            int? companyId = (await Bitrix24.Crm.Companies.Add()).Result;
            AllocatedCompanies.Add(companyId.Value);

            int? requisiteId = (await Bitrix24.Crm.Requisites.Add(x => x
                .SetField(x => x.EntityTypeId, EntityTypeIdEnum.Company.EntityTypeId)
                .SetField(x => x.EntityId, companyId)
                .SetField(x => x.PresetId, 1)
                .SetField(x => x.Name, "test")
                .SetField(x => x.RqEmail, "t@t.ru"))).Result;
            AllocatedRequisites.Add(requisiteId.Value);

            DeleteResponse deleteResponse = (await Bitrix24.Crm.Requisites.Delete(requisiteId.Value));

            Assert.ThrowsAsync<Exception>(async ()=>
            {
                CrmRequisite requisite = (await Bitrix24.Crm.Requisites.Get(requisiteId.Value)).Result;
            });
        }
    }
}
