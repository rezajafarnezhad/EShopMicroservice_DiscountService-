using DiscountService.Domain.Entities;
using DiscountService.Infrastructure;
using DiscountService.Services.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.Services;

public interface IDiscountService
{
    Task<DiscountModel> GetDiscountBy(string code);
    Task<bool> UseDiscountBy(string code);
    Task<bool> AddDiscount(string code, decimal amount);

}


public class DiscountService : IDiscountService
{
    private readonly DiscountDatebaseContext _context;
    public DiscountService(DiscountDatebaseContext context)
    {
        _context = context;
    }

    public async Task<DiscountModel> GetDiscountBy(string code)
    {
        var discount = await _context.Discounts.AsNoTracking().SingleOrDefaultAsync(c => c.Code == code);
        if (discount is null)
            throw new Exception("Discount not found ...");

        return discount.Adapt(new DiscountModel());
    }

    public async Task<bool> UseDiscountBy(string code)
    {
        var discount = await _context.Discounts.AsNoTracking().SingleOrDefaultAsync(c => c.Code == code);
        if (discount is null)
            throw new Exception("Discount not found ...");

        discount.UseDiscount();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddDiscount(string code, decimal amount)
    {
        var discount = await _context.Discounts.AsNoTracking().SingleOrDefaultAsync(c => c.Code == code);
        if (discount is not null)
            throw new Exception("Discount not found ...");

        var Newdiscount = new Discount(code, amount);
        _context.Discounts.Add(Newdiscount);
        await _context.SaveChangesAsync();
        return true;
    }
}