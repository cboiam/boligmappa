using System.Net;
using Boligmappa.Core.Entities;

namespace Boligmappa.Service.DummyJson.Test.Services;

public class UserServiceTest : ServiceTest
{
    private readonly UserService instance;

    public UserServiceTest() : base()
    {
        instance = new UserService(options);
    }

    [Fact]
    public async Task GetUsers_ValidateRequest()
    {
        using (var httpTest = new HttpTest())
        {
            var result = await instance.GetUsers();

            httpTest.ShouldHaveCalled("https://dummyjson.com/users")
                .WithQueryParam("limit", 0);
        }
    }

    [Fact]
    public async Task GetUsers_EmptyResult()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(@"{ ""users"": [] }", 
                HttpStatusCode.OK.GetHashCode());

            var result = await instance.GetUsers();

            result.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task GetUsers_ReturnUser_ShouldBeMappedCorrectly()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(@"
                { 
                    ""users"": [{ 
                        ""id"": 1, 
                        ""userName"": ""test"", 
                        ""bank"": { ""cardType"": ""card"" } 
                    }]
                }", 
                HttpStatusCode.OK.GetHashCode());

            var result = await instance.GetUsers();

            result.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainEquivalentOf(new User(1)
                {
                    UserName = "test",
                    CreditCard = "card"
                });
        }
    }

    [Fact]
    public async Task GetUsers_PropagateExceptionOnInternalServerError()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(string.Empty, HttpStatusCode.InternalServerError.GetHashCode());

            Func<Task> call = async () => await instance.GetUsers();

            await call.Should().ThrowAsync<FlurlHttpException>();
        }
    }
}