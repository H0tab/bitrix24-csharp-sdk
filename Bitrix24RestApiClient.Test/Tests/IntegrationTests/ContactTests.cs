using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Bitrix24RestApiClient.Core.Models.Response;
using Bitrix24RestApiClient.Api.Crm.CrmContact.Models;
using Bitrix24RestApiClient.Test.Utilities;

namespace Bitrix24RestApiClient.Test.Tests.IntegrationTests
{
    public class ContactTests : AbstractTest
    {
        [Fact]
        public async Task AddTest()
        {
            int? contactId = (await Bitrix24.Crm.Contacts.Add(x => x.SetField(x => x.Name, "test"))).Result;
            AllocatedContacts.Add(contactId.Value);

            Contact contact = (await Bitrix24.Crm.Contacts.Get(contactId.Value)).Result;
            Assert.Equal(contactId.Value, contact.Id);
        }

        [Fact]
        public async Task ListTest()
        {
            int? contactId = (await Bitrix24.Crm.Contacts.Add(x => x.SetField(x => x.Name, "test"))).Result;
            AllocatedContacts.Add(contactId.Value);

            ListResponse<Contact> response = await Bitrix24.Crm.Contacts.List(x=>x
                .AddFilter(x=>x.Id, contactId.Value)
                .AddSelect(x=>x.Name));

            Assert.Equal("test", response.Result.First().Name);
        }

        [Fact]
        public async Task FirstTest()
        {
            int? contactId = (await Bitrix24.Crm.Contacts.Add(x => x.SetField(x => x.Name, "test"))).Result;
            AllocatedContacts.Add(contactId.Value);

            Contact contact = await Bitrix24.Crm.Contacts.First(x => x
                .AddFilter(x => x.Id, contactId.Value)
                .AddSelect(x => x.Name));

            Assert.Equal("test", contact.Name);
        }

        [Fact]
        public async Task UpdateTest()
        {
            int? contactId = (await Bitrix24.Crm.Contacts.Add(x => x.SetField(x => x.Name, "fizz"))).Result;
            AllocatedContacts.Add(contactId.Value);

            await Bitrix24.Crm.Contacts.Update(contactId.Value, x => x.SetField(x => x.Name, "buzz"));

            Contact contact = (await Bitrix24.Crm.Contacts.Get(contactId.Value, x=>x.Name)).Result;
            Assert.Equal("buzz", contact.Name);
        }

        [Fact]
        public async Task FieldsTest()
        {
            FieldsResponse fields = (await Bitrix24.Crm.Contacts.Fields());
        }

        [Fact]
        public async Task DeleteTest()
        {
            int? contactId = (await Bitrix24.Crm.Contacts.Add(x => x.SetField(x => x.Name, "test"))).Result;

            DeleteResponse deleteResponse = (await Bitrix24.Crm.Contacts.Delete(contactId.Value));

            Assert.ThrowsAsync<Exception>(async ()=>
            {
                Contact contact = (await Bitrix24.Crm.Contacts.Get(contactId.Value)).Result;
            });
        }
    }
}
