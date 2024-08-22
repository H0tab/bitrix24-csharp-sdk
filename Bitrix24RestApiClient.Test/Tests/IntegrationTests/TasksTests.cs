using Bitrix24RestApiClient.Test.Utilities;
using Xunit;

namespace Bitrix24RestApiClient.Test.Tests.IntegrationTests;

public class TasksTests : AbstractTest
{
    
    //TODO: Fix tests
    [Fact]
    public async Task AddTest()
    {
        var taskId = (await Bitrix24.Tasks.Add(x => x
            .SetField(a => a.Title, "test")
            .SetField(a => a.ResponsibleId, 1)
            .SetField(a => a.CreatedBy, 1)
            .SetField(a => a.ForumId, 1))).Result.Task.Id;
        AllocatedTasks.Add(taskId);
        
        var task = (await Bitrix24.Tasks.Get<Api.Task.Models.Task>(taskId, entity => entity.Id)).Id;
        Assert.Equal(taskId, task);
    }

    [Fact]
    public async Task ListTest()
    {
        var taskId = (await Bitrix24.Tasks.Add(x => x
            .SetField(a => a.Title, "test")
            .SetField(a => a.ResponsibleId, 1)
            .SetField(a => a.CreatedBy, 1)
            .SetField(a => a.ForumId, 1))).Result.Task.Id;
        AllocatedTasks.Add(taskId);

        var response = await Bitrix24.Tasks.List(x=>x
            .AddFilter(a=>a.Id, taskId)
            .AddSelect(a=>a.Title));

        Assert.Equal("test", response.Result.First().Title);
    }

    [Fact]
    public async Task FirstTest()
    {
        int taskId = (await Bitrix24.Tasks.Add(x => x
            .SetField(a => a.Title, "test")
            .SetField(a => a.ResponsibleId, 1)
            .SetField(a => a.CreatedBy, 1)
            .SetField(a => a.ForumId, 1))).Result.Task.Id;
        AllocatedTasks.Add(taskId);

        var task = await Bitrix24.Tasks.First(x => x
            .AddFilter(a => a.Id, taskId)
            .AddSelect(a => a.Title));

        Assert.Equal("test", task.Title);
    }

    [Fact]
    public async Task UpdateTest()
    {
        var taskId = (await Bitrix24.Tasks.Add(x => x
            .SetField(a => a.Title, "test")
            .SetField(a => a.ResponsibleId, 1)
            .SetField(a => a.CreatedBy, 1)
            .SetField(a => a.ForumId, 1))).Result.Task.Id;
        AllocatedTasks.Add(taskId);

        await Bitrix24.Tasks.Update(taskId, x => x.SetField(a => a.Title, "buzz"));

        var task = (await Bitrix24.Tasks.Get(taskId, a=>a.Title)).Result;
        Assert.Equal("buzz", task.Title);
    }

    [Fact]
    public async Task FieldsTest()
    {
        var fields = (await Bitrix24.Tasks.Fields());
    }

    [Fact]
    public async Task DeleteTest()
    {
        var taskId = (await Bitrix24.Tasks.Add(x => x
            .SetField(a => a.Title, "test")
            .SetField(a => a.ResponsibleId, 1)
            .SetField(a => a.CreatedBy, 1)
            .SetField(a => a.ForumId, 1))).Result.Task.Id;

        var deleteResponse = (await Bitrix24.Tasks.Delete(taskId));

        Assert.ThrowsAsync<Exception>(async ()=>
        {
            var task = (await Bitrix24.Tasks.Get(taskId)).Result;
        });
    }
}