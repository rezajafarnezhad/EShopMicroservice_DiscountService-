using DiscountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.Infrastructure;

public class DiscountDatebaseContext : DbContext
{
    public DiscountDatebaseContext(DbContextOptions<DiscountDatebaseContext> options) : base(options) { }
    public DbSet<Discount> Discounts { get; set; }
}