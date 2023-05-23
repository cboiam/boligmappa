using Boligmappa.Service.DummyJson.Responses.Abstractions;

namespace Boligmappa.Service.DummyJson.Responses;

public class GetPostsResponse : ListResponse
{
    public List<Models.Post> Posts { get; set; }
}