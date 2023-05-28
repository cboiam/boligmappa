using Boligmappa.Core.Entities;
using Boligmappa.Core.Services;
using Boligmappa.Core.Extensions;
using Boligmappa.Service.DummyJson.Responses;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Boligmappa.Service.DummyJson.Services;

public class PostService : IPostService
{
    private readonly string endpoint;

    public PostService(IOptions<DummyJsonSettings> settings)
    {
        endpoint = settings.Value.BaseUrl;
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        var response = await endpoint.AppendPathSegment(DummyJsonSettings.PostPath)
            .RemovePagination()
            .GetJsonAsync<GetPostsResponse>();

        return response?.Posts?.ToEntity();
    }
}