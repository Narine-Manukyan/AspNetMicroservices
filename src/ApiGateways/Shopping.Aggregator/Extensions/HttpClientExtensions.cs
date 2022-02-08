using System.Text.Json;

namespace Shopping.Aggregator.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

        var dataAsByteArray = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAsByteArray, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
    }
}
