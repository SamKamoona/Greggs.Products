using Greggs.Products.Api.Helper;
using Greggs.Products.Api.Models;
using System;
using Xunit;


namespace Greggs.Products.UnitTests;

public class ProductCurrencyConverterUnitTest
{
    [Fact]
    public void ChangingCurrency_Is_Successfull()
    {
        // Assign
        var product = new Product() { Name = "Greggs happy sandwitch", Price = 1m };
        // Action
        product.ConvertPrice(Api.Types.Currency.Euro);
        // Assert
        Assert.Equal(1.1m, product.Price);
    }
}