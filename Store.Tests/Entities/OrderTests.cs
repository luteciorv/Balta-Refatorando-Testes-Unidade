using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private readonly Customer _customer;
    private readonly Discount _discount;
    private readonly Order _order;

    public OrderTests()
    {
        _customer = new Customer("Nome Teste", "teste@localhost.com");
        _discount = new Discount(0, DateTime.Now.AddHours(1));
        _order = new Order(_customer, 0, _discount);
    }

    [TestMethod]
    public void Dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
    {
        Assert.AreEqual(8, _order.Number.Length);
    }

    [TestMethod]
    public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
    {
        Assert.AreEqual(EOrderStatus.WaitingPayment, _order.Status);
    }

    [TestMethod]
    public void Dado_um_pagamento_do_pedido_seu_status_deve_ser_aguardando_entrega()
    {
        var product = new Product("Produto Test", 10, true);

        _order.AddItem(product, 1);
        _order.Pay(10);

        Assert.AreEqual(EOrderStatus.WaitingDelivery, _order.Status);
    }

    [TestMethod]
    public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
    {
        _order.Cancel();

        Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
    }

    [TestMethod]
    public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
    {
        _order.AddItem(null, 1);
        Assert.AreEqual(0, _order.Items.Count);
    }

    [TestMethod]
    public void Dado_um_novo_item_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado()
    {
        var product = new Product("Produto Teste", 10, true);
        _order.AddItem(product, 0);

        Assert.AreEqual(0, _order.Items.Count);
    }

    [TestMethod]
    public void Dado_um_novo_pedido_valido_seu_total_deve_ser_50()
    {
        var product = new Product("Produto Teste", 10, true);
        _order.AddItem(product, 5);

        Assert.AreEqual(50, _order.Total());
    }

    [TestMethod]
    public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60()
    {
        var discount = new Discount(10, DateTime.Now.AddHours(-1));
        var product = new Product("Produto Teste", 10, true);

        var order = new Order(_customer, 0, discount);
        order.AddItem(product, 6);

        Assert.AreEqual(60, order.Total());
    }

    [TestMethod]
    public void Dado_um_desconto_invalido_o_valor_do_pedido_deve_ser_60()
    {
        var discount = new Discount(10, DateTime.Now.AddHours(-1)); 
        var product = new Product("Produto Teste", 10, true);

        var order = new Order(_customer, 0, discount);
        order.AddItem(product, 6);

        Assert.AreEqual(60, order.Total());
    }

    [TestMethod]
    public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50()
    {
        var discount = new Discount(10, DateTime.Now.AddHours(1));
        var product = new Product("Produto Teste", 10, true);

        var order = new Order(_customer, 0, discount);
        order.AddItem(product, 6);

        Assert.AreEqual(50, order.Total());
    }

    [TestMethod]
    public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60()
    {
        var order = new Order(_customer, 10, _discount);
        var product = new Product("Produto Teste", 10, true);

        order.AddItem(product, 5);

        Assert.AreEqual(60, order.Total());
    }

    [TestMethod]
    public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
    {
        var order = new Order(null, 0, _discount);

        Assert.AreNotEqual(0, order.Notifications.Count);
    }
}
