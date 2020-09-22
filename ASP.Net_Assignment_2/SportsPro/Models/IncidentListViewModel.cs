using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public class IncidentListViewModel
    {
        public List<Customer> Customers { get; set; }
        public int TechnicianID { get; set; }
        public Customer ActiveCust { get; set; }
        public string FilterString { get; set; }
        public Incident ActiveIncident { get; set; }
        public Technician ActiveTechnician { get; set; }
        public string Action { get; set; }
        
        public List<Product> Products { get; set; }

        private List<Incident> incidents;
        public List<Incident> Incidents {
            get => incidents;
            set 
            {
                incidents = value;
            } 
        }

        private List<Technician> technicians;
        public List<Technician> Technicians
        {
            get => technicians; 
            set
            {
                technicians = value; 
            }
        }

        // methods to help view determine active link 
        public string CheckActiveIncident(int d) => d == ActiveIncident.IncidentID ? "active" : "";
        public string CheckActiveTechnician(int e) => e == ActiveTechnician.TechnicianID ? "active" : "";

    }
}


