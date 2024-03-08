using System;
using APG_CRM.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Web.Models
{
    public class SupplierViewModel
    {
        // Primary Key
        public int Id { get; set; }

        // Date when the supplier was added
        [Required]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        // Name of the supplier
        [Required]
        [MaxLength(100)]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        // Main contact person in the supplier's company
        [MaxLength(100)]
        [Display(Name = "Main Contact")]
        public string MainContactName { get; set; }

        // Address details
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(20)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        // Website, Phone and Email details
        [MaxLength(20)]
        [Url]
        public string Website { get; set; }

        [MaxLength(20)]
        [Phone]
        public string Phone { get; set; }

        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        // Additional details or notes about the supplier
        [MaxLength(500)]
        public string Notes { get; set; }
    }
}
