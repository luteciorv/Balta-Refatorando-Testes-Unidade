namespace Store.Domain.Entities;

public class Product(string title, decimal price, bool active) : Entity
{
    public string Title { get; private set; } = title;
    public decimal Price { get; private set; } = price;
    public bool Active { get; private set; } = active;
}
