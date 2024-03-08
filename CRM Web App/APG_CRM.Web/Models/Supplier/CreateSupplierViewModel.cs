using System;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Web.Models
{
    public class CreateSupplierViewModel
    {
        public int Id { get; set; }
        
        // If DateAdded should be set by the system (e.g., DateTime.Now), remove this property from ViewModel
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Please provide the Supplier name")]
        [Display(Name = "Supplier Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Main Contact Name")]
        [MaxLength(30)]
        public string MainContactName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(60)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string City { get; set; }

        [MaxLength(20)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [MaxLength(20)]
        public string Website { get; set; }

        [MaxLength(20)]
         [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [MaxLength(30)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")] 
        public string Email { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Additional fields as needed
    }
}
