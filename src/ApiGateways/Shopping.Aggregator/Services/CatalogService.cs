using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _client;

    public CatalogService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<ProductModel>> GetProducts()
    {
       var response = await _client.GetAsync("/api/v1/Catalog");
       return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> GetProduct(string id)
    {
        var response = await _client.GetAsync($"/api/v1/Catalog/{id}");
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<IEnumerable<ProductModel>> GetProductByCategory(string category)
    {
        var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
        return await response.ReadContentAs<List<ProductModel>>();
    }
}
