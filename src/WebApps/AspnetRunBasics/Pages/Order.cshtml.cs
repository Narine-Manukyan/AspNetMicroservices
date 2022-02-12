using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class OrderModel : PageModel
    {
        private readonly IOrderService _orderservice;

        public OrderModel(IOrderService orderservice)
        {
            _orderservice = orderservice ?? throw new ArgumentNullException(nameof(orderservice));
        }

        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "swn";
            Orders = await _orderservice.GetOrdersByUserName(userName);

            return Page();
        }       
    }
}