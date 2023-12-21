using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private readonly IList<Product> _products;

    public ProductQueriesTests()
    {
        _products = new List<Product>
        {
            new("Produto 01", 10, true),
            new("Produto 02", 20, true),
            new("Produto 03", 30, true),
            new("Produto 04", 40, false),
            new("Produto 05", 50, false),
        };
    }

    [TestMethod]
    public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3()
    {
        var products = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());

        Assert.AreEqual(3, products.Count());
    }

    [TestMethod]
    public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2()
    {
        var products = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());

        Assert.AreEqual(2, products.Count());
    }
}
