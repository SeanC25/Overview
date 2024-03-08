using System;
using System.Collections.Generic;
using APG_CRM.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities
{

    public class Customer
    {
        public int Id { get; set; }

        public DateTime DateEstablished { get; set; } = DateTime.Now.Date;

        public string Type { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        // Address components
        [MaxLength(200)]
        public string Street { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string County { get; set; }

        [MaxLength(20)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]  // Added 

        //Email validation
        //[CorporateEmailDomain("example.com", "companyname.com")]
        public string Email { get; set; }

        public string Description { get; set; } // General description of the customer

        public string PaymentTerms { get; set; }

        //public string PastCustomer { get; set; }

        public IList<Quotation> Quotations { get; set; } = new List<Quotation>();

        // public IList<Survey> Surveys { get; set; } = new List<Survey>();

        // Define a collection of orders related to this customer
        public IList<Order> Orders { get; set; } = new List<Order>();

        public int? PreviousId { get; set; }   //used in the views to navigate
        public int? NextId { get; set; }

    }
}

