using DiscountService.Proto;
using DiscountService.Services;
using Grpc.Core;
using Mapster;

namespace DiscountService.Grpc;

public class GrpcDiscountService : DiscountServiceProto.DiscountServiceProtoBase
{
    private readonly IDiscountService _discountService;
    public GrpcDiscountService(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    public override async Task<ResponseGetDicountBy> GetDiscountById(RequestGetDiscountById request, ServerCallContext context)
    {
        var discount = await _discountService.GetDiscountById(new Guid(request.Id));
        if (discount is null)
            return new ResponseGetDicountBy()
            {
                IsSuccess = false,
                Message = "",
                Date = null
            };

        return new ResponseGetDicountBy()
        {
            IsSuccess = true,
            Message = "",
            Date = discount.Adapt(new DiscountInfo())
        };
    }

    public override async Task<ResponseGetDicountBy> GetDiscountBy(RequestGetDiscountBy request,
        ServerCallContext context)
    {
        var discount = await _discountService.GetDiscountBy(request.Code);

        if (discount is null)
            return new ResponseGetDicountBy()
            {
                IsSuccess = false,
                Message = "",
                Date = null
            };

        return new ResponseGetDicountBy()
        {
            IsSuccess = true,
            Message = "",
            Date = discount.Adapt(new DiscountInfo())
        };
    }

    public override async Task<ResponseStatus> UseDiscountBy(RequestGetDiscountBy request, ServerCallContext context)
    {
        var value = await _discountService.UseDiscountBy(request.Code);
        return new ResponseStatus() { IsSuccess = value };
    }

    public override async Task<ResponseStatus> AddDiscount(RequestAddDicount request, ServerCallContext context)
    {
        var value = await _discountService.AddDiscount(request.Code, request.Amount);
        return new ResponseStatus() { IsSuccess = value };
    }
}