using Boligmappa.Core.Entities;
using Boligmappa.Core.Services;
using Boligmappa.Core.Extensions;
using Boligmappa.Service.DummyJson.Responses;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Boligmappa.Service.DummyJson.Services;

public class UserService : IUserService
{
    private readonly string endpoint;

    public UserService(IOptions<DummyJsonSettings> settings)
    {
        endpoint = settings.Value.BaseUrl;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var response = await endpoint.AppendPathSegment(DummyJsonSettings.UserPath)
            .RemovePagination()
            .GetJsonAsync<GetUsersResponse>();
        
        return response?.Users?.ToEntity();
    }
}