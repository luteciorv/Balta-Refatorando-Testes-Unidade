using Store.Domain.Commands;

namespace Store.Tests.Commands;

[TestClass]
public class CreateOrderCommandTests
{
    [TestMethod]
    public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
    {
        IList<CreateOrderItemCommand> items = new List<CreateOrderItemCommand>
        {
            new(Guid.NewGuid(), 1),
            new(Guid.NewGuid(), 1)
        };
        var command = new CreateOrderCommand("", "123456", "12345", items);

        command.Validate();

        Assert.IsFalse(command.IsValid);
    }
}
