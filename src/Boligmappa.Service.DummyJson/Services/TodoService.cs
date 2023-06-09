using Boligmappa.Core.Entities;
using Boligmappa.Core.Services;
using Boligmappa.Core.Extensions;
using Boligmappa.Service.DummyJson.Responses;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Boligmappa.Service.DummyJson.Services;

public class TodoService : ITodoService
{
    private readonly DummyJsonSettings settings;
    private readonly string endpoint;

    public TodoService(IOptions<DummyJsonSettings> settings)
    {
        this.settings = settings.Value;
        endpoint = this.settings.BaseUrl;
    }

    public async Task<IEnumerable<Todo>> GetTodos()
    {
        var response = await endpoint.AppendPathSegment(DummyJsonSettings.TodoPath)
            .RemovePagination()
            .GetJsonAsync<GetTodosResponse>();

        return response.Todos.ToEntity();
    }
}