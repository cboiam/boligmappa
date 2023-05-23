using Boligmappa.Service.DummyJson.Responses.Abstractions;

namespace Boligmappa.Service.DummyJson.Responses;

public class GetUsersResponse : ListResponse
{
    public List<Models.User> Users { get; set; }
}