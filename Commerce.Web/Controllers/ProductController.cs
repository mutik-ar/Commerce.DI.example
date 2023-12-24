using Commerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Parameters;
using Core.Results;
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
        public IActionResult Create([FromForm] ProductParameter productParameter)
        {
            productParameter.ProductId = Guid.NewGuid();
            Result result = RootComposite.InsertProductService.Execute(productParameter);
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
            ProductParameter productParameter = new() { Name = "", UnitPrice = "", Description ="" };
            return View(model: productParameter);
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

        [HttpGet("/products/Edit")]
        public IActionResult Edit(Guid Id)
        {
            IdProductParameter idProductParameter = new() { ProductId = Id };
            GetProductsResult result = RootComposite.GetProductService.Execute(idProductParameter); ;
            ProductParameter productParameter = new();
            if (result.Product != null)
            {
                productParameter.ProductId = result.Product.Id;
                productParameter.Name = result.Product.Name;
                productParameter.UnitPrice = result.Product.UnitPrice.ToString();
                productParameter.Description = result.Product.Description;

            }
            return View(model: productParameter); 
        }

        [HttpPost("/products/Edit")]
        public IActionResult Edit([FromForm] ProductParameter productParameter)
        {
            Result result = RootComposite.UpdateProductService.Execute(productParameter);
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
