using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext Context { get; set; }

        public IncidentController(SportsProContext ctx)
        {
            Context = ctx;
        }

        public IActionResult List()
        {
            var incidents = Context.Incidents.Include(c => c.Customer).Include(p => p.Product).Include(t => t.Technician).ToList();

            return View(incidents);
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
            ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();

            return View("AddEdit", new Incident());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            
            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
            ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList();

            var incident = Context.Incidents.Find(id);
            return View("AddEdit", incident);
        }



        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                    Context.Incidents.Add(incident);
                else
                    Context.Incidents.Update(incident);
                Context.SaveChanges();
                return RedirectToAction("List", "Incident");
            }
            else
            {
                ViewBag.Action = (incident.IncidentID == 0) ? "Add" : "Edit";               
                ViewBag.Customers = Context.Customers.ToList();
                ViewBag.Products = Context.Products.OrderBy(p => p.Name).ToList();
                ViewBag.Technicians = Context.Technicians.OrderBy(t => t.Name).ToList(); 
                return View("AddEdit", incident);
            }
        }
   
        [HttpGet]
        public IActionResult Delete(int id)                 
        {
            var incident = Context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)           
        {
            Context.Incidents.Remove(incident);
            Context.SaveChanges();
            return RedirectToAction("List", "Incident");
        }
    }
}
