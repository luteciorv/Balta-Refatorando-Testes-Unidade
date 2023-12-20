namespace Store.Domain.Entities;

public class Discount(decimal amount, DateTime expireDate) : Entity
{
    public decimal Amount { get; private set; } = amount;
    public DateTime ExpireDate { get; private set; } = expireDate;

    public bool IsValid() =>
        DateTime.Compare(DateTime.Now, ExpireDate) < 0;

    public decimal Value()
    {
        if(IsValid()) return Amount;
        return 0;
    }
}
