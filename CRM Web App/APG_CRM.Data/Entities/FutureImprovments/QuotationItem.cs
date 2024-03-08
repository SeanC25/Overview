using System;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities
{
    public class QuotationItem
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 100)]
        public int ItemQuantity { get; set; }

        [Required]
        [Range(0, 4000)]
        public decimal SizeA { get; set; }

        [Required]
        [Range(0, 4000)]
        public decimal SizeB { get; set; }

        public decimal GlassArea => SizeA * SizeB;

        [Required]
        public bool Shaped { get; set; } = false;

        [Required]
        [Range(1, 100)]
        public decimal ItemCost { get; set; }

        public string Description { get; set; }

        // relationships
        // public int GlassId { get; set; }
        // public Glass Item { get; set; }

        // public int QuotationId { get; set; }
        // public Quotation Quotation { get; set; }

    }
}