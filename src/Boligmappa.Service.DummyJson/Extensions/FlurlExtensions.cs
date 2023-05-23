using Flurl;

namespace Boligmappa.Core.Extensions;

public static class FlurlExtensions
{
    public static Url RemovePagination(this Url url) =>
        url.SetQueryParam("limit", 0);
}