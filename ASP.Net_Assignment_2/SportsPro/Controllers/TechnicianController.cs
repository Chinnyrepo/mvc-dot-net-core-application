using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private SportsProContext Context { get; set; }

        public TechnicianController(SportsProContext ctx)
        {
            Context = ctx;
        }

        [Route("Technicians")]
        public IActionResult List()
        {
            var technicians = Context.Technicians.ToList();
            return View(technicians);
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Technician());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var technician = Context.Technicians.Find(id);
            return View("AddEdit", technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                    Context.Technicians.Add(technician);
                else
                    Context.Technicians.Update(technician);
                Context.SaveChanges();
                return RedirectToAction("List", "Technician");
            }
            else
            {
                ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
                return View("AddEdit", technician);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)               
        {
            var technician = Context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)           
        {
            Context.Technicians.Remove(technician);
            Context.SaveChanges();
            return RedirectToAction("List", "Technician");
        }
    }
}
