using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    private readonly OrderHandler _orderHandler;

    private readonly IList<CreateOrderItemCommand> _items;

    public OrderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _productRepository = new FakeProductRepository();
        _orderRepository = new FakeOrderRepository();

        _orderHandler = new(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);

        _items = new List<CreateOrderItemCommand>
        {
            new(Guid.NewGuid(), 1),
            new(Guid.NewGuid(), 2),
            new(Guid.NewGuid(), 1),
        };
    }

    [TestMethod]
    public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
    {
        var createOrderCommand = new CreateOrderCommand("cliente-invalido", "12345678", "123456789", _items);
        var result = (GenericCommandResult) _orderHandler.Handle(createOrderCommand);

        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public void Dado_um_cep_invalido_o_pedido_nao_deve_ser_gerado()
    {

        var createOrderCommand = new CreateOrderCommand("12345678910", "cep-invalido", "123456789", _items);
        var result = (GenericCommandResult)_orderHandler.Handle(createOrderCommand);

        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
    {
        var createOrderCommand = new CreateOrderCommand("123", "11", "00", null);
        createOrderCommand.Validate();

        Assert.IsFalse(createOrderCommand.IsValid);
    }

    [TestMethod]
    public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
    {
        var createOrderCommand = new CreateOrderCommand("12345678910", "12345678", "123456789", _items);
        var result = (GenericCommandResult)_orderHandler.Handle(createOrderCommand);

        Assert.IsTrue(result.Success);
        Assert.IsFalse(_orderHandler.IsValid);
    }
}
