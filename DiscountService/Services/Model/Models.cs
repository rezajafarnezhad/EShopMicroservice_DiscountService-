namespace DiscountService.Services.Model;

public class DiscountModel
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public decimal Amount { get; set; }
    public bool Used { get; set; }
}