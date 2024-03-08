using System;
using System.Collections.Generic;
using APG_CRM.CustomValidations;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using APG_CRM.Data.Entities;

namespace APG_CRM.Web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date of establishment is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Established")]
        public DateTime DateEstablished { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Customer type is required.")]
        [StringLength(100, ErrorMessage = "Type cannot be longer than 100 characters.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Street address cannot be longer than 200 characters.")]
        public string Street { get; set; }

        [MaxLength(100, ErrorMessage = "City name cannot be longer than 100 characters.")]
        public string City { get; set; }

        [MaxLength(100, ErrorMessage = "County name cannot be longer than 100 characters.")]
        public string County { get; set; }

        [MaxLength(20, ErrorMessage = "Postcode cannot be longer than 20 characters.")]
        public string PostCode { get; set; }

         [StringLength(14, ErrorMessage = "Phone number cannot be longer than 14 characters.")]
       
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must be numeric.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Payment terms are required.")]
        [StringLength(200, ErrorMessage = "Payment terms cannot be longer than 200 characters.")]
        public string PaymentTerms { get; set; }

        // Navigation properties and SelectList properties
        public int? PreviousId { get; set; }
        public int? NextId { get; set; }
        public IEnumerable<Quotation> Quotations { get; set; }
        public SelectList QuotationSelectList { get; set; }
        public SelectList SurveySelectList { get; set; }

        // This property is used for relationships and may not need validation
        public int CustomerId { get; set; }
    }
}
