namespace Boligmappa.Service.DummyJson.Responses.Abstractions;

public abstract class ListResponse
{
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}