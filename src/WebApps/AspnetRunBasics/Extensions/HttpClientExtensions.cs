using System.Net.Http.Headers;
using System.Text.Json;

namespace AspnetRunBasics.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

        var dataAsByteArray = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAsByteArray, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
    {

        var dataAsByteArray = JsonSerializer.SerializeToUtf8Bytes(data);
        var content = new ByteArrayContent(dataAsByteArray);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return httpClient.PostAsync(url, content);
    }

    public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
    {

        var dataAsByteArray = JsonSerializer.SerializeToUtf8Bytes(data);
        var content = new ByteArrayContent(dataAsByteArray);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return httpClient.PutAsync(url, content);
    }
}
