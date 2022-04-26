using Eryaz_ProductSearch.DataLayer;
using Eryaz_ProductSearch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Eryaz_ProductSearch.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
        ProductDAL _dbProducts = new ProductDAL();

        public IActionResult List()
        {
            List<Products> ProductsList = new List<Products>();
            ProductsList = _dbProducts.GetAllProducts();
            return View(ProductsList);
        }

        public IActionResult Search(string parameters)
        {
            List<Products> ProductsList = new List<Products>();
            if (parameters != null)
            {
                ProductsList = _dbProducts.GetAllProducts();
                //var product = ProductsList.Where(x => x.ProductName.ToLower().Contains(parameters)).ToList();
                var product = ProductsList.Where(x => x.ProductName.Contains(parameters)).ToList();
                return View("List", product);
            }
            else
            {
                ProductsList = _dbProducts.GetAllProducts();
                return View("List", ProductsList);
            }
        }

        public IActionResult GetProductDetails(int id)
        {
            var product = _dbProducts.GetProductById(id);
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Products product)
        {
            _dbProducts.CreateProduct(product);
            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var product = _dbProducts.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Products product)
        {
            _dbProducts.UpdateProduct(product);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            _dbProducts.DeleteProduct(id);
            return RedirectToAction("List");
        }
    }
}
