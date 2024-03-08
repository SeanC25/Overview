// using System.ComponentModel.DataAnnotations;
// using APG_CRM.CustomValidations;
// using Microsoft.AspNetCore.Mvc.Rendering;

// namespace APG_CRM.Web.Models
// {
//     public enum GlassType { FIRE, SAFETY, STANDARD, TOUGHENED, MIRROR }

//     public class Glass
//     {
//         public int Id { get; set; }

//         [Required]
//         [Display(Name = "Glass Type")]
//         public GlassType Type { get; set; } = GlassType.STANDARD;

//         [Required]
//         [StringLength(50)]
//         [Display(Name = "Glass Name")]
//         public string Name { get; set; } // this will be unique as no glass is called the same.

//         [Display(Name = "Thickness (mm)")]
//         public double Thickness { get; set; } = 4.0; // most common glass thickness.

//         [Range(0, 6500)]
//         [Display(Name = "Sheet Size (Length)")]
//         public int SheetSizeL { get; set; } = 3210; // European standard Create size for a Full sheet.

//         [Range(0, 6500)]
//         [Display(Name = "Sheet Size (Height)")]
//         public int SheetSizeH { get; set; } = 2250; // European standard Create size for a Full sheet.

//         [Range(0, 10000)]
//         [Display(Name = "Price Per Sheet ($)")]
//         public int PricePerSheet { get; set; }

//         [Range(0, 400)]
//         [Display(Name = "Box Sheet Quantity")]
//         public int BoxSheetQuantity { get; set; }

//         [Required]
//         [Display(Name = "Supplier ID")]
//         public int SupplierId { get; set; }    // Foreign key property
        
//         [Display(Name = "Supplier")]
//         public Supplier Supplier { get; set; } // Navigation property
//     }
// }
