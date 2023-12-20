using Flunt.Validations;

namespace Store.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem(Product product, int quantity)
    {
        AddNotifications(new Contract<OrderItem>()
            .Requires()
            .IsNotNull(product, nameof(Product), "Produto inválido")
            .IsGreaterThan(quantity, 0, nameof(Quantity), "A quantidade deve ser maior do que zero")
        );

        Product = product;
        Price = product is null ? 0 : product.Price;
        Quantity = quantity;
    }

    public Product Product { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public decimal Total() =>
        Price * Quantity;
}
