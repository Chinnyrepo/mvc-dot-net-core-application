using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Technician
    {
		public int TechnicianID { get; set; }

		[Required(ErrorMessage = "Please enter Technicians Name.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter Technicians Email.")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter Technicians Phone number.")]
		[StringLength(10,MinimumLength =10, ErrorMessage ="Minimum and Maximum 10 Digit Required")]
		public string Phone { get; set; }
	}
}
