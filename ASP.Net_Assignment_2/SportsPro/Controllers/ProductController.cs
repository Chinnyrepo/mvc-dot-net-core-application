using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private SportsProContext Context { get; set; }

        public ProductController(SportsProContext ctx)
        {
            Context = ctx;
        }

        [Route("Products")]
        public ViewResult List()
        {
            var products = Context.Products.ToList();

            return View(products);
        }


        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = Context.Products.Find(id);
            return View("AddEdit", product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    Context.Products.Add(product);
                    TempData["message"] =
                    $"{product.Name} was added successfully.";
                }
                else
                {
                    Context.Products.Update(product);
                    TempData["message"] =
                    $"{product.Name} was edited successfully.";
                }
                Context.SaveChanges();
                return RedirectToAction("List", "Product");
                
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                return View("AddEdit", product);
            }
        }
        [HttpGet]
        public ViewResult Delete(int id)                 
        {
            var product = Context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)            
        {
            Context.Products.Remove(product);
            TempData["message"] =
            $"{product.Name} was deleted successfully.";
            Context.SaveChanges();
            return RedirectToAction("List", "Product");
        }
    }
}
