using System.Net;
using Boligmappa.Core.Entities;

namespace Boligmappa.Service.DummyJson.Test.Services;

public class TodoServiceTest : ServiceTest
{
    private readonly TodoService instance;

    public TodoServiceTest() : base()
    {
        instance = new TodoService(options);
    }

    [Fact]
    public async Task GetTodos_ValidateRequest()
    {
        using (var httpTest = new HttpTest())
        {
            var result = await instance.GetTodos();

            httpTest.ShouldHaveCalled("https://dummyjson.com/todos")
                .WithQueryParam("limit", 0);
        }
    }

    [Fact]
    public async Task GetTodos_EmptyResult()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(@"{ ""todos"": [] }",
                HttpStatusCode.OK.GetHashCode());

            var result = await instance.GetTodos();

            result.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task GetTodos_ReturnUser_ShouldBeMappedCorrectly()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(@"
                { 
                    ""todos"": [{ 
                        ""id"": 1, 
                        ""userId"": 1, 
                        ""todo"": ""test"", 
                        ""completed"": true 
                    }]
                }",
                HttpStatusCode.OK.GetHashCode());

            var result = await instance.GetTodos();

            result.Should().NotBeEmpty()
                .And.ContainEquivalentOf(new Todo(1)
                {
                    UserId = 1,
                    Description = "test",
                    Completed = true
                });
        }
    }

    [Fact]
    public async Task GetTodos_PropagateExceptionOnInternalServerError()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(string.Empty, HttpStatusCode.InternalServerError.GetHashCode());

            Func<Task> call = async () => await instance.GetTodos();

            await call.Should().ThrowAsync<FlurlHttpException>();
        }
    }
}