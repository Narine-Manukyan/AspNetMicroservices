namespace Shopping.Aggregator.Models;

public class ShoppingModel
{
    public string UserName { get; set; }
    public BasketModel BasketModelWithProducts { get; set; }
    public IEnumerable<OrderResponseModel> Orders { get; set; }
}
