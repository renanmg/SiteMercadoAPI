using NUnit.Framework;
using SiteMercadoAPI.Domain.Entities;

namespace SiteMercadoAPI.Test.Domain
{
    public class ProdutoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeveCriarProduto_ReturnTrue()
        {
            var produto = new Produto("Produto teste", 10, string.Empty);
            produto.Validate();
            Assert.IsTrue(produto.Valid);
        }

        [Test]
        public void NaoDeveCriarProduto_ReturnTrue()
        {
            var produto = new Produto("Produto teste", 0, string.Empty);
            produto.Validate();
            Assert.IsTrue(produto.Invalid);
        }
    }
}