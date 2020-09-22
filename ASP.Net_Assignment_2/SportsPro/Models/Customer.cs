using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "Please enter Customer First Name.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter Customer Last Name.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter Customer Address.")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Please enter Customer City.")]
		public string City { get; set; }

		[Required(ErrorMessage = "Please enter Customer State.")]
		public string State { get; set; }

		[Required(ErrorMessage = "Please enter Customer Postal Code.")]
		public string PostalCode { get; set; }

		[Required(ErrorMessage = "Please enter Customer Country.")]
		public string CountryID { get; set; }

		public Country Country { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property
	}
}