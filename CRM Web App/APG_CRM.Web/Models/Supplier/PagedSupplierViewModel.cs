using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using APG_CRM.Web.Models;

public class PaginatedSuppliersViewModel 
{
     public Paged<APG_CRM.Data.Entities.Supplier> Suppliers { get; set; }
    
    public string SearchTerm { get; set; }

    
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }

        [Required()]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string MainContactName { get; set; } // who do we deal with in this company most.

        [Required()]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(20)]
        public string PostCode { get; set; }

        [MaxLength(20)]
        public string Website { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        
}



// using APG_CRM.Data.Entities;
// using APG_CRM.Web.Models;

// namespace APG_CRM.Web.Models
// {
//     public class PagedSupplierViewModel : Paged<SupplierViewModel>
//     {

    // Add any other properties that your view might need
    // public IList<Supplier> Suppliers { get; set; }

        //public string Check { get; set; }

        //public SupplierScanViewModel SearchModel { get; set; }
//         // inherit properties from SupplierViewModel to PagedSupplierViewModel to be a paginated list of View. the Paged<T> mapping is in SupplierController
  
//     }
// }