using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private SportsProContext Context { get; set; }

        public CustomerController(SportsProContext ctx)
        {
            Context = ctx;
        }
        [Route("Customers")]
        public IActionResult List()
        {
            var customers = Context.Customers.ToList();

            return View(customers);
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = Context.Countries.OrderBy(c => c.Name).ToList();
            return View("AddEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = Context.Countries.OrderBy(c => c.Name).ToList();
            var customer = Context.Customers.Find(id);
            return View("AddEdit", customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                    Context.Customers.Add(customer);
                else
                    Context.Customers.Update(customer);
                Context.SaveChanges();
                return RedirectToAction("List", "Customer");
            }
            else
            {
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                ViewBag.Countries = Context.Countries.OrderBy(c => c.Name).ToList();
                return View("AddEdit", customer);
            }
        }
        [HttpGet]
        public ViewResult Delete(int id)                 
        {
            var customer = Context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Customer customer)
        {
            Context.Customers.Remove(customer);
            Context.SaveChanges();
            return RedirectToAction("List", "Customer");
        }
    }
}
