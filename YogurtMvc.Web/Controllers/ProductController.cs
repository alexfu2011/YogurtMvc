using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YogurtMvc.Pagination;
using YogurtMvc.Web.Models;

namespace YogurtMvc.Web.Controllers
{
    public class ProductController : Controller
    {
        private const int DefaultPageSize = 10;
        private IList<Product> allProducts = new List<Product>();

        public ProductController()
        {
            InitializeProducts();
        }

        public IActionResult Index(int? page)
        {
            var currentPage = page ?? 1;
            var pageData = allProducts.ToPagedList(currentPage, DefaultPageSize, allProducts.Count);
            return View(pageData);
        }

        private void InitializeProducts()
        {
            for (var i = 0; i < 527; i++)
            {
                allProducts.Add(new Product { Name = "Product " + (i + 1) });
            }
        }
    }
}
