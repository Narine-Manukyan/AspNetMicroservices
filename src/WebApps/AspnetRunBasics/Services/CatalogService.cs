using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _client;

    public CatalogService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<ProductModel>> GetProducts()
    {
        var response = await _client.GetAsync("/Catalog");
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> GetProduct(string id)
    {
        var response = await _client.GetAsync($"/Catalog/{id}");
        return await response.ReadContentAs<ProductModel>();
    }

    public async Task<IEnumerable<ProductModel>> GetProductByCategory(string category)
    {
        var response = await _client.GetAsync($"/Catalog/GetProductByCategory/{category}");
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> CreateProduct(ProductModel model)
    {
        var response = await _client.PostAsJson($"/Catalog", model);
        if(!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong when calling catalog api");

        return await response.ReadContentAs<ProductModel>();
    }
}