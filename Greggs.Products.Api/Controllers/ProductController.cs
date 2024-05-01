using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Helper;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Types;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IDataAccess<Product> _dataAccess;

    public ProductController(IDataAccess<Product> dataAccess, ILogger<ProductController> logger)
    {
        _dataAccess = dataAccess;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get(int pageStart = 0, int pageSize = 5, Currency currency = Currency.Pound)
    {
        if (pageStart < 0 || pageSize < 1)
        {
            return StatusCode(400, "invalid input");
        }
        try
        {
            _logger.LogTrace($"Call get products page start {pageStart} page size {pageSize}");
            List<Product> products = _dataAccess.List(pageStart, pageSize).ToList();

            if (currency is Currency.Euro)
            {
                products.ForEach(x => x.ConvertPrice(Currency.Euro));
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500);
        }
    }
}