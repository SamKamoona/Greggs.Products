using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Helper;
using Greggs.Products.Api.Models;
using Moq;
using System;
using Xunit;


namespace Greggs.Products.UnitTests;

using Greggs.Products.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;

public class ProductControllerConverterUnitTest
{
    [Fact]
    public void ListProducts_Is_Successfull()
    {
        // Assign
        var product = new Product() { Name = "Greggs happy sandwitch", Price = 1m };
        IList<Product> list = new List<Product>();
        list.Add(product);
        var mockDataAccess = new Mock<IDataAccess<Product>>();
        mockDataAccess.Setup(list => list.List(It.IsAny<int>(), It.IsAny<int>())).Returns(list);
        var mockLogger = new Mock<ILogger<ProductController>>();
        var productController = new ProductController(mockDataAccess.Object, mockLogger.Object);
        // Action
        var result = productController.Get(0, 2);
        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okObjectResult.StatusCode);
        var returnedList = okObjectResult.Value as IList<Product>;
        Assert.Equal(1, returnedList.Count);

    }
}