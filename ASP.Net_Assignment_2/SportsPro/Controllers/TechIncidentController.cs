using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private SportsProContext Context { get; set; }

        public TechIncidentController(SportsProContext ctx)
        {
            Context = ctx;
        }


        public ViewResult Get()
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
        public IActionResult List(int TechnicianId)
        {
            int? sessionID = HttpContext.Session.GetInt32("sessionID");         
            if (TechnicianId == 0 && sessionID == null)
            {
                TempData["message"] = $"Please select a Technician.";
                return RedirectToAction("Get");
            }
            else
            {
                if (TechnicianId == 0) TechnicianId = (int)sessionID; 
                var model = new IncidentViewModel
                {
                    ActiveTechnician = Context.Technicians.Find(TechnicianId),
                    Customers = Context.Customers.ToList(),
                    Products = Context.Products.ToList()
                };

                IQueryable<Incident> query = Context.Incidents;
                query = query.Where(t => t.TechnicianID == model.ActiveTechnician.TechnicianID);
                query = query.Where(t => t.DateClosed == null);
                model.Incidents = query.ToList();

                if (model.Incidents.Count == 0)
                    TempData["message"] = $"No Open incidents for this Technician.";
                return View(model);
            }      
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Incident activeIncident = Context.Incidents.Find(id);
           
            var model = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                Incidents = Context.Incidents.ToList(),
                Technicians = Context.Technicians.ToList(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList(),
                Action = "Edit"
            };
            IQueryable<Incident> query = Context.Incidents;
            if (activeIncident.IncidentID != 0)
                query = query.Where(t => t.IncidentID == activeIncident.IncidentID);
            model.Incidents = query.ToList();
            model.ActiveIncident = Context.Incidents.Find(id);
            HttpContext.Session.SetInt32("sessionID", (int)model.ActiveIncident.TechnicianID);
            return View("Edit", model);
        }



        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                TempData["message"] = $"Incident Updated Successfully";
                Context.Incidents.Update(model.ActiveIncident);
                Context.SaveChanges();
                HttpContext.Session.SetInt32("sessionID", (int)model.ActiveIncident.TechnicianID);
                return RedirectToAction("List", "TechIncident", model.ActiveIncident.IncidentID);
            }
            else
            {
                model.ActiveIncident = Context.Incidents.Find(model.ActiveIncident.IncidentID);
                return View("Edit", model);
            }
        }

        //[HttpGet]
        //public IActionResult Delete(int id)                 
        //{
        //    var incident = Context.Incidents.Find(id);
        //    return View(incident);
        //}

        //[HttpPost]
        //public IActionResult Delete(Incident incident)           
        //{
        //    Context.Incidents.Remove(incident);
        //    Context.SaveChanges();
        //    return RedirectToAction("List", "Incident");
        //}
    }
}
