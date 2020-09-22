using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
		public int ProductID { get; set; }

		[Required(ErrorMessage = "Please enter the Product Code.")]
		public string ProductCode { get; set; }

		[Required(ErrorMessage = "Please enter the Product Name.")]
		public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Product Price.")]
		[Range(1, 1000000, ErrorMessage = "Price must be between $1 and $1000000")]
		//[Column(TypeName = "decimal(8,2)")]
		public decimal YearlyPrice { get; set; }

		public DateTime ReleaseDate { get; set; } = DateTime.Today;

		public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + ProductID.ToString();
	}
}
