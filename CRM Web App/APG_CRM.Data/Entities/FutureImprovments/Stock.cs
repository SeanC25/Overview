using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities
{
    public class Stock
    {
        public int Id { get; set; }

        [Range(0, 400)]
        public int InStock { get; set; }

        // Data Annotation relationships
        // One-to-many relationship, where GlassStock is the child
        public int GlassId { get; set; } // Required foreign key property
        public Glass Glass { get; set; } = null!; // Required reference navigation to principal

    }
}
