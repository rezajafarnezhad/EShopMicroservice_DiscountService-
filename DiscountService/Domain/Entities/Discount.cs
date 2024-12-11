namespace DiscountService.Domain.Entities;

public class Discount
{
    private Discount()
    {

    }

    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public decimal Amount { get; private set; }
    public bool Used { get; private set; }

    public Discount(string code, decimal amount)
    {
        Code = code;
        Amount = amount;
        Used = false;
    }


    public void UseDiscount() => Used = true;

}