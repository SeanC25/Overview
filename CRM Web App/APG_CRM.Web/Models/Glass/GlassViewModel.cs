using System.ComponentModel.DataAnnotations;
using APG_CRM.CustomValidations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using APG_CRM.Data.Entities;
using System.Collections.Generic;

namespace APG_CRM.Web.Models

{
    public enum GlassType { FIRE, SAFETY, STANDARD, TOUGHENED, MIRROR }

    public class Glass
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Glass Type")]
        public GlassType Type { get; set; } = GlassType.STANDARD;

        [Required]
        [StringLength(50)]
        [Display(Name = "Glass Name")]
        public string Name { get; set; } // this will be unique as no glass is called the same.

        [Display(Name = "Thickness (mm)")]
        public double Thickness { get; set; } = 4.0; // most common glass thickness.

        [Range(0, 6500)]
        [Display(Name = "Sheet Size (Length)")]
        public int SheetSizeL { get; set; } = 3210; // European standard Create size for a Full sheet.

        [Range(0, 6500)]
        [Display(Name = "Sheet Size (Height)")]
        public int SheetSizeH { get; set; } = 2250; // European standard Create size for a Full sheet.

        [Range(0, 10000)]
        [Display(Name = "Price Per Sheet (£)")]
        public int PricePerSheet { get; set; }

        [Range(0, 400)]
        [Display(Name = "Box Sheet Quantity")]
        public int BoxSheetQuantity { get; set; }

        
        [StringLength(200)]
        [Display(Name = "Glass Image")]
        public string ImageUrl { get; set; }
        //public List<IFormFile> GlassImages { get; set; }

        public List<string> ImageUrls { get; set; } // carousel property for multiple images

        
        // Constructor
        public Glass()
        {
            ImageUrls = new List<string>();
        }

        public int? PreviousGlassId { get; set; }   //used in the views to navigate
        public int? NextGlassId { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }    // Foreign key property

        public Supplier Supplier { get; set; } // Navigation property

        public string DisplaySheetSize { get; set; }

    }
}
