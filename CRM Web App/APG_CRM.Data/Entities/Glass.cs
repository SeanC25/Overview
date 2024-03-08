using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APG_CRM.Data.Entities
{
    public class Glass
    {
        public int Id { get; set; }

        [Required]
        public GlassType Type { get; set; } = GlassType.STANDARD;

        [Required]
        [StringLength(50)]
        public string Name { get; set; }// this will be a unique as no glass is called the same. 

        public double Thickness { get; set; } = 4.0; // most common glass thickness.

        [Range(0, 6500)]
        [DisplayName("Size L")]
        public int SheetSizeL { get; set; } = 3210; // Eurpean standard Create size for a Full sheet.

        [Range(0, 6500)]
        [DisplayName("Size H")]
        public int SheetSizeH { get; set; } = 2250; // Eurpean standard Create size for a Full sheet.

        [Range(0, 10000)]
        [DisplayName("Price")]
        public int PricePerSheet { get; set; }

        [Range(0, 400)]
        [DisplayName("Box Quantity")]
        public int BoxSheetQuantity { get; set; }

        public string ImageUrl { get; set; }

         public ICollection<GlassImage> Images { get; set; } // carousel property for multiple images

        public int? PreviousGlassId { get; set; }   //used in the views to navigate
        public int? NextGlassId { get; set; }


        [Required]
        [DisplayName("Supplier Name")]
        public int SupplierId { get; set; }    // Foreign key property

        public Supplier Supplier { get; set; } // Navigation property

    }

    public enum GlassType { FIRE, SAFETY, STANDARD, TOUGHENED, MIRROR, TEXTURED }


}
