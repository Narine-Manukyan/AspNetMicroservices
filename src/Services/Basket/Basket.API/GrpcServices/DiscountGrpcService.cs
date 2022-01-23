using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _disountProtoService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient disountProtoService)
    {
        _disountProtoService = disountProtoService ?? throw new ArgumentNullException(nameof(disountProtoService));
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };

        return await _disountProtoService.GetDiscountAsync(discountRequest);
    }
}


