using Bitrix24RestApiClient.Api;
using Bitrix24RestApiClient.Core.Client;

namespace Bitrix24RestApiClient.Test.Utilities;

public abstract class AbstractTest : IDisposable
{
    protected readonly Bitrix24 Bitrix24;
    protected readonly List<int> AllocatedDeals = [];
    protected readonly List<int> AllocatedProducts = [];
    protected readonly List<int> AllocatedContacts = [];
    protected readonly List<int> AllocatedLeads = [];
    protected readonly List<int> AllocatedCompanies = [];
    protected readonly List<int> AllocatedRequisites = [];
    protected readonly List<int> AllocatedOldInvoices = [];
    protected readonly List<int> AllocatedTimelineComments = [];
    protected readonly List<int> AllocatedActivities = [];
    protected readonly List<int> AllocatedTasks = [];

    protected AbstractTest()
    {
        var client = new Bitrix24Client(Constants.WebhookUrl, new DummyLogger<Bitrix24Client>());
        Bitrix24 = new Bitrix24(client);
    }

    public void Dispose()
    {
        var tasks = AllocatedOldInvoices.Select(id => Bitrix24.Crm.Invoices.Old.Delete(id)).Cast<Task>().ToList();
        tasks.AddRange(AllocatedTimelineComments.Select(id => Bitrix24.Crm.Timeline.Comments.Delete(id)));
        tasks.AddRange(AllocatedCompanies.Select(id => Bitrix24.Crm.Companies.Delete(id)));
        tasks.AddRange(AllocatedDeals.Select(id => Bitrix24.Crm.Deals.Delete(id)));
        tasks.AddRange(AllocatedLeads.Select(id => Bitrix24.Crm.Leads.Delete(id)));
        tasks.AddRange(AllocatedContacts.Select(id => Bitrix24.Crm.Contacts.Delete(id)));
        tasks.AddRange(AllocatedProducts.Select(id => Bitrix24.Crm.Products.Delete(id)));
        tasks.AddRange(AllocatedRequisites.Select(id => Bitrix24.Crm.Requisites.Delete(id)));
        tasks.AddRange(AllocatedActivities.Select(id => Bitrix24.Crm.Activities.Delete(id)));
        tasks.AddRange(AllocatedTasks.Select(id => Bitrix24.Tasks.Delete(id)));

        Task.WaitAll(tasks.ToArray());
    }
}