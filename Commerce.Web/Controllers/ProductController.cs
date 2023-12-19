using Commerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Model.CommandsServices;
using Model.Entities;
using Model.Parameters;
using Model.Results;
using System;
using System.Diagnostics;

namespace Commerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ProductController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/products/")]
        public IActionResult Products()
        {
            IEnumerable<Product> products = RootComposite.GetAllProductService.Execute(new NullParameter()).Products;
            return View(model: products);
        }

        [HttpPost("/products/Create")]
        public IActionResult Create([FromForm] InsertProductParameter insertProductParameter)
        {
            Result result = RootComposite.InsertProductService.Execute(insertProductParameter);
            string message = "Fail";
            if (result.Code == 0)
            {
                RootComposite.Database.Save();
                message = "Success";
            }
            return RedirectToAction(nameof(Result), new { Message = message });
        }

        [HttpGet("/products/Create")]
        public IActionResult Create()
        {
            InsertProductParameter insertProductParameter = new() { ProductId = Guid.NewGuid(), Name = "", UnitPrice = "", Description ="" };
            return View(model: insertProductParameter);
        }

        [HttpGet("/products/Delete")]
        public IActionResult Delete(Guid Id)
        {
            IdProductParameter idProductParameter = new() { ProductId = Id };
            Result result = RootComposite.DeleteProductService.Execute(idProductParameter); ;
            string message = "Fail";
            if (result.Code == 0)
            {
                RootComposite.Database.Save();
                message = "Success";
            }
            return RedirectToAction(nameof(Result), new { Message = message });
        }

        [HttpGet("/products/Result")]
        public IActionResult Result(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
