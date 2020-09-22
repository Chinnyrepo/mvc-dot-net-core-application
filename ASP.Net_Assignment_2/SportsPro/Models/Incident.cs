using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Incident
    {
		public int IncidentID { get; set; }

		[Required(ErrorMessage = "Please select a Customer.")]
		public int CustomerID { get; set; }
		
		public Customer Customer { get; set; }

		[Required(ErrorMessage = "Please select a Product Name.")]
		public int ProductID { get; set; }
		
		public Product Product { get; set; }  

		public int? TechnicianID { get; set; }     
		public Technician Technician { get; set; }

		[Required(ErrorMessage = "Please enter Incident Title.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Please enter Incident Description.")]
		public string Description { get; set; }

		public DateTime DateOpened { get; set; } = DateTime.Now;

		public DateTime? DateClosed { get; set; } = null;
	}
}