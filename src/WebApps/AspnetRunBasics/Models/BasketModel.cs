namespace AspnetRunBasics.Models;

public class BasketModel
{
    public string UserName { get; set; }
    public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
    public decimal TotalPrice { get { return Items.Sum(item => item.Price * item.Quantity); } }
}

