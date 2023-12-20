using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities;

public class Order : Entity
{
    public Order(Customer customer, decimal deliveryFee, Discount discount)
    {
        AddNotifications(new Contract<Order>()
            .Requires()
            .IsNotNull(customer, nameof(Customer), "Cliente inválido")
        );

        Customer = customer;
        Date = DateTime.Now;
        Number = Guid.NewGuid().ToString()[..8];
        Status = EOrderStatus.WaitingPayment;
        DeliveryFee = deliveryFee;
        Discount = discount;
        Items = [];
    }

    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public string Number { get; private set; }
    public decimal DeliveryFee { get; private set; }
    public EOrderStatus Status { get; private set; }

    public Discount Discount { get; private set; }
    public IList<OrderItem> Items { get; private set; }

    public void AddItem(Product product, int quantity)
    {
        var item = new OrderItem(product, quantity);
        if(item.IsValid)
            Items.Add(item);
    }

    public decimal Total()
    {
        decimal total = Items.Sum(i => i.Total());
        total += DeliveryFee;
        total -= Discount is null ? 0 : Discount.Value();

        return total;
    }

    public void Pay(decimal amount)
    {
        if (amount == Total()) 
            Status = EOrderStatus.WaitingDelivery;
    }

    public void Cancel() =>
        Status = EOrderStatus.Canceled;
}
