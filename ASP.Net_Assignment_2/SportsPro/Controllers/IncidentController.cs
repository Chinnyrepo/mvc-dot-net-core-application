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

        [Route("Incidents")]
        public ViewResult List()
        {
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var model = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = Context.Incidents.ToList(),
                Technicians = Context.Technicians.ToList(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList(),
            };
            IQueryable<Incident> query = Context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(t =>
                    t.IncidentID ==
                        activeIncident.IncidentID);
            if (activeTechnician.TechnicianID != 0)
                query = query.Where(t =>
                    t.Technician.TechnicianID ==
                        activeTechnician.TechnicianID);
            model.Incidents = query.ToList();
            return View(model);

        }


        [HttpGet]
        public IActionResult Add()
        {
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var model = new IncidentListViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = Context.Incidents.ToList(),
                Technicians = Context.Technicians.ToList(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList(),
                Action = "Add"
            };
            IQueryable<Incident> query = Context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(t =>
                    t.IncidentID ==
                        activeIncident.IncidentID);
            if (activeTechnician.TechnicianID != 0)
                query = query.Where(t =>
                    t.Technician.TechnicianID ==
                        activeTechnician.TechnicianID);
            model.Incidents = query.ToList();
            return View("AddEdit", model);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var model = new IncidentListViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = Context.Incidents.ToList(),
                Technicians = Context.Technicians.ToList(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList(),
                Action = "Edit"
            };
            IQueryable<Incident> query = Context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(t =>
                    t.IncidentID ==
                        activeIncident.IncidentID);
            if (activeTechnician.TechnicianID != 0)
                query = query.Where(t =>
                    t.Technician.TechnicianID ==
                        activeTechnician.TechnicianID);
            model.Incidents = query.ToList();
            model.ActiveIncident = Context.Incidents.Find(id);
            return View("AddEdit", model);
        }



        [HttpPost]
        public IActionResult Edit(IncidentListViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ActiveIncident.IncidentID == 0)
                    Context.Incidents.Add(model.ActiveIncident);
                else
                    Context.Incidents.Update(model.ActiveIncident);
                Context.SaveChanges();
                return RedirectToAction("List", "Incident");
            }
            else
            {
                ViewBag.Action = (model.ActiveIncident.IncidentID == 0) ? "Add" : "Edit";               
                return View("AddEdit", model);
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
