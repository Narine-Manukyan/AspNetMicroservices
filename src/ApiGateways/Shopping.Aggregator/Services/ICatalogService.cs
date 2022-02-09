using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public interface ICatalogService
{
    Task<IEnumerable<ProductModel>> GetProducts();
    Task<IEnumerable<ProductModel>> GetProductByCategory(string category);
    Task<ProductModel> GetProduct(string id);

}
