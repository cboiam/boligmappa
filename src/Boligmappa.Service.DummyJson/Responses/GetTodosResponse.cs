using Boligmappa.Service.DummyJson.Responses.Abstractions;

namespace Boligmappa.Service.DummyJson.Responses;

public class GetTodosResponse : ListResponse
{
    public List<Models.Todo> Todos { get; set; }
}