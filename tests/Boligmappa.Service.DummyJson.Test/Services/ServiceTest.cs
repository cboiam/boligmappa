using Microsoft.Extensions.Options;

namespace Boligmappa.Service.DummyJson.Test.Services;

public abstract class ServiceTest
{
    protected readonly IOptions<DummyJsonSettings> options;

    public ServiceTest()
    {
        DummyJsonSettings settings = new DummyJsonSettings
        {
            BaseUrl = "https://dummyjson.com",
        };
        options = Options.Create(settings);
    }
}