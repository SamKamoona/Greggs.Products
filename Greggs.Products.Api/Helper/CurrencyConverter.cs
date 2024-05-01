using Greggs.Products.Api.Models;
using Greggs.Products.Api.Types;
using System;

namespace Greggs.Products.Api.Helper
{
    public static class CurrencyConverter
    {
        // Setting Euro exchange rate as a hard coded value so not to over complicate the code.
        // In real life solution, I would get the value from a database, settings file, etc.
        private static decimal EuroExchangeRate = 1.1m;
        public static Product ConvertPrice(this Product product, Currency currency)
        {
            if (currency is Currency.Euro)
            {
                product.Price = product.Price * EuroExchangeRate;
            }
            return product;
        }
    }
}
